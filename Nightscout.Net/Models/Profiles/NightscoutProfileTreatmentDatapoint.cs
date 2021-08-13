using Newtonsoft.Json;

namespace Nightscout.Net.Models
{
    public class NightscoutProfileTreatmentDatapoint
    {
        /// <summary>
        /// The time the datapoint will take effect.
        /// </summary>
        [JsonProperty("time")]
        public string Time { get; set; } = null!;

        /// <summary>
        /// Value of the datapoint.
        /// </summary>
        [JsonProperty("value")]
        public double? Value { get; set; } = null!;

        /// <summary>
        /// The time (in seconds) the datapoint will take effect.
        /// </summary>
        [JsonProperty("timeAsSeconds")]
        public int? TimeAsSeconds { get; set; } = null!;
    }
}
