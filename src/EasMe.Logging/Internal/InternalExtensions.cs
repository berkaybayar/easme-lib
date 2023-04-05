using EasMe.Result;

namespace EasMe.Logging.Internal;

internal static class InternalExtensions
{
    internal static EasLogLevel ToEasLogLevel(this ResultSeverity resultSeverity)
    {
        switch (resultSeverity)
        {
            case ResultSeverity.Info:
                return EasLogLevel.Information;
            case ResultSeverity.Warn:
                return EasLogLevel.Warning;
            case ResultSeverity.Error:
                return EasLogLevel.Error;
            case ResultSeverity.Fatal:
                return EasLogLevel.Fatal;
            default:
                return EasLogLevel.Off;
        }
    }

    internal static EasLogLevel ToEasLogLevel(this string logLevel)
    {
        switch (logLevel)
        {
            case "Debug":
                return EasLogLevel.Debug;
            case "Information":
                return EasLogLevel.Information;
            case "Warning":
                return EasLogLevel.Warning;
            case "Error":
                return EasLogLevel.Error;
            case "Fatal":
                return EasLogLevel.Fatal;
            default:
                return EasLogLevel.Off;
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

    internal static string ToLogString(this object[] param, EasLogLevel logLevel)
    {
        var paramStr = $"[{logLevel.ToString().ToUpper()}] [{DateTime.Now:MM/dd/yyyy HH:mm:ss}]";
        foreach (var item in param) paramStr += " [" + item + "]";
        return paramStr;
    }

    internal static bool IsLoggable(this EasLogLevel severity)
    {
        var list = EasLogHelper.GetLoggableLevels();
        return list.Contains(severity);
    }
}