using System.Runtime.Serialization;

namespace Nightscout.Net.Models
{
    public enum GlucoseUnit
    {
        [EnumMember(Value = "mg/dl")]
        mgdl,
        mmol
    }
}
