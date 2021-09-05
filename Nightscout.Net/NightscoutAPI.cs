using System;
using Nightscout.Net.Http;
using Nightscout.Net.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Nightscout.Net
{
    public class NightscoutAPI : IDisposable
    {
        internal bool IsDisposed { get; set; }

        public event EventHandler<NightscoutEntry[]> OnEntriesFetched = delegate { };

        private NightscoutAPIOptions _options;
        private readonly IHttpService _httpService;
        private NightscoutStatus? _status;
        private NightscoutEntry? _lastEntryCached;


        public NightscoutAPI(NightscoutAPIOptions nightscoutOptions) // todo more constructors
        {
            _options = nightscoutOptions;
#if RELEASE_UNITY
            _httpService = new UnityWebRequestService()
            {
                BaseURL = nightscoutOptions.SiteUrl.ToString(),
                Timeout = nightscoutOptions.Timeout,
                UserAgent = nightscoutOptions.UserAgent,
            };
#else
            _httpService = new HttpClientService(nightscoutOptions.SiteUrl.ToString(), nightscoutOptions.Timeout, nightscoutOptions.UserAgent);
#endif
            if (nightscoutOptions.AutomaticallyFetchSGVs)
            {
                AutomaticallyFetchEntry();
            }
        }

        private NightscoutEntry? _lastAutomaticEntryCached;
        private float _sensorDelayInSeconds = 5f; // extra buffer to give some time for upload
        private float _sensorUpdateRateInSeconds = 300f; // how often the sensor refreshes - essentially every CGM on earth has this as 5 minutes
        private float _sensorUpdateRateWhenFailedInSeconds = 5f;

        private async void AutomaticallyFetchEntry(float delayInSeconds = 0f)
        {
            await Task.Delay((int)(delayInSeconds * 1000));
            var entries = await FetchEntries().ConfigureAwait(false);
            var latestEntry = entries?[0];
            if (latestEntry != null && _lastAutomaticEntryCached?.ID != latestEntry.ID)
            {
                // new entry
                _lastAutomaticEntryCached = latestEntry;

                var currentDate = DateTime.Now;
                var currentSecondValue = (currentDate.Minute * 60) + currentDate.Second;
                var pastSecondValue = (latestEntry.Date?.Minute * 60) + latestEntry.Date?.Second;

                var elapsedSeconds = currentSecondValue - pastSecondValue;
                float? timeToWait = _sensorUpdateRateInSeconds + _sensorDelayInSeconds - elapsedSeconds;

                AutomaticallyFetchEntry(timeToWait.HasValue ? timeToWait.Value : _sensorUpdateRateWhenFailedInSeconds);
            }
            else AutomaticallyFetchEntry(_sensorUpdateRateWhenFailedInSeconds);
        }

        public async Task<NightscoutEntry[]?> FetchEntries(NightscoutEntryFetchOptions? options = null)
        {
            if (options == null) options = new NightscoutEntryFetchOptions();
            NightscoutEntry[]? entries = await FetchEntriesInternal($"api/v1/entries/sgv.json?count={_options.PageSize}").ConfigureAwait(false);
            if (entries != null && entries.Length > 0) {
                if(entries[0] != null && _lastEntryCached?.ID != entries[0].ID)
                {
                    _lastEntryCached = entries[0];
                    OnEntriesFetched?.Invoke(null, entries);
                }
                return entries;
            }
            else throw new Exception("NOT FOUND!");

            //todo smooth pagination and caching
        }

        public async Task<NightscoutEntry?> FetchLatestEntry()
        {
            NightscoutEntry[]? entries = await FetchEntriesInternal("api/v1/entries/sgv.json?count=1").ConfigureAwait(false);
            if (entries != null && entries.Length > 0) return entries[0];
            else throw new Exception("NOT FOUND!");
        }

        public async Task<NightscoutProfiles?> Profiles()
        {
            var profiles = await NightscoutAPIRequestInternal<NightscoutProfiles[]>("api/v1/profile.json").ConfigureAwait(false);
            return profiles?[0];
        }

        public async Task<NightscoutStatus?> GetStatus()
        {
            if (_status == null) _status = await NightscoutAPIRequestInternal<NightscoutStatus>("api/v1/status.json").ConfigureAwait(false);
            return _status;
        }

        public async Task<NightscoutSettings?> GetSettings()
        {
            NightscoutSettings? settings = null;
            var status = await GetStatus().ConfigureAwait(false);
            if (status != null) settings = status.Settings;
            return settings;
        }

        private async Task<NightscoutEntry[]?> FetchEntriesInternal(string url, CancellationToken token = default)
        {
            if(!await NightscoutAPIAvailable()) throw new Exception("API unavailable!");
            var response = await _httpService.GetAsync(url, token).ConfigureAwait(false);
            if (!response.Successful)
                return null;

            NightscoutEntry[] entries = await response.ReadAsObjectAsync<NightscoutEntry[]>().ConfigureAwait(false);
            // cache here?
            return entries;
        }

        private async Task<T?> NightscoutAPIRequestInternal<T>(string url, CancellationToken token = default) where T : class
        {
            var response = await _httpService.GetAsync(url, token).ConfigureAwait(false);
            if (!response.Successful)
                return null;

            var entries = await response.ReadAsObjectAsync<T>().ConfigureAwait(false);
            return entries;
        }

        private async Task<bool> NightscoutAPIAvailable()
        {
            // todo: add other forms of non-public auth/logging in so non-public sites work properly
            NightscoutStatus? status = await GetStatus().ConfigureAwait(false);
            return status != null && status.Status == "ok" && status.APIEnabled.HasValue ? status.APIEnabled.Value : false;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (_httpService is IDisposable disposable)
                disposable.Dispose();
            IsDisposed = true;
        }
    }
}
