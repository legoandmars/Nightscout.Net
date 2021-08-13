using System;


namespace Nightscout.Net
{
    public class NightscoutAPIOptions
    {
        public Uri SiteUrl { get; set; }
        public string UserAgent;
        public bool AutomaticallyFetchSGVs { get; set; }
        public uint PageSize { get; set; }
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

        public NightscoutAPIOptions(string siteUrl, string applicationName, string applicationVersion, uint pageSize, bool automaticallyFetchSgvs = true) : this(siteUrl, applicationName, Version.Parse(applicationVersion), pageSize, automaticallyFetchSgvs)
        {

        }

        public NightscoutAPIOptions(string siteUrl, string applicationName, Version applicationVersion, uint pageSize, bool automaticallyFetchSgvs = true) : this(siteUrl, $"{applicationName}/{applicationVersion}", pageSize, automaticallyFetchSgvs)
        {

        }

        public NightscoutAPIOptions(string siteUrl, string userAgent, uint pageSize, bool automaticallyFetchSgvs = true)
        {
            bool passed = Uri.TryCreate(siteUrl, UriKind.Absolute, out Uri siteUri) && (siteUri.Scheme == Uri.UriSchemeHttp || siteUri.Scheme == Uri.UriSchemeHttps);
            // look at this later
            if (!passed) throw new Exception("Invalid url!");
            else SiteUrl = siteUri;

            UserAgent = userAgent;
            AutomaticallyFetchSGVs = automaticallyFetchSgvs;
            PageSize = pageSize;
        }
    }
}
