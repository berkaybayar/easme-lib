using EasMe.Exceptions;
using EasMe.Models.LogModels;

namespace EasMe
{


    /// <summary>
    /// Simple static json and console logger with useful options.
    /// </summary>
    public static class EasLog
    {
        internal static EasLogConfiguration Configuration { get; set; } = new EasLogConfiguration();
        private static int? _OverSizeExt  = 0;

        private static string ExactLogPath { get; set; } = GetExactLogPath();

        internal static bool _IsCreated = false;
        
        /// <summary>
        /// Initialize the EasLogConfiguration. Call this method in your application startup. If no config is set, it will use default config.
        /// </summary>
        /// <param name="config"></param>
        public static void LoadConfig(EasLogConfiguration? config = null)
        {
            if (_IsCreated) throw new AlreadyLoadedException("EasLog configuration is already loaded.");
            if (config != null)
            {
                Configuration = config;
            }
            ExactLogPath = GetExactLogPath();
            _IsCreated = true;
            Info("EasLogConfiguration loaded. LogPath: " + Configuration.LogFolderPath);
        }
        private static string GetExactLogPath()
        {
            return Configuration.LogFolderPath + "\\" + Configuration.LogFileName + DateTime.Now.ToString(Configuration.DateFormatString) + Configuration.LogFileExtension;
        }

        public static void Write(Severity severity,object log, string? source = null)
        {
            var model = EasLogHelper.LogModelCreate(severity, log, null, false, source);
            WriteLog(model);
        }

        /// <summary>
        /// Creates log with Info severity and success state.
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Info(object log , string?  source = null)
        {
            var model = EasLogHelper.LogModelCreate(Severity.INFO, log, null,false,source);
            WriteLog(model);
        }


        /// <summary>
        /// Creates log with Error severity.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Error(object logMessage, string? source = null)
        {
            var model = EasLogHelper.LogModelCreate(Severity.ERROR, logMessage, null, false, source);
            WriteLog(model);
        }

        /// <summary>
        /// Creates log with Error severity.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Error(Error err , object logMessage, string? source = null)
        {
            var model = EasLogHelper.LogModelCreate(Severity.ERROR, err.ToString() + ": " + logMessage, null, false, source);
            WriteLog(model);
        }


        
        /// <summary>
        /// Creates log with warning severity.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public static void Warn(object logMessage, string? source = null)
        {
            
            var model = EasLogHelper.LogModelCreate(Severity.WARN, logMessage, null, false, source);
            WriteLog(model);
        }

        /// <summary>
        /// Creates log with Exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public static void Exception(Exception ex, string? source = null)
        {
            var model = EasLogHelper.LogModelCreate(Severity.EXCEPTION, ex.Message, ex, false, source);
            WriteLog(model);
            if (Configuration.ThrowException) throw new EasException(EasMe.Error.EXCEPTION, ex.Message, ex);
        }
        
        /// <summary>
        /// Creates log with Exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Exception(object logMessage, Exception ex, string? source = null)
        {
            var model = EasLogHelper.LogModelCreate(Severity.EXCEPTION, logMessage, ex, false, source);
            WriteLog(model);
            if (Configuration.ThrowException) throw new EasException(EasMe.Error.EXCEPTION, ex.Message, ex);
        }


        /// <summary>
        /// Creates log with Fatal severity.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Fatal(object logMessage, string? source = null)
        {
            var model = EasLogHelper.LogModelCreate(Severity.FATAL, logMessage, null, false, source);
            WriteLog(model);
        }
        /// <summary>
        /// Creates log with Fatal severity.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Fatal(object logMessage, Exception ex, string? source = null)
        {
            var model = EasLogHelper.LogModelCreate(Severity.FATAL, logMessage, ex, false, source);
            WriteLog(model);
            if (Configuration.ThrowException) throw new EasException(EasMe.Error.EXCEPTION, ex.Message, ex);
        }
        
        /// <summary>
        /// Creates log with Debug severity.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public static void Debug(object logMessage, string? source = null)
        {
            var model = EasLogHelper.LogModelCreate(Severity.DEBUG, logMessage, null, true, source);
            WriteLog(model);
        }
        
        /// <summary>
        /// Creates log with Debug severity.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public static void Debug(string logMessage, Exception ex, string? source = null)
        {
            var model = EasLogHelper.LogModelCreate(Severity.DEBUG, logMessage, ex, true,  source);
            WriteLog(model);

        }
        /// <summary>
        /// Creates log with given object. If its not string it will log serialized object.
        /// </summary>
        /// <param name="LogContent"></param>
        /// <param name="UseDefaultDate"></param>
        /// <returns>LogContent</returns>
        public static void WriteLog(object obj)
        {
            if (!_IsCreated)
                throw new NotInitializedException("EasLog.Create() must be called before any other method.");
            if (obj == null) throw new EasException(EasMe.Error.NULL_REFERENCE, "Log content is null");
            
            try
            {
                
                var serialized = obj.JsonSerialize();
                if (!Directory.Exists(Configuration.LogFolderPath)) Directory.CreateDirectory(Configuration.LogFolderPath);

                if (File.Exists(ExactLogPath))
                {
                    var size = File.ReadAllBytes(ExactLogPath).Length;
                    if (size > EasLogHelper.ConvertConfigFileSize(Configuration.MaxLogFileSize))
                    {
                        _OverSizeExt++;
                        ExactLogPath = ExactLogPath.Replace(Configuration.LogFileExtension, $"_{_OverSizeExt}{Configuration.LogFileExtension}");
                    }
                }
                File.AppendAllText(ExactLogPath, serialized + "\n");
                if (Configuration.ConsoleAppender)
                    Console.WriteLine(serialized);
            }
            catch (Exception e)
            {
                throw new LoggingFailedException("Exception occured while writing log to log file.", e);
            }

        }


    }
    
}

