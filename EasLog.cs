using System;
using System.IO;

namespace EasMe
{
    public class EasLog
    {
        private static string DirLog;

        public EasLog(string FilePath)
        {
            DirLog = FilePath;
        }

        public EasLog()
        {
            DirLog = Directory.GetCurrentDirectory() + "\\Logs\\";
        }
        
        /*
        Interval value        
        0 => Daily (Default)
        1 => Hourly 
        2 => Every Minute
        */
        public void Create(string LogContent, int Interval = 0)
        {
            string IntervalFormat = "";
            LogContent = $"[{DateTime.Now}] {LogContent}\n";

            //Creates log file in current directory
            if (!Directory.Exists(DirLog)) Directory.CreateDirectory(DirLog);

            switch (Interval)
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

            string LogPath = DirLog + DateTime.Now.ToString(IntervalFormat) + " -log.txt";
            File.AppendAllText(LogPath, LogContent);

        }

    }


}
