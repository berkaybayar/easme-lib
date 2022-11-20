namespace EasMe
{
    public class EasLogConfiguration
    {


        /// <summary>
        /// Set logs folder path to be stored. Defualt is current directory, adds folder named Logs.
        /// </summary>
        public string LogFolderPath { get; set; } = ".\\Logs";

        /// <summary>
        /// Formatting DateTime in log file name, default value is "MM.dd.yyyy". 
        /// This is added after LogFileName variable.
        /// </summary>
        public string DateFormatString { get; set; } = "MM.dd.yyyy";

        /// <summary>
        /// Set log file name, default value is "Log_". 
        /// This will what value to write before datetime.
        /// </summary>
        public string LogFileName { get; set; } = "Log_";

        /// <summary>
        /// Set log file extension default value is ".json".
        /// </summary>
        public string LogFileExtension { get; set; } = ".json";

        /// <summary>
        /// Whether to enable logging to console, writes json logs in console as well as saving logs to a file. 
        /// Default value is true.
        /// </summary>
        public bool ConsoleAppender { get; set; } = true;
        /// <summary>
        /// Whether to log incoming web request information to log file, default value is false. 
        /// If your app running on server this should be set to true. 
        /// Also you need to Configure EasMe.HttpContext in order to log request data.
        /// </summary>
        public bool WebInfoLogging { get; set; } = false;

        /// <summary>
        /// Whether to enable Trace logging. Default value is false. 
        /// If set true, it will print which class and method log method is called from. 
        /// This is useful for debugging.
        /// </summary>
        public bool TraceLogging { get; set; } = false;
        
        /// <summary>
        /// This will disable logging completely.
        /// </summary>
        public bool DontLog { get; set; } = false;

        /// <summary>
        /// Whether to hide sensitive information from being logged. 
        /// Default value is true. 
        /// If set to false, it will log sensitive information. 
        /// Otherwise it will only print the exception message.
        /// </summary>
        public bool ExceptionHideSensitiveInfo { get; set; } = true;

        /// <summary>
        /// Whether to log debug loggings.
        /// Default value is false. 
        /// If set to true, it will log debug loggings.
        /// </summary>
        public bool IsDebug { get; set; } = false;

        /// <summary>
        /// In order this to work you must configure EasMe.HttpContext and enable web logging. 
        /// This will add request url to log message start.
        /// </summary>
        public bool AddRequestUrlToStart { get; set; } = true;

        /// <summary>
        /// Whether to use default log model and print logs as json. 
        /// Default value is true. If set to false, it will print logs as string. 
        /// If set false set LogFileExtension as well.
        /// </summary>
        public bool IsLogJson { get; set; } = true;

        /// <summary>
        /// Logger will stack this amount of logs in memory and once the amount is reached it will print all logs to file. 
        /// By default this number is 0. Which mean it is disabled and will not cache any log in memory.
        /// Use EasLog.Flush() to save all logs in memory. This must be used before application exists. 
        /// In order to not lose any last second logs.
        /// </summary>
        public int StackLogCount { get; set; } = 0;
    }
}
