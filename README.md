# Nightscout.Net
An unofficial .NET library for the [Nightscout](https://github.com/nightscout/cgm-remote-monitor) API.

# Installation
This library has two versions:
- A normal version for C# apps and other normal .NET usecases
- A Unity version intended to make Unity implementation easier

These versions are completely the same spec-wise, the only differences are internal dependencies and HTTP request handling. 

You can grab the latest stable version from the [Releases](https://github.com/legoandmars/Nightscout.Net/releases/latest) page, or grab the latest build of `main` as an artifact from the [GitHub Actions](https://github.com/legoandmars/Nightscout.Net/actions) page.

To install the regular version, simply add `Nightscout.Net.dll` as a reference in your project.

To install the Unity version, extract `Nightscout.Net (Unity).zip` into your `Assets/Plugins` folder. Make sure you a version of [Newtonsoft.JSON](https://github.com/jilleJr/Newtonsoft.Json-for-Unity) for Unity installed.

# Example Usage
## Connect to the Nightscout API
```csharp
var options = new NightscoutAPIOptions(
    "https://testsite.herokuapp.com", // User's Nightscout site
    "Nightscout.ExampleApp/1.0", // The UserAgent that will be used for web requests
    10, // Page length of 10
    true // Automatically update glucose values and send them to OnEntriesFetched
);

var API = new NightscoutAPI(options);
```

## Manually fetching glucose values
```csharp
var entry = await API.FetchLatestEntry();

var latestGlucose = entry.SGV;
Console.WriteLine(latestGlucose); // Write the latest glucose value to the console
```

## Automatically fetching glucose values
Since auto-fetching of glucose is enabled in the `NightscoutAPIOptions`, simply bind a method to the `OnEntriesFetched` event to get automatic updates.
```csharp
API.OnEntriesFetched += EntriesUpdated;

...

private void EntriesUpdated(object sender, Models.NightscoutEntry[] entries)
{
    // Entries will always be sorted by newest first
    var latestGlucose = entries[0].SGV;
    Console.WriteLine(latestGlucose); // Write the latest glucose value to the console
}
```

## User site options
Nightscout is very customizable and thus has many on-site options that, when implemented, can make your app feel more familiar.
### Units
```csharp
var settings = await API.GetSettings();

var units = settings.Units; // The measurement unit displayed by the site - mg/dL or mmol/L.
// Keep in mind that this is purely for display and the SGV value itself stays mg/dL regardless of this setting.
// Therefore, you should convert before displaying to the user:
var mgdlGlucose = $"{latestGlucose} mg/dL";
var mmolGlucose = $"{((float)latestGlucose / 18.016).ToString("#.#")} mmol/L"; 
// mg/dL to mmol/L conversion is exactly 18.016, generally only displayed up to the first decimal point.
// This will hopefully have a dedicated method in the future.
if (settings.Units == GlucoseUnit.mgdl) Console.WriteLine(mgdlGlucose);
else Console.WriteLine(mmolGlucose);
```

### Thresholds
```csharp
var settings = await API.GetSettings();

var thresholds = settings.Thresholds; // Various values for what quantifies a high/low/in-range glucose value.

if(latestGlucose >= thresholds.BgHigh) Console.WriteLine("Your glucose is high!");
else if(latestGlucose <= thresholds.BgLow) Console.WriteLine("Your glucose is low!");
else Console.WriteLine("Your glucose is normal!");
```

### Various other useful options
```csharp
var settings = await API.GetSettings();

var title = settings.CustomTitle; // The custom title displayed at the top of the site. Defaults to Nightscout

var theme = settings.Theme; // The site's theme - usually default or colors, but other themes are possible
```

# Building
Building this project requires the .NET 5 SDK. The library itself uses .NET Standard 2.0, however the tests use .NET 5.

Nightscout.Net has two release build options: Release and Release-Unity. Make sure to switch to the type you need before building.

To build using Visual Studio, simply open the project in Visual Studio 2017 or higher and click `Build/Build Solution`.

If using CLI, run `dotnet restore` to install the project dependencies, and `dotnet build` to build.

# Tests
Unit tests are currently incomplete and require you to manually substitute the Nightscout site URL in `Nightscout.Net.Tests.TestHelper.cs`. Mock data will be added eventually.

Make sure the site URL is a valid public nightscout site or all tests will fail.

To run tests using Visual Studio, simply click `Tests/Run Tests`.

If using CLI, you can run the tests using `dotnet test`.
