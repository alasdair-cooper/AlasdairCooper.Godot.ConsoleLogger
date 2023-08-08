using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlasdairCooper.Godot.ConsoleLogger;

[ProviderAlias("GodotConsole")]
public sealed class GodotLoggingProvider : ILoggerProvider
{
    private readonly IDisposable? _onChangeToken;
    private GodotLoggerConfiguration _currentConfig;
    private readonly ConcurrentDictionary<string, GodotLogger> _loggers =
        new(StringComparer.OrdinalIgnoreCase);

    public GodotLoggingProvider(IOptionsMonitor<GodotLoggerConfiguration> config)
    {
        _currentConfig = config.CurrentValue;
        _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
    }
    
    public ILogger CreateLogger(string categoryName) =>
        _loggers.GetOrAdd(categoryName, name => new GodotLogger(name, GetCurrentConfig));
    
    private GodotLoggerConfiguration GetCurrentConfig() => _currentConfig;
    
    public void Dispose()
    {
        _loggers.Clear();
        _onChangeToken?.Dispose();
    }
}