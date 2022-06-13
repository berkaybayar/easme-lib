using EasMe.Models;
using System.Diagnostics;
using System.Text.Json;

namespace EasMe
{
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
        public EasLog(string FilePath, bool EnableConsoleLogging = true,int Interval = 0)
        {
            
            _DirLog = FilePath;
            _Interval = Interval;
            _EnableConsoleLogging = EnableConsoleLogging;
        }

        public EasLog(bool EnableConsoleLogging = true,int Interval = 0)
        {
            _DirLog = Directory.GetCurrentDirectory() + "\\Logs\\";
            _Interval = Interval;
            _EnableConsoleLogging = EnableConsoleLogging;
        }
        
        
        public string Log(string Severity, string LogMessage, string Status, string? Ip = null, string? HWID = null, string? HttpMethod = null, string? RequestUrl = null, int? ErrorNo = null, Exception? ex = null, Dictionary<string, string>? Headers = null)
        {
            string serialized = "";
            try
            {
                var log = CreateModel(Severity, LogMessage, Status, Ip, HWID, HttpMethod, RequestUrl, ErrorNo, ex, Headers);
                serialized = Serialize(log);
                CreateJustString(serialized, false);
            }
            catch (Exception)
            {
                
            }
            return serialized;
        }
        public string Info(string LogMessage, string Status = "SUCCESS", string? Ip = null, string? HWID = null, string? HttpMethod = null, string? RequestUrl = null, int? ErrorNo = null, Dictionary<string, string>? Headers = null)
        {
            string serialized = "";
            try
            {
                var log = CreateModel("INFO", LogMessage, Status, Ip, HWID, HttpMethod, RequestUrl, ErrorNo, null, Headers);
                serialized = Serialize(log);
                CreateJustString(serialized, false);

            }
            catch (Exception)
            {
            }
            return serialized;

        }

        public string Error(string LogMessage, string Status = "FAILED", int ErrorNo = 3, Exception? ex = null, string? Ip = null, string? HWID = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized = "";
            try
            {
                var log = CreateModel("ERROR", LogMessage, Status, Ip, HWID, HttpMethod, RequestUrl, ErrorNo, ex, Headers);
                serialized = Serialize(log);
                CreateJustString(serialized, false);
            }
            catch (Exception)
            {
            }
            return serialized;
        }

        public string Exception(Exception ex, string? Ip = null, string? HWID = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            string serialized = "";
            try
            {
                var log = CreateModel("EXCEPTION", "An exception occured", "FAILED", Ip, HWID, HttpMethod, RequestUrl, 6, ex, Headers);
                serialized = Serialize(log);
                CreateJustString(serialized, false);
            }
            catch (Exception)
            {
            }
            return serialized;
        }

        public string Warn(string LogMessage, string Status, string Ip = "", string HWID = "", string HttpMethod = "", string RequestUrl = "", int? ErrorNo = 2, Dictionary<string, string>? Headers = null)
        {
            string serialized = "";
            try
            {
                var log = CreateModel("WARN", LogMessage, Status, Ip, HWID, HttpMethod, RequestUrl, ErrorNo, null, Headers);
                serialized = Serialize(log);
                CreateJustString(serialized, false);
            }
            catch (Exception)
            {
            }
            return serialized;

        }

        public string Custom(object obj)
        {
            string serialized = "";
            try
            {
                serialized = Serialize(obj);
                CreateJustString(serialized, false);

            }
            catch (Exception)
            {
            }
            return serialized;
        }

        private string Serialize(object obj)
        {
            try
            {
                
                return JsonSerializer.Serialize(obj);

            }
            catch (Exception)
            {
                return "Exception occured"; 
            }
        }
        //private string SerializeException(object ex)
        //{
        //    return Newtonsoft.Json.JsonConvert.SerializeObject(ex, Newtonsoft.Json.Formatting.Indented);       
        //}
        
        public void CreateJustString(string LogContent, bool UseDefaultDate = false)
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
                string LogPath = _DirLog + DateTime.Now.ToString(IntervalFormat) + " -log.txt";
                File.AppendAllText(LogPath, LogContent);
                if(_EnableConsoleLogging)
                    Console.WriteLine(LogContent);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        private LogModel CreateModel(string Severity, string LogMessage, string Status, string? Ip, string? HWID, string? HttpMethod, string? RequestUrl, int? ErrorNo, Exception? ex, Dictionary<string, string>? Headers)
        {
            var log = new LogModel();
            log.Severity = Severity.ToUpper();
            log.Message = LogMessage;
            log.TraceAction = GetActionName();
            log.TraceClass = GetClassName();
            log.Status = Status.ToUpper();
            log.Ip = Ip;
            log.HWID = HWID;
            log.HttpMethod = HttpMethod;
            log.RequestUrl = RequestUrl;
            log.Headers = ConvertHeadersToString(Headers);
            if(ex != null)
            {
                log.ExceptionMessage = ex.Message;
                log.ExceptionStackTrace = ex.StackTrace;
                log.ExceptionSource = ex.Source;
                if (ex.InnerException != null)
                {
                    log.ExceptionInner = ex.InnerException.Message;
                }
            }
           

            log.ErrorNo = ErrorNo;

            return log;
        }
        private string ConvertHeadersToString(Dictionary<string, string>? Headers)
        {
            var val = "";
            if (Headers == null) return "";
            foreach (var item in Headers)
            {
                val += $"{item.Key}:{item.Value}|";
            }
            return val.Substring(0, val.Length - 1);

        }
   
        private string? GetActionName()
        {
            var methodInfo = new StackTrace().GetFrame(3).GetMethod();
            return methodInfo.Name;
        }
        private string? GetClassName()
        {
            var methodInfo = new StackTrace().GetFrame(3).GetMethod();
            return methodInfo.ReflectedType.Name;
        }
        /*
        public DateTime Date { get; private set; } = DateTime.Now;
        public string Severity { get; set; }
        public string? Status { get; set; }
        public string? TraceAction { get; set; }
        public string? TraceClass { get; set; }
        public object? Message { get; set; }
        public int? ErrorNo { get; set; }        
        public string? Ip { get; set; }
        public string? HttpMethod { get; set; }
        public string? RequestUrl { get; set; }
        public string? Headers { get; set; }
        public string? Exception { get; set; }
        public string? HWID { get; set; }
        */

    }

}
