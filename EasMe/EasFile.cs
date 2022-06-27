using EasMe.Models.LogModels;

namespace EasMe
{
    /// <summary>
    /// File or folder deletion with logging options, uses EasLog
    /// </summary>
    public static class EasFile
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
        /// <param name="filePath"></param>
        /// <param name="isLog"></param>
        public static void DeleteAll(string filePath, bool isLoggingEnabled = true)
        {
            if (Directory.Exists(filePath))
            {
                string[] files = Directory.GetFiles(filePath);
                string[] subdirs = Directory.GetDirectories(filePath);
                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                        if (isLoggingEnabled) EasLog.Info("File deleted: " + file);
                    }
                    catch (Exception ex)
                    {
                        if (isLoggingEnabled) EasLog.Exception("Error deleting file => Path: " + filePath, ex);
                    }

                }
                foreach (string subdir in subdirs)
                {
                    DeleteAll(subdir);
                }
                try
                {
                    Directory.Delete(filePath);
                    if (isLoggingEnabled) EasLog.Info("Folder deleted: " + filePath);
                }
                catch (Exception ex)
                {
                    if (isLoggingEnabled) EasLog.Exception("Error deleting file => Path: " + filePath, ex);
                }
            }
            else
            {
                try
                {
                    File.Delete(filePath);
                    if (isLoggingEnabled) EasLog.Info("File deleted: " + filePath);
                }
                catch (Exception ex)
                {
                    if (isLoggingEnabled) EasLog.Exception("Error deleting file => Path: " + filePath, ex);
                }

            }


            
        }
        public static void MoveAll(string sourceFolder, string destFolder,bool overwrite, bool isLoggingEnabled = true)
        {
            try
            {

                DirectoryInfo dirInfo = new DirectoryInfo(destFolder);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(destFolder);

                List<string> Files = Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories).ToList();

                foreach (string file in Files)
                {
                    FileInfo mFile = new(file);
                    try
                    {
                        mFile.MoveTo(dirInfo + "\\" + mFile.Name, overwrite);
                        if (isLoggingEnabled) EasLog.Info("File moved: " + file);
                    }
                    catch
                    {
                        if (isLoggingEnabled) EasLog.Error("Overwrite not true, Error copying file => Source: " + sourceFolder + " Destination: " + destFolder);
                    }
                }
            }
            catch (Exception ex)
            {

                if (isLoggingEnabled) EasLog.Exception(Error.FAILED_TO_MOVE, ex);
            }
        }

        public static void CopyAll(string sourceFolder, string destFolder, bool overwrite, bool isLoggingEnabled = true)
        {
            try
            {

                DirectoryInfo dirInfo = new DirectoryInfo(destFolder);
                if (!dirInfo.Exists)
                    Directory.CreateDirectory(destFolder);

                List<string> Files = Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories).ToList();

                foreach (string file in Files)
                {
                    FileInfo mFile = new(file);
                    try
                    {
                        mFile.CopyTo(dirInfo + "\\" + mFile.Name, overwrite);
                        if (isLoggingEnabled) EasLog.Info("File copied: " + file);
                    }
                    catch
                    {
                        if (isLoggingEnabled) EasLog.Error("Overwrite not true, Error copying file => Source: " + sourceFolder + " Destination: " + destFolder + file);
                    }
                }
            }
            catch (Exception ex)
            {
                if (isLoggingEnabled) EasLog.Exception("Error copying file => Source: " + sourceFolder + " Destination: " + destFolder, ex);
            }
        }        
    }

}
