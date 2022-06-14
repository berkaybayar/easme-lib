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
        private static bool _SeperateLogTypes;
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

        public EasLog(string FilePath, bool SeperateLogTypes = true, bool EnableConsoleLogging = true, int Interval = 0)
        {
            _SeperateLogTypes = SeperateLogTypes;
            _DirLog = FilePath;
            _Interval = Interval;
            _EnableConsoleLogging = EnableConsoleLogging;
        }

        public EasLog(bool SeperateLogTypes = true, bool EnableConsoleLogging = true, int Interval = 0)
        {
            _SeperateLogTypes = SeperateLogTypes;
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
        public string Info(string LogMessage)
        {
            string serialized;
            try
            {
                var log = BaseModelCreate("INFO", LogMessage, ErrorType.TypeList.SUCCESS);
                serialized = Serialize(log);
                Log(serialized, false);

            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;

        }
        public string InfoWeb(string LogMessage, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            try
            {
                var log = WebModelCreate("INFO", LogMessage, ErrorType.TypeList.SUCCESS, Ip, HttpMethod, RequestUrl, Headers, null);
                serialized = Serialize(log);
                Log(serialized, false);

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
                var log = ClientModelCreate("INFO", LogMessage, ErrorType.TypeList.SUCCESS);
                serialized = Serialize(log);
                Log(serialized, false);

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
        public string Error(string LogMessage, ErrorType.TypeList ErrorNo = ErrorType.TypeList.ERROR)
        {
            string serialized;
            try
            {
                var log = BaseModelCreate("ERROR", LogMessage, ErrorNo);
                serialized = Serialize(log);
                Log(serialized, false);
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;
        }
        public string ErrorWeb(string LogMessage, ErrorType.TypeList ErrorNo = ErrorType.TypeList.ERROR, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            try
            {
                var log = WebModelCreate("ERROR", LogMessage, ErrorNo, Ip, HttpMethod, RequestUrl, Headers, null);
                serialized = Serialize(log);
                Log(serialized, false);
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
                var log = ClientModelCreate("ERROR", LogMessage, ErrorNo);
                serialized = Serialize(log);
                Log(serialized, false);
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;
        }

        /// <summary>
        /// Creates log with Exception severity and failed status. ErrorLogModel is used
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public string Exception(Exception ex, ErrorType.TypeList Error = ErrorType.TypeList.EXCEPTION_OCCURED)
        {
            string serialized;
            try
            {
                serialized = Serialize(ErrorModelCreate(ex, "ERROR", Error));
                Log(serialized, false);
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;
        }

        /// <summary>
        /// Creates log with Warning severity and warn status.
        /// </summary>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="Ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public string Warn(string LogMessage, ErrorType.TypeList ErrorNo = ErrorType.TypeList.WARN)
        {
            string serialized;
            try
            {
                var log = BaseModelCreate("WARN", LogMessage, ErrorNo);
                serialized = Serialize(log);
                Log(serialized, false);
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;

        }
        public string WarnWeb(string LogMessage, ErrorType.TypeList ErrorNo = ErrorType.TypeList.WARN, string? Ip = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized;
            try
            {
                var log = WebModelCreate("WARN", LogMessage, ErrorNo, Ip, HttpMethod, RequestUrl, Headers, null);
                serialized = Serialize(log);
                Log(serialized, false);
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
                var log = ClientModelCreate("WARN", LogMessage, ErrorNo);
                serialized = Serialize(log);
                Log(serialized, false);
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;

        }


        /// <summary>
        /// Creates log with given custom class, serializes the custom log model.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string Custom(object obj)
        {
            string serialized;
            try
            {
                serialized = Serialize(obj);
                Log(serialized, false);

            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.LOGGING_ERROR);
            }
            return serialized;
        }

        /// <summary>
        /// Serializing the log model to json string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private string Serialize(object obj)
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

        /// <summary>
        /// Creates simple log with given string.
        /// </summary>
        /// <param name="LogContent"></param>
        /// <param name="UseDefaultDate"></param>
        /// <returns>LogContent</returns>
        public string Log(string LogContent, bool UseDefaultDate = false)
        {

            try
            {
                string IntervalFormat = "";
                if (UseDefaultDate)
                    LogContent = $"[{DateTime.Now}] {LogContent}\n";
                else
                    LogContent = $"{LogContent}\n";
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
                switch (_SeperateLogTypes)
                {
                    case true:
                        break;
                    case false:
                        break;
                }
                string LogPath = _DirLog + DateTime.Now.ToString(IntervalFormat) + " -log.txt";
                File.AppendAllText(LogPath, LogContent);
                if (_EnableConsoleLogging)
                    Console.WriteLine(LogContent);
            }
            catch (Exception e)
            {
                return Exception(e, ErrorType.TypeList.CREATING_LOG_ERROR);
            }
            return LogContent;

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
            var log = new WebLogModel
            {
                Severity = Severity.ToUpper(),
                LogType = 1,
                Message = LogMessage,
                TraceAction = GetActionName(),
                TraceClass = GetClassName(),
                Ip = Ip,
                HttpMethod = HttpMethod,
                RequestUrl = RequestUrl,
                Headers = EasProxy.ConvertHeadersToString(Headers),
                ErrorNo = ErrorNo.ToString()
            };
            return log;
        }

        /// <summary>
        /// Converts given parameters to BaseLogModel.
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="LogMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <returns>EasMe.Models.BaseLogModel</returns>
        private BaseLogModel BaseModelCreate(string Severity, string LogMessage, ErrorType.TypeList ErrorNo)
        {
            var log = new BaseLogModel
            {
                Severity = Severity.ToUpper(),
                Message = LogMessage,
                LogType = 0,
                TraceAction = GetActionName(),
                TraceClass = GetClassName(),
                ErrorNo = ErrorNo.ToString()
            };
            return log;
        }

        /// <summary>
        /// Converts given parameters to ErrorLogModel.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="Severity"></param>
        /// <param name="ErrorNo"></param>
        /// <returns>EasMe.Models.ErrorLogModel</returns>
        private ErrorLogModel ErrorModelCreate(Exception? ex, string Severity, ErrorType.TypeList ErrorNo)
        {
            var message = ErrorType.EnumGetKeybyValue(ErrorNo);
            if (string.IsNullOrEmpty(message)) message = "Unknown Error";
            var log = new ErrorLogModel
            {
                LogType = -1,
                Severity = Severity.ToUpper(),
                Message = message,
                TraceAction = GetActionName(),
                TraceClass = GetClassName(),
                ErrorNo = ErrorNo.ToString()
            };
            if (ex != null)
            {
                log.ExceptionMessage = ex.Message;
                log.ExceptionStackTrace = ex.StackTrace;
                log.ExceptionSource = ex.Source;
                if (ex.InnerException != null)
                {
                    log.ExceptionInner = ex.InnerException.Message;
                }
            }
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
            var log = new ClientLogModel
            {
                LogType = 2,
                Severity = Severity.ToUpper(),
                Message = LogMessage,
                TraceAction = GetActionName(),
                TraceClass = GetClassName(),
                ErrorNo = ErrorNo.ToString(),
            };
            return log;
        }


        /// <summary>
        /// Gets the name of the function this function is called from.
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        private string? GetActionName(int frame = 3)
        {
            var methodInfo = new StackTrace().GetFrame(frame).GetMethod();
            return methodInfo.Name;
        }

        /// <summary>
        /// Gets the name of the class this function is called from.
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        private string? GetClassName(int frame = 3)
        {
            var methodInfo = new StackTrace().GetFrame(frame).GetMethod();
            return methodInfo.ReflectedType.Name;
        }


    }

}
