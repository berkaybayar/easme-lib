using EasMe.Models.LogModels;
using System.Diagnostics;
using System.Text.Json;

namespace EasMe
{


    /// <summary>
    /// Simple logging helper with few useful options.
    /// </summary>
    public static class EasLog
    {
        internal static EasLogConfiguration? Configuration { get; set; }
        private static int? OverSizeExt { get; set; } = 0;

        private static string ExactLogPath { get; set; } = "";

        /// <summary>
        /// Initialize the EasLogConfiguration. Call this method in your application startup.
        /// </summary>
        /// <param name="config"></param>
        public static void LoadConfiguration(EasLogConfiguration config)
        {
            Configuration = config;
            ExactLogPath = Configuration.LogFolderPath + "\\" + Configuration.LogFileName + DateTime.Now.ToString(Configuration.DateFormatString) + Configuration.LogFileExtension;
            Info("EasLogConfiguration loaded. LogPath: " + Configuration.LogFolderPath);
        }
        /// <summary>
        /// Initialize default EasLogConfiguration. Call this method in your application startup.
        /// </summary>
        public static void LoadConfigurationDefault()
        {
            Configuration = new EasLogConfiguration();
            ExactLogPath = Configuration.LogFolderPath + "\\" + Configuration.LogFileName + DateTime.Now.ToString(Configuration.DateFormatString) + Configuration.LogFileExtension;
            Info("Default EasLogConfiguration loaded. LogPath: " + ExactLogPath);
        }


        public static void CheckConfig()
        {
            if (Configuration == null) throw new EasException(EasMe.Error.NOT_LOADED, "EasLogConfiguration not loaded, call LoadConfiguration() or LoadConfigurationDefault() in your application startup.");
            if (string.IsNullOrEmpty(ExactLogPath)) throw new EasException(EasMe.Error.NULL_REFERENCE, "EasLog configuration error, exact log file path not loaded.");
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

        public static void Info(object log)
        {
            var model = BaseModelCreate(Severity.INFO, log, null);
            Log(model);
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

        public static void Error(object logMessage)
        {
            var model = BaseModelCreate(Severity.ERROR, logMessage, null);
            Log(model);
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

        public static void Error(Error err , object logMessage)
        {
            var model = BaseModelCreate(Severity.ERROR, err.ToString() + ": " + logMessage, null);
            Log(model);
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
        public static void Warn(object logMessage)
        {
            
            var model = BaseModelCreate(Severity.WARN, logMessage, null);
            Log(model);
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
        public static void Exception(Exception ex)
        {
            var model = BaseModelCreate(Severity.EXCEPTION, ex.Message, ex);
            Log(model);
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

        public static void Exception(object logMessage, Exception ex)
        {
            var model = BaseModelCreate(Severity.EXCEPTION, logMessage, ex);
            Log(model);
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

        public static void Fatal(object logMessage)
        {
            var model = BaseModelCreate(Severity.FATAL, logMessage, null);
            Log(model);
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

        public static void Fatal(object logMessage, Exception ex)
        {
            var model = BaseModelCreate(Severity.FATAL, logMessage, ex);
            Log(model);
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

        public static void Debug(object logMessage)
        {
            var model = BaseModelCreate(Severity.DEBUG, logMessage, null, true);
            Log(model);
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

        public static void Debug(string logMessage, Exception ex)
        {
            var model = BaseModelCreate(Severity.DEBUG, logMessage, ex, true);
            Log(model);

        }
        /// <summary>
        /// Creates log with given object. If its not string it will log serialized object.
        /// </summary>
        /// <param name="LogContent"></param>
        /// <param name="UseDefaultDate"></param>
        /// <returns>LogContent</returns>
        public static void Log(object obj)
        {
            CheckConfig();
            string serialized;
            try
            {
                if (obj == null) throw new EasException(EasMe.Error.NULL_REFERENCE, "Log content is null");
                //possibly this check not needed
                serialized = obj.Serialize();
                if (!Directory.Exists(Configuration.LogFolderPath)) Directory.CreateDirectory(Configuration.LogFolderPath);

                if (File.Exists(ExactLogPath))
                {
                    var size = File.ReadAllBytes(ExactLogPath).Length;
                    if (size > EasLogHelper.ConvertConfigFileSize(Configuration.MaxLogFileSize))
                    {
                        OverSizeExt++;
                        ExactLogPath = ExactLogPath.Replace(Configuration.LogFileExtension, $"_{OverSizeExt}{Configuration.LogFileExtension}");
                    }
                }
                File.AppendAllText(ExactLogPath, serialized + "\n");
                if (Configuration.ConsoleLogging)
                    Console.WriteLine(serialized);
            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.LOGGING_ERROR, "Exception occured while writing log to log file.", e);
            }

        }

        /// <summary>
        /// Converts given parameters to BaseLogModel.
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <returns>EasMe.Models.BaseLogModel</returns>
        private static BaseLogModel BaseModelCreate(Severity Severity, object Log, Exception? Exception = null, bool ForceDebug = false)
        {
            CheckConfig();
            try
            {
                var log = new BaseLogModel();
                log.Severity = Severity.ToString();
                log.Log = Log;
                log.LogType = (int)LogType.BASE;
                if (Configuration.DebugMode || ForceDebug || Configuration.TraceLogging)
                {
                    log.TraceAction = EasLogHelper.GetActionName();
                    log.TraceClass = EasLogHelper.GetClassName();
                }
                if (Configuration.ClientInfoLogging)
                {
                    log.ClientLog = new ClientLogModel();
                    log.LogType = (int)LogType.CLIENT;
                }
                if (Configuration.WebInfoLogging)
                {
                    log.WebLog = WebModelCreate();
                    log.LogType = (int)LogType.WEB;                    
                }
                if (Exception != null)
                {
                    log.Exception = EasLogHelper.ConvertExceptionToLogModel(Exception, ForceDebug);
                    log.LogType = (int)LogType.EXCEPTION;
                }
                return log;
            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.FAILED_TO_CREATE, "Base Log Model creation failed.", e);
            };
        }
        /// <summary>
        /// Converts given parameters to WebLogModel.
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <param name="ex"></param>
        /// <returns>EasMe.Models.WebLogModel</returns>
        private static WebLogModel? WebModelCreate()
        {
            if (EasHttpContext.Current == null)
            {
                return null;
            }
            try
            {
                
                var log = new WebLogModel();
                log.Ip = EasHttpContext.Current.Request.GetRemoteIpAddress();
                log.HttpMethod = EasHttpContext.Current.Request.Method;
                log.RequestUrl = EasHttpContext.Current.Request.GetRequestQuery();
                log.Headers = EasHttpContext.Current.Request.GetHeaderValues().Serialize();
                return log;
            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.FAILED_TO_CREATE, "Web Log Model creation failed.", e);
            }
            
        }
    }
    
}

