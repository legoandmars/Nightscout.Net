using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nightscout.Net.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class NightscoutEntry : IEquatable<NightscoutEntry>
    {
        /// <summary>
        /// The ID of the entry object.
        /// </summary>
        [JsonProperty("_id")]
        public string ID { get; set; } = null!;

        /// <summary>
        /// The device the entry came from.
        /// </summary>
        [JsonProperty("device")]
        public string Device { get; set; } = null!;
        /// <summary>
        /// The date at which the event was uploaded to Nightscout.
        /// </summary>
        [JsonProperty("date")]
        public UInt64? DateTimestamp { get; set; } = null!;

        /// <summary>
        /// A UTC string of the date at which the event was uploaded.
        /// </summary>
        [JsonProperty("dateString")]
        public DateTime? Date { get; set; } = null!;

        /// <summary>
        /// The entry's SGV (sensor glucose value). This is generally between 40 and 401 but can vary between sensors. Always in mg/dL regardless of user preferences.
        /// </summary>
        [JsonProperty("sgv")]
        public int? SGV { get; set; } = null!;

        /// <summary>
        /// The difference between the current and last SGV.
        /// </summary>
        [JsonProperty("delta")]
        public double? Delta { get; set; } = null!;

        /// <summary>
        /// The direction the SGV is heading.
        /// </summary>
        [JsonProperty("direction")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SGVDirection? Direction { get; set; } = null!;

        /// <summary>
        /// The type of the entry, usually sgv.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = null!;

        /// <summary>
        /// Filtered SVG data.
        /// </summary>
        [JsonProperty("filtered")]
        public double Filtered { get; set; }

        /// <summary>
        /// Unfiltered SVG data.
        /// </summary>
        [JsonProperty("unfiltered")]
        public double Unfiltered { get; set; }

        /// <summary>
        /// Signal strength to the CGM transmitter.
        /// </summary>
        [JsonProperty("rssi")]
        public int Rssi { get; set; }

        /// <summary>
        /// How noisy the values outputted by the CGM transmitter are.
        /// </summary>
        [JsonProperty("noise")]
        public int Noise { get; set; }

        /// <summary>
        /// System time when the entry was uploaded.
        /// </summary>
        [JsonProperty("sysTime")]
        public DateTime SystemTime { get; set; }

        /// <summary>
        /// UTC Offset of the system time.
        /// </summary>
        [JsonProperty("utcOffset")]
        public int UtcOffset { get; set; }

        /// <summary>
        /// A unicode arrow of the direction the SGV is headed.
        /// </summary>
        [JsonIgnore]
        public string DirectionArrow
        {
            get
            {
                switch (Direction)
                {
                    case SGVDirection.DoubleUp:
                        return "↑";
                    case SGVDirection.SingleUp:
                        return "↑";
                    case SGVDirection.FortyFiveUp:
                        return "↗";
                    case SGVDirection.Flat:
                        return "→";
                    case SGVDirection.FortyFiveDown:
                        return "↘";
                    case SGVDirection.SingleDown:
                        return "↓";
                    case SGVDirection.DoubleDown:
                        return "↓";
                    default:
                        return "";

                }
            }
        }

        public bool Equals(NightscoutEntry? other) => ID == other?.ID;

        public enum SGVDirection
        {
            None,
            DoubleUp,
            SingleUp,
            FortyFiveUp,
            Flat,
            FortyFiveDown,
            SingleDown,
            DoubleDown,
            [EnumMember(Value = "NOT COMPUTABLE")]
            NotComputable,
            [EnumMember(Value = "RATE OUT OF RANGE")]
            RateOutOfRange
        }
    }

}
