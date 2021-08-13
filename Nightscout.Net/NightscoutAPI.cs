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
                // this is horrible please do something else I beg you
                var timer = new Timer(async (e) =>
                {
                    await FetchEntries();
                }, null, 1000, 60000);
            }
        }

        public async Task<NightscoutEntry[]?> FetchEntries(NightscoutEntryFetchOptions? options = null)
        {
            if (options == null) options = new NightscoutEntryFetchOptions();
            // filter need be sir
            NightscoutEntry[]? entries = await FetchEntriesInternal($"api/v1/entries/sgv.json?count={_options.PageSize}");
            if (entries != null && entries.Length > 0) {
                OnEntriesFetched?.Invoke(null, entries);
                return entries;
            }
            else throw new Exception("NOT FOUND!");

            //todo smooth pagination
        }

        public async Task<NightscoutEntry?> FetchLatestEntry()
        {
            NightscoutEntry[]? entries = await FetchEntriesInternal("api/v1/entries/sgv.json?count=1");
            if (entries != null && entries.Length > 0) return entries[0];
            else throw new Exception("NOT FOUND!");
        }

        public async Task<NightscoutProfiles?> Profiles()
        {
            var profiles = await NightscoutAPIRequestInternal<NightscoutProfiles[]>("api/v1/profile.json");
            return profiles?[0];
        }

        public async Task<NightscoutStatus?> GetStatus()
        {
            if (_status == null) _status = await NightscoutAPIRequestInternal<NightscoutStatus>("api/v1/status.json");
            return _status;
        }

        public async Task<NightscoutSettings?> GetSettings()
        {
            NightscoutSettings? settings = null;
            var status = await GetStatus();
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
            NightscoutStatus? status = await GetStatus();
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
