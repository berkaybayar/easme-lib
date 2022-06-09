using System;
using System.Collections.Generic;
using System.IO;

namespace EasMe
{
    public class EasLog
    {
        private static string _DirLog;
        private static int _Interval;

        public EasLog(string FilePath, int Interval = 0)
        {
            _DirLog = FilePath;
            _Interval = Interval;
        }

        public EasLog(int Interval = 0)
        {
            _DirLog = Directory.GetCurrentDirectory() + "\\Logs\\";
            _Interval = Interval;
        }

        /*
        Interval value        
        0 => Daily (Default)
        1 => Hourly 
        2 => Every Minute
        */
        public void Create(string LogContent)
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

    }
    public class EasLogger
    {

    }

    public class LogMessageModel
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public string HttpMethod { get; set; }
        public string RequestUrl { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string HWID { get; set; }
        public string Ip { get; set; }
        public string Status { get; set; }
        public string Action { get; set; }

        public object LogMessage { get; set; }

        public string LogException { get; set; }
        public Exception Exception { get; set; }

        public int ErrorNo { get; set; }

        public string ErrorMsg { get; set; }

        public string ProjectName { get; set; }

        public string ServiceType { get; set; }

        public string PlatformIndex { get; set; }


    }
}
