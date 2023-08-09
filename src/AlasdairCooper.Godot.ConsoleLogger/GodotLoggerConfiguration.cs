using System.Text.Json.Serialization;
using Godot;
using Microsoft.Extensions.Logging;

namespace AlasdairCooper.Godot.ConsoleLogger;

public class GodotLoggerConfiguration
{
    private static Dictionary<LogLevel, string> DefaultLogLevelColorMap { get; } = new()
    {
        [LogLevel.Trace] = nameof(Colors.White),
        [LogLevel.Debug] = nameof(Colors.White),
        [LogLevel.Information] = nameof(Colors.Blue),
        [LogLevel.Warning] = nameof(Colors.Orange),
        [LogLevel.Error] = nameof(Colors.Red),
        [LogLevel.Critical] = nameof(Colors.Red),
    };

    [JsonInclude] public Dictionary<LogLevel, string> LogLevelColorMap { get; } = DefaultLogLevelColorMap;

    public string GetColorForLogLevel(LogLevel logLevel) =>
        LogLevelColorMap.TryGetValue(logLevel, out var color) ? color : DefaultLogLevelColorMap[logLevel];
}