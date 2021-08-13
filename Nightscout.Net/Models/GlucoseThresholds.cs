using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Nightscout.Net.Models
{
    public class GlucoseThresholds
    {
        [JsonProperty("bgHigh")]
        public int BgHigh { get; set; }

        [JsonProperty("bgTargetTop")]
        public int BgTargetTop { get; set; }

        [JsonProperty("bgTargetBottom")]
        public int BgTargetBottom { get; set; }

        [JsonProperty("bgLow")]
        public int BgLow { get; set; }
    }
}
