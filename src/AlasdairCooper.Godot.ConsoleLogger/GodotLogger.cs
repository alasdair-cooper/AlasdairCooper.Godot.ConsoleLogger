using Microsoft.Extensions.Logging;
using Godot;

namespace AlasdairCooper.Godot.ConsoleLogger;

public sealed class GodotLogger : ILogger
{
    private readonly string _name;
    private readonly Func<GodotLoggerConfiguration> _getCurrentConfig;

    public GodotLogger(string name, Func<GodotLoggerConfiguration> getCurrentConfig) =>
        (_name, _getCurrentConfig) = (name, getCurrentConfig);

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        var message = $"[{_name}] {formatter(state, exception)}";

        Action log = logLevel switch
        {
            LogLevel.Trace or LogLevel.Debug or LogLevel.Information => () => GD.Print(message),
            LogLevel.Warning => () => GD.PushWarning(message),
            LogLevel.Error or LogLevel.Critical => () => GD.PushError(message),
            _ => () => { },
        };

        log();
    }

    public bool IsEnabled(LogLevel logLevel) => logLevel >= _getCurrentConfig().LogLevel;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;
}