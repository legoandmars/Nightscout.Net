using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Nightscout.Net.Models
{
    public class NightscoutStatus
    {
        [JsonProperty("status")]
        public string Status { get; set; } = null!;

        [JsonProperty("name")]
        public string Name { get; set; } = null!;

        [JsonProperty("version")]
        public string Version { get; set; } = null!;

        [JsonProperty("serverTime")]
        public DateTime? ServerTime { get; set; } = null!;

        [JsonProperty("serverTimeEpoch")]
        public long? ServerTimeEpoch { get; set; } = null!;

        [JsonProperty("apiEnabled")]
        public bool? APIEnabled { get; set; } = null!;

        [JsonProperty("careportalEnabled")]
        public bool? CarePortalEnabled { get; set; } = null!;

        [JsonProperty("boluscalcEnabled")]
        public bool? BolusCalculationEnabled { get; set; } = null!;

        [JsonProperty("settings")]
        public NightscoutSettings Settings { get; set; } = null!;

        /*[JsonProperty("extendedSettings")]
        public ExtendedSettings ExtendedSettings { get; set; } = null!;*/

        [JsonProperty("authorized")]
        public object Authorized { get; set; } = null!;
    }
}
