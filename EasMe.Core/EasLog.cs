using System.Text.Json;

namespace EasMe.Core
{
    public class EasLog
    {
        private static string _DirLog;
        private static int _Interval;


        /*
        Interval value        
        0 => Daily (Default)
        1 => Hourly 
        2 => Every Minute
        */
        public EasLog(string FilePath, bool SeperateEachSeverity = false, int Interval = 0)
        {
            _DirLog = FilePath;
            _Interval = Interval;
        }

        public EasLog(int Interval = 0)
        {
            _DirLog = Directory.GetCurrentDirectory() + "\\Logs\\";
            _Interval = Interval;
        }




        public void Info(string LogMessage, string Action, string Status, string Ip = "", string HWID = "", string HttpMethod = "", string RequestUrl = "")
        {
            try
            {
                var log = CreateModel("Info", LogMessage, Action, Status, Ip, HWID, HttpMethod, RequestUrl);
                CreateJustString(Serialize(log), false);
            }
            catch (Exception ex)
            {
                ErrorOccured(ex);
            }
            
        }
        public void Simple(string Severity, string LogMessage, string Action, string Status)
        {
            try
            {
                var log = CreateModel(Severity, LogMessage, Action, Status);
                CreateJustString(Serialize(log), false);
            }
            catch (Exception ex)
            {
                ErrorOccured(ex);
            }
            
        }
        public void Error(string LogMessage, string Action, string Status, string? Ip = null, string? HWID = null, string? HttpMethod = null, string? RequestUrl = null, string? ErrorMessage = null, int? ErrorNo = null, Exception? ex = null, Dictionary<string, string>? Headers = null)
        {
            try
            {
                var log = CreateModel("Error", LogMessage, Action, Status, Ip, HWID, HttpMethod, RequestUrl, ErrorMessage, ErrorNo, ex, Headers);
                CreateJustString(Serialize(log), false);
            }
            catch (Exception exc)
            {
                ErrorOccured(exc);
            }
            

        }

        public void Exception(string LogMessage, string Action, string Status, Exception ex, string ErrorMessage, int ErrorNo, string? Ip = null, string? HWID = null, string? HttpMethod = null, string? RequestUrl = null, Dictionary<string, string>? Headers = null)
        {
            try
            {
                var log = CreateModel("Exception", LogMessage, Action, Status, Ip, HWID, HttpMethod, RequestUrl, ErrorMessage, ErrorNo, ex, Headers);
                CreateJustString(Serialize(log), false);

            }
            catch (Exception exc)
            {
                ErrorOccured(exc);
            }
            
        }

        public void Warn(string LogMessage, string Action, string Status, string Ip = "", string HWID = "", string HttpMethod = "", string RequestUrl = "")
        {
            try
            {
                var log = CreateModel("Warn", LogMessage, Action, Status, Ip, HWID, HttpMethod, RequestUrl);
                CreateJustString(Serialize(log), false);
            }
            catch (Exception exc)
            {
                ErrorOccured(exc);
            }
            

        }

        public void Custom(object obj)
        {
            try
            {
                CreateJustString(Serialize(obj), false);

            }
            catch (Exception ex)
            {
                ErrorOccured(ex);
            }
        }

        private string Serialize(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj);

            }
            catch (Exception ex)
            {
                ErrorOccured(ex);
                return "Error occured while serializing object";
            }
        }

        public void CreateJustString(string LogContent, bool UseDefaultDate = true)
        {
            string IntervalFormat = "";
            LogContent = $"[{DateTime.Now}] {LogContent}\n";

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
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ErrorOccured(Exception ex)
        {
            try
            {
                CreateJustString(Serialize(ex), false);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        private LogMessageModel CreateModel(string Severity, string LogMessage, string Action, string Status, string? Ip = null, string? HWID = null, string? HttpMethod = null, string? RequestUrl = null, string? ErrorMessage = null, int? ErrorNo = null, Exception? ex = null, Dictionary<string, string>? Headers = null)
        {
            var log = new LogMessageModel();
            log.Severity = Severity;
            log.Action = Action;
            log.Status = Status;
            log.Ip = Ip;
            log.HWID = HWID;
            log.HttpMethod = HttpMethod;
            log.RequestUrl = RequestUrl;
            log.Headers = Headers;
            log.Exception = ex;
            log.ErrorNo = ErrorNo;
            log.ErrorMessage = ErrorMessage;
            return log;
        }
        public class LogMessageModel
        {
            public DateTime Date { get; set; } = DateTime.Now;
            public string Severity { get; set; }
            public string? HttpMethod { get; set; }
            public string? RequestUrl { get; set; }
            public Dictionary<string, string>? Headers { get; set; }
            public string? HWID { get; set; }
            public string? Ip { get; set; }
            public string? Status { get; set; }
            public string? Action { get; set; }

            public object? Message { get; set; }

            public Exception? Exception { get; set; }

            public int? ErrorNo { get; set; }

            public string? ErrorMessage { get; set; }


        }
    }

}
