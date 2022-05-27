using System.IO;

namespace EasMe
{
    public class EasDel
    {


        static EasLog _easlog = new EasLog();
        //public static string DirCurrent = Directory.GetCurrentDirectory();//gets current directory 
        //public static string DirUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);//gets user directory C:\Users\%username%
        //public static string DirDesktop = DirUser + "\\Desktop";//gets desktop directory C:\Users\%username%\desktop
        //public static string DirAppData = DirUser + "\\AppData\\Roaming";//gets appdata directory C:\Users\%username%\AppData\\Roaming
        //public static string DirSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);//gets system32 directory C:\Windows\System32
        //public static string DirLog = DirCurrent + "\\Logs\\"; //log file directory

        public static bool _isEnableLogging = true;

        //When calling the class give bool value to determine to enable or disable logging         
        public EasDel(bool isEnableLogging = false)
        {
            _isEnableLogging = isEnableLogging;
        }

        public EasDel(string LogPath)
        {
            _isEnableLogging = true;
            _easlog = new EasLog(LogPath);
        }

        public void DeleteAllFiles(string FilePath)
        {
            if (Directory.Exists(FilePath))
            {
                string[] files = Directory.GetFiles(FilePath);
                string[] subdirs = Directory.GetDirectories(FilePath);
                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                        if (_isEnableLogging) _easlog.Create("[FILE] [DELETED]: " + file);
                    }
                    catch
                    {
                        if (_isEnableLogging) _easlog.Create("[FILE] [DELETION_FAILED]: " + file);
                    }

                }
                foreach (string subdir in subdirs)
                {
                    DeleteAllFiles(subdir);
                }
                try
                {
                    Directory.Delete(FilePath);
                    if (_isEnableLogging) _easlog.Create("[FOLDER] [DELETED]: " + FilePath);
                }
                catch
                {
                    if (_isEnableLogging) _easlog.Create("[FOLDER] [DELETION_FAILED]: " + FilePath);
                }
            }
            else
            {
                try
                {
                    File.Delete(FilePath);
                    if (_isEnableLogging) _easlog.Create("[FILE] [DELETED]: " + FilePath);
                }
                catch
                {
                    if (_isEnableLogging) _easlog.Create("[FILE] [DELETION_FAILED]: " + FilePath);
                }

            }

        }




    }

}
