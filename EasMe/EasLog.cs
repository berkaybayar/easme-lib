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
            Info("EasLogConfiguration loaded.");
        }
        
        /// <summary>
        /// Initialize default EasLogConfiguration. Call this method in your application startup.
        /// </summary>
        public static void LoadConfigurationDefault()
        {
            Configuration = new EasLogConfiguration();
            ExactLogPath = Configuration.LogFolderPath + "\\" + Configuration.LogFileName + DateTime.Now.ToString(Configuration.DateFormatString) + Configuration.LogFileExtension;
            Info("Default EasLogConfiguration loaded.");
        }
        public static void CheckConfig()
        {
            if (Configuration == null) throw new EasException(EasMe.Error.NOT_LOADED, "EasLogConfiguration not loaded, call LoadConfiguration() or LoadConfigurationDefault() in your application startup.");            
            if (string.IsNullOrEmpty(ExactLogPath)) throw new EasException(EasMe.Error.NULL_REFERENCE, "EasLog configuration error, exact log file path not loaded.");
        }

        /// <summary>
        /// Creates log with Info severity.
        /// </summary>
        /// <param name="LogMessage"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Info(object LogMessage, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            CheckConfig();
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.INFO, LogMessage, EasMe.Error.SUCCESS, null, webModel);
            Log(model);
        }


        /// <summary>
        /// Creates log with Error severity.
        /// </summary>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Error(object LogMessage, Error ErrorNo = EasMe.Error.ERROR, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            CheckConfig();
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.ERROR, LogMessage, ErrorNo, null, webModel);
            Log(model);
        }


        /// <summary>
        /// Creates log with Exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Error(Exception ex, Error ErrorNo = EasMe.Error.EXCEPTION, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            CheckConfig();
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.EXCEPTION, ErrorNo.ToString(), ErrorNo, ex, webModel);
            Log(model);
            if (Configuration.EnableExceptionThrow) throw new EasException(EasMe.Error.EXCEPTION, ex.Message,ex);
        }

        /// <summary>
        /// Creates log with Exception severity.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Error(string LogMessage,Exception ex, Error ErrorNo = EasMe.Error.EXCEPTION, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            CheckConfig();
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.EXCEPTION,LogMessage, ErrorNo, ex, webModel);
            Log(model);
            if (Configuration.EnableExceptionThrow) throw new EasException(EasMe.Error.EXCEPTION, ex.Message, ex);
        }
        /// <summary>
        /// Creates log with Fatal severity.
        /// </summary>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Fatal(object LogMessage, Error ErrorNo = EasMe.Error.FATAL, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            CheckConfig();
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.FATAL, LogMessage, ErrorNo, null, webModel);
            Log(model);
        }
        /// <summary>
        /// Creates log with Fatal severity.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Fatal(string LogMessage, Exception ex, Error ErrorNo = EasMe.Error.FATAL, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            CheckConfig();
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.FATAL, LogMessage, ErrorNo, ex, webModel);
            Log(model);
            if (Configuration.EnableExceptionThrow) throw new EasException(EasMe.Error.EXCEPTION, ex.Message, ex);
        }
        /// <summary>
        /// Creates log with Debug severity.
        /// </summary>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        
        public static void Debug(object LogMessage, Error ErrorNo = EasMe.Error.DEBUG, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            CheckConfig();
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.DEBUG, LogMessage, ErrorNo, null, webModel,true);
             Log(model);
        }
        /// <summary>
        /// Creates log with Debug severity.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static void Debug(string LogMessage, Exception ex, Error ErrorNo = EasMe.Error.DEBUG, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            CheckConfig();
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.DEBUG, LogMessage, ErrorNo, ex, webModel, true);
            Log(model);

        }
        /// <summary>
        /// Creates log with warning severity.
        /// </summary>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public static void Warn(object LogMessage, Error ErrorNo = EasMe.Error.WARN, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            CheckConfig();
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.WARN, LogMessage, ErrorNo, null, webModel);
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
                if (Configuration.EnableConsoleLogging)
                    Console.WriteLine(serialized);
            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.LOGGING_ERROR,"Exception occured while writing log to log file.", e);
            }
            
           

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
                throw new EasException(EasMe.Error.SERIALIZATION_ERROR,"Exception occured while serializing object.", e);
            }
        }

        /// <summary>
        /// Converts given parameters to WebLogModel.
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <param name="ex"></param>
        /// <returns>EasMe.Models.WebLogModel</returns>
        private static WebLogModel? WebModelCreate(string? Ip, string? HttpMethod, string? RequestUrl, Dictionary<string, string>? Headers)
        {
            var log = new WebLogModel();
            try
            {
                if (string.IsNullOrEmpty(Ip) && string.IsNullOrEmpty(HttpMethod) && string.IsNullOrEmpty(RequestUrl) && Headers == null)
                {
                    return null;
                }
                log.Ip = Ip;
                log.HttpMethod = HttpMethod;
                log.RequestUrl = RequestUrl;
                log.Headers = EasProxy.ConvertHeadersToString(Headers);
            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.FAILED_TO_CREATE,"Web Log Model creation failed." ,e);
            }
            return log;
        }

        /// <summary>
        /// Converts given parameters to BaseLogModel.
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <returns>EasMe.Models.BaseLogModel</returns>
        private static BaseLogModel BaseModelCreate(Severity Severity, object Log, Error ErrorNo, Exception? Exception = null, WebLogModel? WebLog = null,bool ForceDebug = false)
        {
            try
            {
                var log = new BaseLogModel();
                log.Severity = Severity.ToString();
                log.Message = Log;
                log.LogType = (int)LogType.BASE;
                log.ErrorNo = ErrorNo.ToString();
                if (Configuration.EnableDebugMode || ForceDebug)
                {
                    log.TraceAction = GetActionName();
                    log.TraceClass = GetClassName();
                }
                if (Configuration.EnableClientInfoLogging)
                {
                    log.ClientLog = new ClientLogModel();
                    log.LogType = (int)LogType.CLIENT;
                }
                if (WebLog != null)
                {
                    log.WebLog = WebLog;
                    log.LogType = (int)LogType.WEB;
                }
                if (Exception != null)
                {
                    log.Exception = ConvertExceptionToLogModel(Exception,ForceDebug);
                    log.LogType = (int)LogType.EXCEPTION;
                }
                return log;
            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.FAILED_TO_CREATE,"Base Log Model creation failed.", e);
            };
        }



        /// <summary>
        /// Converts System.Exception model to custom Exception model.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ForceDebug"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        private static ErrorLogModel ConvertExceptionToLogModel(Exception ex,bool ForceDebug = false)
        {
            
            var model = new ErrorLogModel();
            try
            {
                model.ExceptionMessage = ex.Message;
                if (Configuration.EnableDebugMode || ForceDebug)
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
                throw new EasException(EasMe.Error.FAILED_TO_CONVERT,"Failed to convert System.Exception model to Custom Exception model.", e);
            }
            return model;

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
            catch(Exception ex)
            {
                throw new EasException(EasMe.Error.FAILED_TO_PARSE, "Failed to parse configuration file size.", ex);
            }
        }

    }
}

