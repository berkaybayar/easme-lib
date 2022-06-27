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
        private static EasLogConfiguration? Configuration { get; set; }
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
        //.Replace("..", Directory.GetCurrentDirectory())
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
            CheckConfig();
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
            CheckConfig();
            var model = BaseModelCreate(Severity.ERROR, logMessage, null);
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
            CheckConfig();
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
            CheckConfig();
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
            CheckConfig();
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
            CheckConfig();
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
            CheckConfig();
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
            CheckConfig();
            var model = BaseModelCreate(Severity.DEBUG, logMessage, ex, true);
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
            CheckConfig();
            var model = BaseModelCreate(Severity.WARN, logMessage, null);
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
                    if (size > ConvertConfigFileSize(Configuration.MaxLogFileSize))
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
            try
            {
                var log = new BaseLogModel();
                log.Severity = Severity.ToString();
                log.Log = Log;
                log.LogType = (int)LogType.BASE;
                if (Configuration.DebugMode || ForceDebug)
                {
                    log.TraceAction = GetActionName();
                    log.TraceClass = GetClassName();
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
                    log.Exception = ConvertExceptionToLogModel(Exception, ForceDebug);
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
                log.Headers = Serialize(EasHttpContext.Current.Request.GetHeaderValues());
                return log;
            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.FAILED_TO_CREATE, "Web Log Model creation failed.", e);
            }
            
        }


        /// <summary>
        /// Converts System.Exception model to custom Exception model.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ForceDebug"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        private static ErrorLogModel ConvertExceptionToLogModel(Exception ex, bool ForceDebug = false)
        {

            var model = new ErrorLogModel();
            try
            {
                model.ExceptionMessage = ex.Message;
                if (Configuration.DebugMode || ForceDebug)
                {
                    model.ExceptionSource = ex.Source;
                    model.ExceptionStackTrace = ex.StackTrace;
                    var inner = ex.InnerException;
                    if (inner != null)
                        model.ExceptionInner = inner.ToString();
                }

            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.FAILED_TO_CONVERT, "Failed to convert System.Exception model to Custom Exception model.", e);
            }
            return model;

        }

        /// <summary>
        /// Serializes given object to json string. Uses UnsafeRelaxedJsonEscaping JavaScriptEncoder.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static string Serialize(this object obj)
        {
            try
            {
                var o = new JsonSerializerOptions();
                o.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                return JsonSerializer.Serialize(obj, o);
            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.SERIALIZATION_ERROR, "Exception occured while serializing object.", e);
            }
        }

        /// <summary>
        /// Gets the name of the function this function is called from.
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        private static string GetActionName(int frame = 3)
        {
            var trace = new StackTrace().GetFrame(frame);
            if (trace == null) return "Unkown";
            var method = trace.GetMethod();
            if (method != null) return method.Name;
            return "Unkown";
        }

        /// <summary>
        /// Gets the name of the class this function is called from.
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        private static string GetClassName(int frame = 3)
        {
            var trace = new StackTrace().GetFrame(frame);
            if (trace == null) return "Unkown";
            var method = trace.GetMethod();
            if (method == null) return "Unkown";
            var reflected = method.ReflectedType;
            if (reflected != null) return reflected.Name;
            var declaring = method.DeclaringType;
            if (declaring != null) return declaring.Name;
            return "Unkown";
        }

        private static int ConvertConfigFileSize(string value)
        {
            try
            {
                var size = Convert.ToInt32(value.Split("-")[0].Trim());
                var unit = value.Split("-")[1].ToLower();
                return unit switch
                {
                    "kb" => size * 1024,
                    "mb" => size * 1024 * 1024,
                    "gb" => size * 1024 * 1024 * 1024,
                    _ => size,
                };
            }
            catch (Exception ex)
            {
                throw new EasException(EasMe.Error.FAILED_TO_PARSE, "Failed to parse configuration file size.", ex);
            }
        }

    }
    
}

