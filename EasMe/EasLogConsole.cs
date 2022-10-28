namespace EasMe
{
    /// <summary>
    /// Basic console logger for heavy api request logging. 
    /// File logging with heavy api request sometime creating errors. 
    /// With this you only log to console but if error happens you log with EasLog or some other library to a file.
    /// </summary>
    public static class EasLogConsole
    {
        private static ConsoleColor FatalColor = ConsoleColor.Magenta;
        private static ConsoleColor ErrorColor = ConsoleColor.Red;
        private static ConsoleColor BaseColor = ConsoleColor.White;
        private static ConsoleColor WarningColor = ConsoleColor.Yellow;
        private static ConsoleColor InfoColor = ConsoleColor.Green;
        private static ConsoleColor DebugColor = ConsoleColor.Blue;
        private static ConsoleColor TraceColor = ConsoleColor.Cyan;
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
        public static void Log(Severity severity, string message)
        {
            switch (severity)
            {
                case Severity.ERROR:
                    Log(message, ErrorColor);
                    break;
                case Severity.WARN:
                    Log(message, WarningColor);
                    break;
                case Severity.INFO:
                    Log(message, InfoColor);
                    break;
                case Severity.DEBUG:
                    Log(message, DebugColor);
                    break;
                case Severity.TRACE:
                    Log(message, TraceColor);
                    break;
                case Severity.FATAL:
                    Log(message, FatalColor);
                    break;
                default:
                    Log(message, BaseColor);
                    break;
            }
        }
        public static void Log(Severity severity, string message, params string[] param)
        {
            var paramStr = param.ToLogString(severity);
            switch (severity)
            {
                case Severity.ERROR:
                    Log(paramStr, ErrorColor);
                    break;
                case Severity.WARN:
                    Log(paramStr, WarningColor);
                    break;
                case Severity.INFO:
                    Log(paramStr, InfoColor);
                    break;
                default:
                    Log(paramStr, BaseColor);
                    break;
            }
        }
        private static string ToLogString(this string[] param)
        {
            var paramStr = "";
            foreach (var item in param)
            {
                paramStr += " [" + item + "]";
            }
            return paramStr;
        }
        private static string ToLogString(this string[] param,Severity severity)
        {
            var paramStr = $"[{severity.ToString().ToUpper()}] [{DateTime.Now:MM/dd/yyyy HH:mm:ss}]";
            foreach (var item in param)
            {
                paramStr += " [" + item + "]";
            }
            return paramStr;
        }
        public static void Error(string message, params string[] param)
        {
            Log(Severity.ERROR, message, param);
        }
        public static void Fatal(string message, params string[] param)
        {
            Log(Severity.FATAL, message, param);
        }
        public static void Warn(string message, params string[] param)
        {
            Log(Severity.WARN, message, param);
        }
        public static void Info(string message, params string[] param)
        {
            Log(Severity.INFO, message, param);
        }
        public static void Debug(string message, params string[] param)
        {
            Log(Severity.DEBUG, message, param);
        }
        public static void Trace(string message, params string[] param)
        {
            Log(Severity.TRACE, message, param);
        }
    }
}
