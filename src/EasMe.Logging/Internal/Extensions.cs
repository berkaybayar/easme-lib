using EasMe.Result;

namespace EasMe.Logging.Internal;

public static class Extensions
{
    public static EasLogLevel ToEasLogLevel(this ResultSeverity resultSeverity)
    {
        return resultSeverity switch
        {
            ResultSeverity.Info => EasLogLevel.Information,
            ResultSeverity.Warn => EasLogLevel.Warning,
            ResultSeverity.Error => EasLogLevel.Error,
            ResultSeverity.Fatal => EasLogLevel.Fatal,
            _ => EasLogLevel.Off
        };
    }

    internal static EasLogLevel ToEasLogLevel(this string logLevel)
    {
        return logLevel switch
        {
            "Debug" => EasLogLevel.Debug,
            "Information" => EasLogLevel.Information,
            "Warning" => EasLogLevel.Warning,
            "Error" => EasLogLevel.Error,
            "Fatal" => EasLogLevel.Fatal,
            _ => EasLogLevel.Off
        };
    }

    internal static string ToLogString(this object[] param)
    {
        if (param.Length <= 0) return string.Empty;
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