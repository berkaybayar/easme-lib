namespace EasMe
{
    public class EasLogConfiguration
    {
        /// <summary>
        /// Whether to seperate different log severities to different log files.
        /// </summary>
        public bool SeperateLogs { get; set; } = false;

        /// <summary>
        /// Set logs folder path to be stored. Defualt is current directory, adds folder named Logs.
        /// </summary>
        public string LogFolderPath { get; set; } = Directory.GetCurrentDirectory() + "\\Logs";

        /// <summary>
        /// Max log file size, after this value is being reached creates new log file, default value is "10MB".
        /// </summary>
        public string MaxLogFileSize { get; set; } = "10MB";

        /// <summary>
        /// Whether to enable console logging, writes json logs in console as well as saving logs to a file. Default value is true.
        /// </summary>
        public bool EnableConsoleLogging { get; set; } = true;
        /// <summary>
        /// Formatting datetime in log file name, default value is "MM.dd.yyyy"
        /// </summary>
        public string DateFormatString { get; set; } = "MM.dd.yyyy";

        /// <summary>
        /// Log file name addition to DateFormatString, default value is "Log_".
        /// </summary>
        public string LogFileName { get; set; } = "Log_";

        /// <summary>
        /// Set log file extension default value is ".json".
        /// </summary>
        public string LogFileExtension { get; set; } = ".json";

        /// <summary>
        /// Whether to log current client information to log file, default value is false. This may present sensitive information about client. If your app running on server this should be set to false.
        /// </summary>
        public bool EnableClientInfoLogging { get; set; } = false;

        /// <summary>
        /// Whether to enable debug mode for Exception Error logging. Default value is false. If set false exception logging will only write exception message to log file.
        /// </summary>
        public bool EnableDebugMode { get; set; } = false;
        /// <summary>
        /// Whether to throw Exceptions after it has been logged. 
        /// </summary>
        public bool EnableExceptionThrow { get; set; } = false;

        ///// <summary>
        ///// Whether to print calling function and its class to logs. Defualt value is false.
        ///// </summary>
        //public bool EnableTrace { get; set; } = false; //enable trace for debug logging also only log exception full in debug logging for normal only log message
        public Dictionary<int, string> CustomErrorList { get; set; } = new Dictionary<int, string>();
    }
}
