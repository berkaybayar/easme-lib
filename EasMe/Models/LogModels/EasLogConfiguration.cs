namespace EasMe
{
    public class EasLogConfiguration
    {


        /// <summary>
        /// Set logs folder path to be stored. Defualt is current directory, adds folder named Logs.
        /// </summary>
        public string LogFolderPath { get; set; } =  ".\\Logs";

        /// <summary>
        /// Max log file size, after this value is being reached creates new log file, default value is "10-MB".
        /// </summary>
        public string MaxLogFileSize { get; set; } = "10-MB";

        /// <summary>
        /// Whether to enable console logging, writes json logs in console as well as saving logs to a file. Default value is true.
        /// </summary>
        public bool ConsoleLogging { get; set; } = true;
        
        /// <summary>
        /// Formatting DateTime in log file name, default value is "MM.dd.yyyy". This is added after LogFileName variable.
        /// </summary>
        public string DateFormatString { get; set; } = "MM.dd.yyyy";

        /// <summary>
        /// Set log file name, default value is "Log_".
        /// </summary>
        public string LogFileName { get; set; } = "Log_";

        /// <summary>
        /// Set log file extension default value is ".json".
        /// </summary>
        public string LogFileExtension { get; set; } = ".json";

        /// <summary>
        /// Whether to log current client information to log file, default value is false. If your app running on server this should be set to false.
        /// </summary>
        public bool ClientInfoLogging { get; set; } = false;

        /// <summary>
        /// Whether to log incoming web request information to log file, default value is false. If your app running on server this should be set to true. Also you need to Configure EasMe.HttpContext in order to log request data.
        /// </summary>
        public bool WebInfoLogging { get; set; } = false;
        
        /// <summary>
        /// Whether to enable debug mode for Exception Error logging. Default value is false. If set false exception logging will only write exception message to log file.
        /// </summary>
        public bool DebugMode { get; set; } = false;
        
        /// <summary>
        /// Whether to throw Exceptions after it has been logged. 
        /// </summary>
        public bool ThrowException { get; set; } = false;

        
    }
}
