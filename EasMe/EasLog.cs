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
        private static int _Interval;
        private static bool _EnableConsoleLogging;        
        public static Dictionary<int, string> CustomErrorNoList;
        /*
        Interval value        
        0 => Daily (Default)
        1 => Hourly 
        2 => Every Minute
        */
        /*
        Log Model Num      
        0 => BaseLogModel
        1 => WebLogModel 
        2 => Every Minute
        */

        public EasLog(string FilePath, bool EnableConsoleLogging = true, int Interval = 0)
        {
            
            _DirLog = FilePath;
            _Interval = Interval;
            _EnableConsoleLogging = EnableConsoleLogging;
        }

        public EasLog(bool EnableConsoleLogging = true, int Interval = 0)
        {
            
            _DirLog = Directory.GetCurrentDirectory() + "\\Logs\\";
            _Interval = Interval;
            _EnableConsoleLogging = EnableConsoleLogging;
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
                if (!string.IsNullOrEmpty(Ip))
                {
                    var model = WebModelCreate("INFO", LogMessage, ErrorType.TypeList.SUCCESS, Ip, HttpMethod, RequestUrl, Headers, null);
                    serialized = Log(model);
                }                
                else
                {
                    var model = BaseModelCreate("INFO", LogMessage, ErrorType.TypeList.SUCCESS);
                    serialized = Log(model);
                }
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;
        }
        public string InfoClient(string LogMessage)
        {
            string serialized;
            try
            {
                var model = ClientModelCreate("INFO", LogMessage, ErrorType.TypeList.SUCCESS);
                serialized = Log(model);

            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
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
                if (!string.IsNullOrEmpty(Ip))
                {
                    var model = WebModelCreate("ERROR", LogMessage, ErrorNo, Ip, HttpMethod, RequestUrl, Headers, null);
                    serialized = Log(model);
                }               
                else
                {
                    var model = BaseModelCreate("ERROR", LogMessage, ErrorNo);
                    serialized = Log(model);
                }

            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;
        }
        public string ErrorClient(string LogMessage, ErrorType.TypeList ErrorNo = ErrorType.TypeList.ERROR)
        {
            string serialized;
            try
            {
                var model = ClientModelCreate("ERROR", LogMessage, ErrorNo);
                serialized = Log(model);
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
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

        public string Exception(Exception ex, ErrorType.TypeList ErrorNo = ErrorType.TypeList.EXCEPTION_OCCURED, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            try
            {
                if (!string.IsNullOrEmpty(Ip))
                {
                    var model = WebModelCreate("EXCEPTION", ex.Message, ErrorNo, Ip, HttpMethod, RequestUrl, Headers, null);
                    serialized = Log(model);
                }
                else
                {
                    var model = BaseModelCreate("EXCEPTION", ErrorNo.ToString(), ErrorNo, ex);
                    serialized = Log(model);
                }
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;

        }
        /// <summary>
        /// Creates log with Exception severity and failed status. Logs client model.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public string ExceptionClient(Exception ex, ErrorType.TypeList ErrorNo = ErrorType.TypeList.EXCEPTION_OCCURED)
        {
            string serialized;
            try
            {
                var model = ClientModelCreate("EXCEPTION", ex.Message, ErrorNo);
                serialized = Log(model);
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
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
                if (!string.IsNullOrEmpty(Ip))
                {
                    var model = WebModelCreate("WARN", LogMessage, ErrorNo, Ip, HttpMethod, RequestUrl, Headers, null);
                    serialized = Log(model);
                }                
                else
                {
                    var model = BaseModelCreate("WARN", LogMessage, ErrorNo);
                    serialized = Log(model);
                }
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;
        }
        public string WarnClient(string LogMessage, ErrorType.TypeList ErrorNo = ErrorType.TypeList.WARN)
        {
            string serialized;
            try
            {
                var model = ClientModelCreate("WARN", LogMessage, ErrorNo);
                serialized = Log(model);
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;

        }





        /// <summary>
        /// Creates log with given object.
        /// </summary>
        /// <param name="LogContent"></param>
        /// <param name="UseDefaultDate"></param>
        /// <returns>LogContent</returns>
        public string Log(object obj, bool UseDefaultDate = false)
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
                string IntervalFormat = "";
                if (UseDefaultDate)
                    serialized = $"[{DateTime.Now}] {serialized}";
                else
                    serialized = $"{serialized}";
                //Creates log file in current directory
                if (!Directory.Exists(_DirLog)) Directory.CreateDirectory(_DirLog);

                switch (_Interval)
                {
                    case 0:
                        IntervalFormat = "MM.dd.yyyy";
                        break;
                    case 1:
                        IntervalFormat = "MM.dd.yyyy HH";
                        break;
                    case 2:
                        IntervalFormat = "MM.dd.yyyy HH.mm";
                        break;
                }

                string LogPath = _DirLog + DateTime.Now.ToString(IntervalFormat) + " -log.json";
                File.AppendAllText(LogPath, serialized + "\n");
                if (_EnableConsoleLogging)
                    Console.WriteLine(serialized);
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.CREATING_LOG_ERROR);
            }
            return serialized;
            
            string Serialize(object obj)
            {
                try
                {
                    return JsonSerializer.Serialize(obj);
                }
                catch (Exception e)
                {
                    return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
                }
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
        private WebLogModel WebModelCreate(string Severity, string LogMessage, ErrorType.TypeList ErrorNo, string? Ip, string? HttpMethod, string? RequestUrl, Dictionary<string, string>? Headers, Exception? ex)
        {
            var log = new WebLogModel();
            log.Severity = Severity.ToUpper();
            log.LogType = 1;
            log.Message = LogMessage;
            log.TraceAction = GetActionName();
            log.TraceClass = GetClassName();
            log.Ip = Ip;
            log.HttpMethod = HttpMethod;
            log.RequestUrl = RequestUrl;
            log.Headers = EasProxy.ConvertHeadersToString(Headers);
            log.ErrorNo = ErrorNo.ToString();
            return log;
        }

        /// <summary>
        /// Converts given parameters to BaseLogModel.
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <returns>EasMe.Models.BaseLogModel</returns>
        private BaseLogModel BaseModelCreate(string Severity, string LogMessage, ErrorType.TypeList ErrorNo, Exception? Exception = null)
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
            return log;
        }
        /// <summary>
        /// Converts given parameters to ClientModelCreate.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="Severity"></param>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <returns>EasMe.Models.ClientLogModel</returns>
        private ClientLogModel ClientModelCreate(string Severity, string LogMessage, ErrorType.TypeList ErrorNo)
        {
            var log = new ClientLogModel();
            log.LogType = 2;
            log.Severity = Severity.ToUpper();
            log.Message = LogMessage;
            log.TraceAction = GetActionName();
            log.TraceClass = GetClassName();
            log.ErrorNo = ErrorNo.ToString();
            return log;
        }


        private ErrorLogModel ConvertExceptionToLogModel(Exception ex)
        {
            var model = new ErrorLogModel();
            model.ExceptionMessage = ex.Message;
            model.ExceptionSource = ex.Source;
            model.ExceptionStackTrace = ex.StackTrace;
            var inner = ex.InnerException;
            if (inner != null)
                model.ExceptionInner = inner.ToString();
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
