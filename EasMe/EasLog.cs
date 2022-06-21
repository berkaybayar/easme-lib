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

        private static LogConfiguration _config = new();

        /// <summary>
        /// Initialize the log configuration. Call this method in your application startup.
        /// </summary>
        /// <param name="config"></param>
        public static void LoadConfiguration(LogConfiguration config)
        {
            _config = config;
            Info("Log configuration loaded.");
        }
        /// <summary>
        /// Initialize default configuration. Call this method in your application startup.
        /// </summary>
        public static void LoadConfigurationDefault()
        {
            LoadConfiguration(new LogConfiguration());
        }

        /// <summary>
        /// Creates log with Info severity and success status.
        /// </summary>
        /// <param name="LogMessage"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static string Info(object LogMessage, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.INFO, LogMessage, EasMe.Error.SUCCESS, null, webModel);
            serialized = Log(model);
            return serialized;
        }


        /// <summary>
        /// Creates log with Error severity and failed status.
        /// </summary>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static string Error(object LogMessage, Error ErrorNo = EasMe.Error.ERROR, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.ERROR, LogMessage, ErrorNo, null, webModel);
            serialized = Log(model);
            return serialized;
        }


        /// <summary>
        /// Creates log with Exception severity and failed status. If ip variable added it logs web model. If not it logs base model.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public static string Error(Exception ex, Error ErrorNo = EasMe.Error.EXCEPTION_OCCURED, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.EXCEPTION_OCCURED, ErrorNo.ToString(), ErrorNo, ex, webModel);
            serialized = Log(model);
            return serialized;

        }


        /// <summary>
        /// Creates log with warning severity and warn status. If ip variable added it logs web model. If client logging enabled logs client model. If not it logs base model.
        /// </summary>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public static string Warn(object LogMessage, Error ErrorNo = EasMe.Error.WARN, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate(Severity.WARN, LogMessage, ErrorNo, null, webModel);
            serialized = Log(model);
            return serialized;
        }




        /// <summary>
        /// Creates log with given object.
        /// </summary>
        /// <param name="LogContent"></param>
        /// <param name="UseDefaultDate"></param>
        /// <returns>LogContent</returns>
        public static string Log(object obj)
        {
            
            string serialized = "Error in Logging";
            try
            {
                if (obj == null) return serialized;
                if (obj is string)
                {
                    serialized = obj.ToString();
                }
                else
                {
                    serialized = Serialize(obj);
                }
                if (!Directory.Exists(_config.LogFolderPath)) Directory.CreateDirectory(_config.LogFolderPath);

                string LogPath = _config.LogFolderPath + "\\" + _config.LogFileName + DateTime.Now.ToString(_config.DateFormatString) + _config.LogFileExtension;
                File.AppendAllText(LogPath, serialized + "\n");
                if (_config.EnableConsoleLogging)
                    Console.WriteLine(serialized);
                return serialized;
            }
            catch (Exception e)
            {
                throw new Exception(EasMe.Error.LOGGING_ERROR.ToString(), e);
            }


        }

        public static string Serialize(object obj)
        {
            try
            {
                var o = new JsonSerializerOptions();
                o.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                return JsonSerializer.Serialize(obj, o);
            }
            catch (Exception e)
            {
                throw new Exception(EasMe.Error.SERIALIZATION_ERROR.ToString(), e);
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
                throw new Exception(EasMe.Error.FAILED_TO_CREATE_WEB_MODEL.ToString(), e);
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
        private static BaseLogModel BaseModelCreate(Severity Severity, object Log, Error ErrorNo, Exception? Exception = null, WebLogModel? WebLog = null)
        {
            
            try
            {
                
                var log = new BaseLogModel();
                log.Severity = Severity.ToString();
                log.Message = Log;
                log.LogType = 0;
                if (_config.EnableDebugMode == true)
                {
                    log.TraceAction = GetActionName();
                    log.TraceClass = GetClassName();
                }

                log.ErrorNo = ErrorNo.ToString();


                if (_config.EnableClientInfoLogging)
                {
                    log.ClientLog = new ClientLogModel();
                    log.LogType = 2;
                }
                if (WebLog != null)
                {
                    log.WebLog = WebLog;
                    log.LogType = 1;
                }
                if (Exception != null)
                {
                    log.Exception = ConvertExceptionToLogModel(Exception);
                    log.LogType = -1;
                }
                return log;
            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.FAILED_TO_CREATE_BASE_MODEL, e);
            };
        }




        private static ErrorLogModel ConvertExceptionToLogModel(Exception ex)
        {
            var model = new ErrorLogModel();
            try
            {
                model.ExceptionMessage = ex.Message;
                if (_config.EnableDebugMode)
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
                throw new Exception(EasMe.Error.FAILED_TO_CONVERT_EXCEPTION_TO_LOG_MODEL.ToString(), e);
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


    }
}

