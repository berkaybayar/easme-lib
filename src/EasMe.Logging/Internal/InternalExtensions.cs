using EasMe.Result;
using Microsoft.Extensions.Logging;

namespace EasMe.Logging.Internal;

internal static class InternalExtensions
{
    internal static LogLevel ToLogLevel(this ResultSeverity resultSeverity)
    {
        switch (resultSeverity)
        {
            case ResultSeverity.Info:
                return LogLevel.Information;
            case ResultSeverity.Warn:
                return LogLevel.Warning;
            case ResultSeverity.Error:
                return LogLevel.Error;
            case ResultSeverity.Fatal:
                return LogLevel.Critical;
            default:
                return LogLevel.None;
        }
    }

    internal static LogLevel ToLogLevel(this string logLevel)
    {
        switch (logLevel)
        {
            case "Debug":
                return LogLevel.Debug;
            case "Information":
                return LogLevel.Information;
            case "Warning":
                return LogLevel.Warning;
            case "Error":
                return LogLevel.Error;
            case "Critical":
                return LogLevel.Critical;
            default:
                return LogLevel.None;
        }
    }

    internal static string ToLogString(this object[] param)
    {
        if (param.Length > 0)
        {
            var last = param.Last();
            var paramStr = "";
            foreach (var item in param)
            {
                if (item is null) continue;
                if (last != item)
                    paramStr += " [" + item + "]";
                else paramStr += " " + item;
            }

            return paramStr.Trim();
        }

        return string.Empty;
    }

    internal static string ToLogString(this object[] param, LogLevel logLevel)
    {
        var paramStr = $"[{logLevel.ToString().ToUpper()}] [{DateTime.Now:MM/dd/yyyy HH:mm:ss}]";
        foreach (var item in param) paramStr += " [" + item + "]";
        return paramStr;
    }
}