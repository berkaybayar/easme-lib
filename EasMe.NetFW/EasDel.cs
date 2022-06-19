using System.IO;

namespace EasMe
{
    /// <summary>
    /// File or folder deletion with logging options, uses EasLog
    /// </summary>
    public class EasDel
    {

        static EasLog _log = new EasLog();
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
            _log = new EasLog(LogPath);
        }
        /// <summary>
        /// Deletes file or folder, if it is folder it will delete all files and subfolders
        /// </summary>
        /// <param name="FilePath"></param>
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
                        if (_isEnableLogging) _log.Info("File deleted: " + file);
                    }
                    catch
                    {
                        if (_isEnableLogging) _log.Error("Error deleting file: " + file,Models.ErrorType.TypeList.FAILED_TO_DELETE_FILE);
                    }

                }
                foreach (string subdir in subdirs)
                {
                    DeleteAllFiles(subdir);
                }
                try
                {
                    Directory.Delete(FilePath);
                    if (_isEnableLogging) _log.Info("Folder deleted: " + FilePath);
                }
                catch
                {
                    if (_isEnableLogging) _log.Error("Error deleting folder:" + FilePath);
                }
            }
            else
            {
                try
                {
                    File.Delete(FilePath);
                    if (_isEnableLogging) _log.Info("File deleted: " + FilePath);
                }
                catch
                {
                    if (_isEnableLogging) _log.Error("Error deleting file:" + FilePath);
                }

            }

        }
    }

}
