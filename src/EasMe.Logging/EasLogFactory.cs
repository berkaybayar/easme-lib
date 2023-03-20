using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace EasMe.Logging;

public static class EasLogFactory
{
    public static readonly IEasLog StaticLogger = CreateLogger();
    private static bool _isConfigured;

    internal static EasLogConfiguration Config { get; set; } = new();

    public static IEasLog CreateLogger()
    {
        var methodInfo = new StackTrace().GetFrame(1)?.GetMethod();
        var className = methodInfo?.ReflectedType?.FullName;
        return new EasLog(className ?? "Sys");
    }

    public static IEasLog CreateLogger(string name)
    {
        return new EasLog(name);
    }

    /// <summary>
    ///     EasLog logging configuration. Call this method in your startup. If you don't call it it will use default values.
    /// </summary>
    /// <param name="action"></param>
    public static void Configure(Action<EasLogConfiguration> action)
    {
        if (_isConfigured) throw new InvalidOperationException("EasLog configuration already loaded.");
        var config = new EasLogConfiguration();
        action(config);
        Config = config;
        _isConfigured = true;
    }


    public static void ConfigureTraceDefault(bool isWeb)
    {
        if (_isConfigured) throw new InvalidOperationException("EasLog configuration already loaded.");
        Config = new EasLogConfiguration
        {
            ConsoleAppender = true,
            ExceptionHideSensitiveInfo = false,
            LogFileName = "Trace_",
            MinimumLogLevel = LogLevel.Trace,
            WebInfoLogging = isWeb,
            TraceLogging = true,
            SeparateLogLevelToFolder = false
        };
        _isConfigured = true;
    }
}