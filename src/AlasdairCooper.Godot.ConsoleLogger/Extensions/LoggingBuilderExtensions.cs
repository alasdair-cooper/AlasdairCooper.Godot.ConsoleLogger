using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace AlasdairCooper.Godot.ConsoleLogger.Extensions;

public static class LoggingBuilderExtensions
{
    // ReSharper disable once MemberCanBePrivate.Global
    public static ILoggingBuilder AddGodot(this ILoggingBuilder builder)
    {
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, GodotLoggingProvider>());
        LoggerProviderOptions.RegisterProviderOptions<GodotLoggerConfiguration, GodotLoggingProvider>(builder.Services);
        
        return builder;
    } 
    
    public static ILoggingBuilder AddGodot(this ILoggingBuilder builder, Action<GodotLoggerConfiguration> configure)
    {
        builder.AddGodot();
        builder.Services.Configure(configure);
        
        return builder;
    } 
}