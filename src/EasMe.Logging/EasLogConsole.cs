using EasMe.Logging.Internal;


namespace EasMe.Logging;

/// <summary>
///     Basic console logger for heavy api request logging.
///     File logging with heavy api request sometime creating errors.
///     With this you only log to console but if error happens you log with EasLog or some other library to a file.
/// </summary>
public static class EasLogConsole
{
    private const ConsoleColor FatalColor = ConsoleColor.Magenta;
    private const ConsoleColor ErrorColor = ConsoleColor.Red;
    private const ConsoleColor BaseColor = ConsoleColor.White;
    private const ConsoleColor WarningColor = ConsoleColor.Yellow;
    private const ConsoleColor InfoColor = ConsoleColor.Green;
    private const ConsoleColor DebugColor = ConsoleColor.Blue;
    private const ConsoleColor TraceColor = ConsoleColor.Cyan;

    public static void Log(string message)
    {
        Console.WriteLine(message);
    }

    /// <summary>
    ///     Writes log to console with given color. Creates new line.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    public static void Log(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void Log(string message, ConsoleColor color, bool newLine)
    {
        Console.ForegroundColor = color;
        if (newLine)
            Console.WriteLine(message);
        else
            Console.Write(message);
        Console.ResetColor();
    }

    public static void Log(EasLogLevel level, string message)
    {
        switch (level)
        {
            case EasLogLevel.Error:
                Log(message, ErrorColor);
                break;
            case EasLogLevel.Warning:
                Log(message, WarningColor);
                break;
            case EasLogLevel.Information:
                Log(message, InfoColor);
                break;
            case EasLogLevel.Debug:
                Log(message, DebugColor);
                break;
            case EasLogLevel.Trace:
                Log(message, TraceColor);
                break;
            case EasLogLevel.Fatal:
                Log(message, FatalColor);
                break;
            default:
                Log(message, BaseColor);
                break;
        }
    }

    public static void Log(EasLogLevel severity, string message, params object[] param)
    {
        var logStr = param.ToLogString(severity) + " " + message;
        switch (severity)
        {
            case EasLogLevel.Error:
                Log(logStr, ErrorColor);
                break;
            case EasLogLevel.Warning:
                Log(logStr, WarningColor);
                break;
            case EasLogLevel.Information:
                Log(logStr, InfoColor);
                break;
            case EasLogLevel.Trace:
                Log(logStr, TraceColor);
                break;
            case EasLogLevel.Debug:
                Log(logStr, DebugColor);
                break;
            case EasLogLevel.Fatal:
                Log(logStr, FatalColor);
                break;
            default:
                Log(logStr, BaseColor);
                break;
        }
    }

    public static void Error(string message, params string[] param)
    {
        Log(EasLogLevel.Error, message, param);
    }

    public static void Fatal(string message, params string[] param)
    {
        Log(EasLogLevel.Fatal, message, param);
    }

    public static void Warn(string message, params string[] param)
    {
        Log(EasLogLevel.Warning, message, param);
    }

    public static void Info(string message, params string[] param)
    {
        Log(EasLogLevel.Information, message, param);
    }

    public static void Debug(string message, params string[] param)
    {
        Log(EasLogLevel.Debug, message, param);
    }

    public static void Trace(string message, params string[] param)
    {
        Log(EasLogLevel.Trace, message, param);
    }
}