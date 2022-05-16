using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe
{
    public class EasLog
    {
        public static string DirCurrent = Directory.GetCurrentDirectory();//gets current directory 
        public static string DirLog = DirCurrent + "\\Logs\\"; //log file directory

        /*
        Interval value        
        0 => Daily (Default)
        1 => Hourly 
        2 => Every Minute
        */
        public void Create(string log, int interval = 0)
        {
            string IntervalFormat = "";
            string LogContent = $"[{DateTime.Now}] {log}\n";

            //Creates log file in current directory
            if (!Directory.Exists(DirLog)) Directory.CreateDirectory(DirLog);

            switch (interval)
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
        EasAdvancedLog _log = new EasAdvancedLog();
        
    }
    public class EasAdvancedLog : EasLog
    {
        public void Create(string log,string controller, string action, int interval = 0)
        {
            Create($"[{controller}] [{action}] {log}", interval);
            
        }


    }

}
