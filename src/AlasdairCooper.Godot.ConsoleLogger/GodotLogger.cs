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

        var config = _getCurrentConfig();

        var exceptionString = exception is not null ? $" - {exception}" : string.Empty;
        
        GD.PrintRich($"[color={config.GetColorForLogLevel(logLevel).ToLower()}] [b][{_name}][/b] {formatter(state, exception)}{exceptionString}[/color]");

        var message = $"[{_name}] {formatter(state, exception)}";

        if (exception is not null) message += exceptionString;
        
        switch (logLevel)
        {
            case LogLevel.Error or LogLevel.Critical:
                GD.PushError(message);
                break;
            case LogLevel.Warning:
                GD.PushWarning(message);
                break;
        }
    }

    public bool IsEnabled(LogLevel logLevel) => logLevel is not LogLevel.None;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;
}