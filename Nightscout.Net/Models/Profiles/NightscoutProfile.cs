using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nightscout.Net.Models
{
    public class NightscoutProfile
    {
        /// <summary>
        /// Duration of insulin action - how long insulin will lower the blood sugar for.
        /// </summary>
        [JsonProperty("dia")]
        public int? Dia { get; set; } = null!;

        // I legitimately have no idea what these do or what they represent
        [JsonProperty("carbs_hr")]
        public int? CarbsHr { get; set; } = null!;

        [JsonProperty("delay")]
        public int? Delay { get; set; } = null!;

        /// <summary>
        /// Timezone of the current profile.
        /// </summary>
        [JsonProperty("timezone")]
        public string Timezone { get; set; } = null!;

        /// <summary>
        /// The ratios of how much insulin needs to be taken for carbs throughout various points of the day.
        /// </summary>
        [JsonProperty("carbratio")]
        public NightscoutProfileTreatmentDatapoint[] CarbRatio { get; set; } = null!;

        /// <summary>
        /// The ratios of how much insulin needs to be taken for blood sugar values throughout various points of the day.
        /// </summary>
        [JsonProperty("sens")]
        public NightscoutProfileTreatmentDatapoint[] Sens { get; set; } = null!;

        /// <summary>
        /// How much basal needs to be taken ambiently throughout various points of the day.
        /// </summary>
        [JsonProperty("basal")]
        public NightscoutProfileTreatmentDatapoint[] Basal { get; set; } = null!;

        /// <summary>
        /// The number that defines a low blood sugar throughout various points of the day.
        /// </summary>
        [JsonProperty("target_low")]
        public NightscoutProfileTreatmentDatapoint[] TargetLow { get; set; } = null!;

        /// <summary>
        /// The number that defines a low blood sugar throughout various points of the day.
        /// </summary>
        [JsonProperty("target_high")]
        public NightscoutProfileTreatmentDatapoint[] TargetHigh { get; set; } = null!;

        /// <summary>
        /// Profile start date.
        /// </summary>
        [JsonProperty("startDate")]
        public DateTime? StartDate { get; set; } = null!;

        /// <summary>
        /// The unit this profile uses - mmol or mgdl. 
        /// </summary>
        [JsonProperty("units")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GlucoseUnit? Units { get; set; } = null!;
    }
}
