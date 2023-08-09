![Nuget](https://img.shields.io/nuget/v/AlasdairCooper.Godot.ConsoleLogger?color=eebb00&style=for-the-badge) ![Nuget](https://img.shields.io/nuget/dt/AlasdairCooper.Godot.ConsoleLogger?color=0033ee&style=for-the-badge)

# AlasdairCooper.Godot.ConsoleLogger

This is a basic logging provider for the Godot console compatible with `Microsoft.Extensions.Logging`.

Install [the NuGet package](https://www.nuget.org/packages/AlasdairCooper.Godot.ConsoleLogger).

## Setup

```csharp
# Program.cs

...
using AlasdairCooper.Godot.ConsoleLogger.Extensions;
...
builder.Services.AddLogging(c => c.AddGodot());
```

## Configuration

You can configure the log levels of individual categories if you configure with an `appsettings.json` like so:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    },
    "GodotConsole": {
      "LogLevel": {
        "Default": "Error",
        "Lorem.Ipsum": "Debug"
      }
    }
  }
}
```
