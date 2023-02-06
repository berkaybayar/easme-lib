using EasMe.Enums;
using EasMe.Extensions;
using EasMe.Logging.Internal;

namespace EasMe.Logging
{
    /// <summary>
    /// Basic console logger for heavy api request logging. 
    /// File logging with heavy api request sometime creating errors. 
    /// With this you only log to console but if error happens you log with EasLog or some other library to a file.
    /// </summary>
    public static class EasLogConsole
    {
        private static ConsoleColor FatalColor = ConsoleColor.DarkMagenta;
        private static ConsoleColor ErrorColor = ConsoleColor.Red;
        private static ConsoleColor BaseColor = ConsoleColor.White;
        private static ConsoleColor WarningColor = ConsoleColor.Yellow;
        private static ConsoleColor InfoColor = ConsoleColor.Green;
        private static ConsoleColor DebugColor = ConsoleColor.Blue;
        private static ConsoleColor TraceColor = ConsoleColor.Cyan;
        private static ConsoleColor ExceptionColor = ConsoleColor.Magenta;
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }
        /// <summary>
        /// Writes log to console with given color. Creates new line.
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
            {
                Console.WriteLine(message);
            }
            else
            {
                Console.Write(message);
            }
            Console.ResetColor();
        }
        public static void Log(LogSeverity severity, string message)
        {
            switch (severity)
            {
                case LogSeverity.ERROR:
                    Log(message, ErrorColor);
                    break;
                case LogSeverity.WARN:
                    Log(message, WarningColor);
                    break;
                case LogSeverity.INFO:
                    Log(message, InfoColor);
                    break;
                case LogSeverity.DEBUG:
                    Log(message, DebugColor);
                    break;
                case LogSeverity.TRACE:
                    Log(message, TraceColor);
                    break;
                case LogSeverity.FATAL:
                    Log(message, FatalColor);
                    break;
                case LogSeverity.EXCEPTION:
                    Log(message, ExceptionColor);
                    break;
                default:
                    Log(message, BaseColor);
                    break;
            }
        }
        public static void Log(LogSeverity severity, string message, params string[] param)
        {
            var paramStr = param.ToLogString(severity);
            switch (severity)
            {
                case LogSeverity.ERROR:
                    Log(paramStr + " " + message, ErrorColor);
                    break;
                case LogSeverity.WARN:
                    Log(paramStr + " " + message, WarningColor);
                    break;
                case LogSeverity.INFO:
                    Log(paramStr + " " + message, InfoColor);
                    break;
                default:
                    Log(paramStr + " " + message, BaseColor);
                    break;
            }
        }

        public static void Error(string message, params string[] param)
        {
            Log(LogSeverity.ERROR, message, param);
        }
        public static void Fatal(string message, params string[] param)
        {
            Log(LogSeverity.FATAL, message, param);
        }
        public static void Warn(string message, params string[] param)
        {
            Log(LogSeverity.WARN, message, param);
        }
        public static void Info(string message, params string[] param)
        {
            Log(LogSeverity.INFO, message, param);
        }
        public static void Debug(string message, params string[] param)
        {
            Log(LogSeverity.DEBUG, message, param);
        }
        public static void Trace(string message, params string[] param)
        {
            Log(LogSeverity.TRACE, message, param);
        }
    }
}
