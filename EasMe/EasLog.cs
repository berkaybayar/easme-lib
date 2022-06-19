using EasMe.Models;
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
        private static string _DirLog;
        private static string _DateFormat;
        private static bool _EnableConsoleLogging;
        private static string _MaxLogFileSize;
        private static string _LogFileExtension;
        private static string _LogFileName;
        private static bool _SeperateLogs;
        private static bool _EnableClientInfoLogging;
        private static bool _EnableDebugMode;
        private static bool _IsLoadedConfig;
        public static Dictionary<int, string> _CustomErrorList;


        /// <summary>
        /// Initialize the log configuration. Call this method in your application startup.
        /// </summary>
        /// <param name="config"></param>
        public static void LoadConfiguration(LogConfiguration config)        {
            
            try
            {
                _DirLog = config.LogFolderPath;
                _DateFormat = config.DateFormatString;
                _EnableConsoleLogging = config.EnableConsoleLogging;
                _CustomErrorList = config.CustomErrorList;
                _MaxLogFileSize = config.MaxLogFileSize;
                _LogFileExtension = config.LogFileExtension;
                _SeperateLogs = config.SeperateLogs;
                _LogFileName = config.LogFileName;
                _EnableClientInfoLogging = config.EnableClientInfoLogging;
                _EnableDebugMode = config.EnableDebugMode;
                _IsLoadedConfig = true;
            }
            catch (Exception e)
            {
                _IsLoadedConfig = false;
                throw new Exception(ErrorType.ErrorList.LOAD_CONFIGURATION_ERROR.ToString(),e);
            }
        }
        /// <summary>
        /// Initialize default configuration. Call this method in your application startup.
        /// </summary>
        public static void LoadDefaultConfiguration()
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

        public static string Info(string LogMessage, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate("INFO", LogMessage, ErrorType.ErrorList.SUCCESS, null, webModel);
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

        public static string Error(string LogMessage, ErrorType.ErrorList ErrorNo = ErrorType.ErrorList.ERROR, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate("ERROR", LogMessage, ErrorNo, null, webModel);
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

        public static string Error(Exception ex, ErrorType.ErrorList ErrorNo = ErrorType.ErrorList.EXCEPTION_OCCURED, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate("EXCEPTION", ErrorNo.ToString(), ErrorNo, ex, webModel);
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
        public static string Warn(string LogMessage, ErrorType.ErrorList ErrorNo = ErrorType.ErrorList.WARN, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
            var model = BaseModelCreate("WARN", LogMessage, ErrorNo, null, webModel);
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
            if (!_IsLoadedConfig)
            {
                throw new Exception(ErrorType.ErrorList.NOT_FOUND_LOADED_CONFIGURATION_ERROR.ToString());
            }
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
                if (!Directory.Exists(_DirLog)) Directory.CreateDirectory(_DirLog);

                string LogPath = _DirLog + "\\" + _LogFileName  + DateTime.Now.ToString(_DateFormat) + _LogFileExtension;
                File.AppendAllText(LogPath, serialized + "\n");
                if (_EnableConsoleLogging)
                    Console.WriteLine(serialized);
                return serialized;                
            }
            catch (Exception e)
            {
                throw new Exception(ErrorType.ErrorList.LOGGING_ERROR.ToString(), e);
            }
            
            
        }
        
        public static string Serialize(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj).Trim();
            }
            catch (Exception e)
            {
                throw new Exception(ErrorType.ErrorList.SERIALIZATION_ERROR.ToString(), e);
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
        private static WebLogModel WebModelCreate(string? Ip, string? HttpMethod, string? RequestUrl, Dictionary<string, string>? Headers)
        {
            var log = new WebLogModel();            
            try
            {
                log.Ip = Ip;
                log.HttpMethod = HttpMethod;
                log.RequestUrl = RequestUrl;
                log.Headers = EasProxy.ConvertHeadersToString(Headers);                
            }
            catch (Exception e)
            {
                throw new Exception(ErrorType.ErrorList.CREATE_WEB_MODEL_ERROR.ToString(), e);
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
        private static BaseLogModel BaseModelCreate(string Severity, string LogMessage, ErrorType.ErrorList ErrorNo, Exception? Exception = null,WebLogModel? WebLog = null)
        {
            try
            {
                var log = new BaseLogModel();
                log.Severity = Severity.ToUpper();
                log.Message = LogMessage;
                log.LogType = 0;

                log.TraceAction = GetActionName();
                log.TraceClass = GetClassName();
                log.ErrorNo = ErrorNo.ToString();
                if (Exception != null)
                    //log.Exception = Serialize(ConvertExceptionToLogModel(Exception));
                    log.Exception = ConvertExceptionToLogModel(Exception);
                if (_EnableClientInfoLogging)
                    log.ClientLog = new ClientLogModel();
                log.WebLog = WebLog;
                return log;
            }
            catch (Exception e)
            {
                throw new Exception(ErrorType.ErrorList.CREATE_BASE_MODEL_ERROR.ToString(), e);
            }
        }



        private static ErrorLogModel ConvertExceptionToLogModel(Exception ex)
        {
            var model = new ErrorLogModel();
            try
            {
                model.ExceptionMessage = ex.Message;
                if (_EnableDebugMode)
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
                throw new Exception(ErrorType.ErrorList.CONVERT_EXCEPTION_TO_LOG_MODEL_ERROR.ToString(), e);
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
