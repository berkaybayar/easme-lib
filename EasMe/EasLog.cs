using EasMe.Exceptions;
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
        internal static EasLogConfiguration Configuration { get; set; } = new EasLogConfiguration();
        private static int? _OverSizeExt  = 0;

        private static string ExactLogPath { get; set; } = GetExactLogPath();

        private static bool _IsCreated = false;
        
        /// <summary>
        /// Initialize the EasLogConfiguration. Call this method in your application startup. If no config is set, it will use default config.
        /// </summary>
        /// <param name="config"></param>
        public static void LoadConfig(EasLogConfiguration? config = null)
        {
            if(_IsCreated) 
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

        public static void Write(object log, string? source = null)
        {
            var model = BaseModelCreate(Severity.BASE, log, null, false, source);
            CreateLog(model);
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
            var model = BaseModelCreate(Severity.INFO, log, null,false,source);
            CreateLog(model);
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
            var model = BaseModelCreate(Severity.ERROR, logMessage, null, false, source);
            CreateLog(model);
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
            var model = BaseModelCreate(Severity.ERROR, err.ToString() + ": " + logMessage, null, false, source);
            CreateLog(model);
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
            
            var model = BaseModelCreate(Severity.WARN, logMessage, null, false, source);
            CreateLog(model);
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
            var model = BaseModelCreate(Severity.EXCEPTION, ex.Message, ex, false, source);
            CreateLog(model);
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
            var model = BaseModelCreate(Severity.EXCEPTION, logMessage, ex, false, source);
            CreateLog(model);
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
            var model = BaseModelCreate(Severity.FATAL, logMessage, null, false, source);
            CreateLog(model);
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
            var model = BaseModelCreate(Severity.FATAL, logMessage, ex, false, source);
            CreateLog(model);
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
            var model = BaseModelCreate(Severity.DEBUG, logMessage, null, true, source);
            CreateLog(model);
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
            var model = BaseModelCreate(Severity.DEBUG, logMessage, ex, true,  source);
            CreateLog(model);

        }
        
        /// <summary>
        /// Creates log with given object. If its not string it will log serialized object.
        /// </summary>
        /// <param name="LogContent"></param>
        /// <param name="UseDefaultDate"></param>
        /// <returns>LogContent</returns>
        public static void CreateLog(object obj)
        {
            if (!_IsCreated) 
                throw new NotInitializedException("EasLog.Create() must be called before any other method.");
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
                        _OverSizeExt++;
                        ExactLogPath = ExactLogPath.Replace(Configuration.LogFileExtension, $"_{_OverSizeExt}{Configuration.LogFileExtension}");
                    }
                }
                File.AppendAllText(ExactLogPath, serialized + "\n");
                if (Configuration.ConsoleLogging)
                    Console.WriteLine(serialized);
            }
            catch (Exception e)
            {
                throw new LoggingFailedException("Exception occured while writing log to log file.", e);
            }

        }

        /// <summary>
        /// Converts given parameters to BaseLogModel.
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <returns>EasMe.Models.BaseLogModel</returns>
        private static BaseLogModel BaseModelCreate(Severity Severity, object Log, Exception? Exception = null, bool ForceDebug = false, string? source = null)
        {
            try
            {
                var log = new BaseLogModel();
                log.Severity = Severity.ToString();
                log.Log = Log;
                log.LogType = (int)LogType.BASE;
                log.LogSource = source;
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
                throw new FailedToCreateException("Base Log Model creation failed.", e);
            }
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
                throw new FailedToCreateException("Web Log Model creation failed.", e);
            }
            
        }
    }
    
}

