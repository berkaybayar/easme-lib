namespace EasMe
{
    /// <summary>
    /// File or folder deletion with logging options, uses EasLog
    /// </summary>
    public static class EasDel
    {

        //public static string DirCurrent = Directory.GetCurrentDirectory();//gets current directory 
        //public static string DirUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);//gets user directory C:\Users\%username%
        //public static string DirDesktop = DirUser + "\\Desktop";//gets desktop directory C:\Users\%username%\desktop
        //public static string DirAppData = DirUser + "\\AppData\\Roaming";//gets appdata directory C:\Users\%username%\AppData\\Roaming
        //public static string DirSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);//gets system32 directory C:\Windows\System32
        //public static string DirLog = DirCurrent + "\\Logs\\"; //log file directory


        /// <summary>
        /// Deletes file or folder, if it is folder it will delete all files and subfolders.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="isLog"></param>
        public static void DeleteAllFiles(string FilePath, bool isLoggingEnabled = true)
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
                        if (isLoggingEnabled) EasLog.Info("File deleted: " + file);
                    }
                    catch
                    {
                        if (isLoggingEnabled) EasLog.Error("Error deleting file: " + file, Error.FAILED_TO_DELETE);
                    }

                }
                foreach (string subdir in subdirs)
                {
                    DeleteAllFiles(subdir);
                }
                try
                {
                    Directory.Delete(FilePath);
                    if (isLoggingEnabled) EasLog.Info("Folder deleted: " + FilePath);
                }
                catch
                {
                    if (isLoggingEnabled) EasLog.Error("Error deleting folder:" + FilePath, Error.FAILED_TO_DELETE);
                }
            }
            else
            {
                try
                {
                    File.Delete(FilePath);
                    if (isLoggingEnabled) EasLog.Info("File deleted: " + FilePath);
                }
                catch
                {
                    if (isLoggingEnabled) EasLog.Error("Error deleting file:" + FilePath, Error.FAILED_TO_DELETE);
                }

            }

        }
    }

}
