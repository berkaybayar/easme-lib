using System;
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

        }

    }


}
