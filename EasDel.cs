using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe
{
    public class EasDel
    {


        EasLog _log = new EasLog();
        public static string DirCurrent = Directory.GetCurrentDirectory();//gets current directory 
        public static string DirUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);//gets user directory C:\Users\%username%
        public static string DirDesktop = DirUser + "\\Desktop";//gets desktop directory C:\Users\%username%\desktop
        public static string DirAppData = DirUser + "\\AppData\\Roaming";//gets appdata directory C:\Users\%username%\AppData\\Roaming
        public static string DirSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);//gets system32 directory C:\Windows\System32
        public static string DirLog = DirCurrent + "\\Logs\\"; //log file directory

        public static bool isEnableLogging = true;

        //When calling the class give bool value to determine to enable or disable logging         
        public EasDel(bool _isEnableLogging)
        {
            isEnableLogging = _isEnableLogging;
        }

        public void DeleteAllFiles(string path)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                string[] subdirs = Directory.GetDirectories(path);
                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                        if (isEnableLogging) _log.Create("FILE DELETED: " + file);
                    }
                    catch
                    {
                        if (isEnableLogging) _log.Create("FILE FAILED: " + file);
                    }

                }
                foreach (string subdir in subdirs)
                {
                    DeleteAllFiles(subdir);
                }
                try
                {
                    Directory.Delete(path);
                    if (isEnableLogging) _log.Create("FOLDER DELETED: " + path);
                }
                catch
                {
                    if (isEnableLogging) _log.Create("FOLDER FAILED: " + path);
                }
            }
            else
            {
                try
                {
                    File.Delete(path);
                    if (isEnableLogging) _log.Create("FILE DELETED: " + path);
                }
                catch
                {
                    if (isEnableLogging) _log.Create("FILE FAILED: " + path);
                }

            }

        }




    }

}
