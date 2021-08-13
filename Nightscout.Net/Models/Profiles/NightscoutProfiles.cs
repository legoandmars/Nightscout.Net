using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nightscout.Net.Models
{
    public class NightscoutProfiles
    {
        [JsonProperty("_id")]
        private string ID { get; set; } = null!;

        /// <summary>
        /// Profile name that nightscout uses by default.
        /// </summary>
        [JsonProperty("defaultProfile")]
        public string DefaultProfileName { get; set; } = null!;

        /// <summary>
        /// A list of profiles by name.
        /// </summary>
        [JsonProperty("store")]
        public Dictionary<string, NightscoutProfile> Profiles { get; set; } = null!;

        /// <summary>
        /// When Nightscout was last started.
        /// </summary>
        [JsonProperty("startDate")]
        public DateTime? StartDate { get; set; } = null!;

        [JsonProperty("mills")]
        private int? Mills { get; set; } = null!;

        /// <summary>
        /// The unit this profile uses - mmol or mgdl. 
        /// </summary>
        [JsonProperty("units")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GlucoseUnit? Units { get; set; } = null!;

        /// <summary>
        /// When nightscout was first started. 
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; } = null!;

        [JsonIgnore]
        public NightscoutProfile DefaultProfile
        {
            get
            {
                return Profiles[DefaultProfileName];
            }
        }

    }
}
