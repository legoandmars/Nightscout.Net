using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace Nightscout.Net.Models
{
    public class NightscoutSettings
    {
        [JsonProperty("units")]
        public GlucoseUnit? Units { get; set; } = null!;

        /// <summary>
        /// Time format - 12 hour or 24 hour clock
        /// </summary>
        [JsonProperty("timeFormat")]
        public int? TimeFormat { get; set; } = null!;

        [JsonProperty("nightMode")]
        public bool? NightMode { get; set; } = null!;

        [JsonProperty("editMode")]
        public bool? EditMode { get; set; } = null!;

        [JsonProperty("showRawbg")]
        public string ShowRawBG { get; set; } = null!;

        [JsonProperty("customTitle")]
        public string CustomTitle { get; set; } = null!;

        [JsonProperty("theme")]
        public string Theme { get; set; } = null!;

        [JsonProperty("alarmUrgentHigh")]
        public bool? AlarmUrgentHigh { get; set; } = null!;

        [JsonProperty("alarmUrgentHighMins")]
        public int[]? AlarmUrgentHighMins { get; set; } = null!;

        [JsonProperty("alarmHigh")]
        public bool? AlarmHigh { get; set; } = null!;

        [JsonProperty("alarmHighMins")]
        public int[]? AlarmHighMins { get; set; } = null!;

        [JsonProperty("alarmLow")]
        public bool? AlarmLow { get; set; } = null!;

        [JsonProperty("alarmLowMins")]
        public int[]? AlarmLowMins { get; set; } = null!;

        [JsonProperty("alarmUrgentLow")]
        public bool? AlarmUrgentLow { get; set; } = null!;

        [JsonProperty("alarmUrgentLowMins")]
        public int[]? AlarmUrgentLowMins { get; set; } = null!;

        [JsonProperty("alarmUrgentMins")]
        public int[]? AlarmUrgentMins { get; set; } = null!;

        [JsonProperty("alarmWarnMins")]
        public int[]? AlarmWarnMins { get; set; } = null!;

        [JsonProperty("alarmTimeagoWarn")]
        public bool? AlarmTimeagoWarn { get; set; } = null!;

        [JsonProperty("alarmTimeagoWarnMins")]
        public string AlarmTimeagoWarnMins { get; set; } = null!;

        [JsonProperty("alarmTimeagoUrgent")]
        public bool? AlarmTimeagoUrgent { get; set; } = null!;

        [JsonProperty("alarmTimeagoUrgentMins")]
        public string AlarmTimeagoUrgentMins { get; set; } = null!;

        [JsonProperty("alarmPumpBatteryLow")]
        public bool? AlarmPumpBatteryLow { get; set; } = null!;

        [JsonProperty("language")]
        public string Language { get; set; } = null!;

        [JsonProperty("scaleY")]
        public string ScaleY { get; set; } = null!;

        [JsonProperty("showPlugins")]
        public string ShowPlugins { get; set; } = null!;

        [JsonProperty("showForecast")]
        public string ShowForecast { get; set; } = null!;

        [JsonProperty("focusHours")]
        public int? FocusHours { get; set; } = null!;

        [JsonProperty("heartbeat")]
        public int? Heartbeat { get; set; } = null!;

        [JsonProperty("baseURL")]
        public string BaseURL { get; set; } = null!;

        [JsonProperty("authDefaultRoles")]
        public string AuthDefaultRoles { get; set; } = null!;

        [JsonProperty("thresholds")]
        public GlucoseThresholds Thresholds { get; set; } = null!;

        [JsonProperty("insecureUseHttp")]
        public bool? InsecureUseHttp { get; set; } = null!;

        [JsonProperty("secureHstsHeader")]
        public bool? SecureHstsHeader { get; set; } = null!;

        [JsonProperty("secureHstsHeaderIncludeSubdomains")]
        public bool? SecureHstsHeaderIncludeSubdomains { get; set; } = null!;

        [JsonProperty("secureHstsHeaderPreload")]
        public bool? SecureHstsHeaderPreload { get; set; } = null!;

        [JsonProperty("secureCsp")]
        public bool? SecureCsp { get; set; } = null!;

        [JsonProperty("deNormalizeDates")]
        public bool? DeNormalizeDates { get; set; } = null!;

        [JsonProperty("showClockDelta")]
        public bool? ShowClockDelta { get; set; } = null!;

        [JsonProperty("showClockLastTime")]
        public bool? ShowClockLastTime { get; set; } = null!;

        [JsonProperty("DEFAULT_FEATURES")]
        public string[] DEFAULTFEATURES { get; set; } = null!;

        [JsonProperty("alarmTypes")]
        public string[] AlarmTypes { get; set; } = null!;

        [JsonProperty("enable")]
        public string[] Enable { get; set; } = null!;
    }
}
