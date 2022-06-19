using EasMe.Models;
using EasMe.Models.LogModels;
using System.Diagnostics;
using System.Text.Json;

namespace EasMe
{
    /// <summary>
    /// Simple logging helper with few useful options.
    /// </summary>
    public class EasLog
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
        public static Dictionary<int, string> _CustomErrorList;
        
        public EasLog(LogConfiguration config)
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

        public string Info(string LogMessage, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            try
            {
                var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
                var model = BaseModelCreate("INFO", LogMessage, ErrorType.TypeList.SUCCESS, null, webModel);
                serialized = Log(model);
            }
            catch (Exception e)
            {
                return Error(e, ErrorType.TypeList.LOGGING_ERROR);
            }
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

        public string Error(string LogMessage, ErrorType.TypeList ErrorNo = ErrorType.TypeList.ERROR, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            try
            {
                var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
                var model = BaseModelCreate("ERROR", LogMessage, ErrorNo, null, webModel);
                serialized = Log(model);

            }
            catch (Exception e)
            {
                return Error(e, ErrorType.TypeList.LOGGING_ERROR);
            }
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

        public string Error(Exception ex, ErrorType.TypeList ErrorNo = ErrorType.TypeList.EXCEPTION_OCCURED, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            try
            {
                var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
                var model = BaseModelCreate("EXCEPTION", ErrorNo.ToString(), ErrorNo, ex, webModel);
                serialized = Log(model);
            }
            catch (Exception e)
            {
                return Error(e, ErrorType.TypeList.LOGGING_ERROR);
            }
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
        public string Warn(string LogMessage, ErrorType.TypeList ErrorNo = ErrorType.TypeList.WARN, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            try
            {
                var webModel = WebModelCreate(Ip, HttpMethod, RequestUrl, Headers);
                var model = BaseModelCreate("WARN", LogMessage, ErrorNo, null, webModel);
                serialized = Log(model);
            }
            catch (Exception e)
            {
                return Error(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;
        }




        /// <summary>
        /// Creates log with given object.
        /// </summary>
        /// <param name="LogContent"></param>
        /// <param name="UseDefaultDate"></param>
        /// <returns>LogContent</returns>
        public string Log(object obj)
        {
            string serialized = "";
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

                string LogPath = _DirLog + "\\" + _LogFileName  + DateTime.Now.ToString(_DateFormat);
                File.AppendAllText(LogPath, serialized + "\n");
                if (_EnableConsoleLogging)
                    Console.WriteLine(serialized);
            }
            catch (Exception e)
            {
                return Error(e, ErrorType.TypeList.CREATING_LOG_ERROR);
            }
            return serialized;
            
            
        }
        
        public string Serialize(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj).Trim();
            }
            catch (Exception e)
            {
                return Error(e, ErrorType.TypeList.LOGGING_ERROR);
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
        private WebLogModel WebModelCreate(string? Ip, string? HttpMethod, string? RequestUrl, Dictionary<string, string>? Headers)
        {
            var log = new WebLogModel();            
            log.Ip = Ip;
            log.HttpMethod = HttpMethod;
            log.RequestUrl = RequestUrl;
            log.Headers = EasProxy.ConvertHeadersToString(Headers);
            
            return log;
        }

        /// <summary>
        /// Converts given parameters to BaseLogModel.
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <returns>EasMe.Models.BaseLogModel</returns>
        private BaseLogModel BaseModelCreate(string Severity, string LogMessage, ErrorType.TypeList ErrorNo, Exception? Exception = null,WebLogModel? WebLog = null)
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
        


        private ErrorLogModel ConvertExceptionToLogModel(Exception ex)
        {
            var model = new ErrorLogModel();
            model.ExceptionMessage = ex.Message;
            if (_EnableDebugMode)
            {
                model.ExceptionSource = ex.Source;
                model.ExceptionStackTrace = ex.StackTrace;
                var inner = ex.InnerException;
                if (inner != null)
                    model.ExceptionInner = inner.ToString();
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
