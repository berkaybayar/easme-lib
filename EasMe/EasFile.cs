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
                Directory.Delete(filePath, true);
                if(isLoggingEnabled) EasLog.Info("Folder deleted: " + filePath); 
            }
            else if(File.Exists(filePath))
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
            else
            {
                throw new EasException("Error in DeleteAll: Given File or folder does not exist => Path: " + filePath);
            }


            
        }

        /// <summary>
        /// Moves all files to destination folder path. If source path is folder moves all files and sub folders inside not the actual folder.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        /// <param name="overwrite"></param>
        /// <param name="isLoggingEnabled"></param>
        public static void MoveAll(string sourcePath, string destPath,bool overwrite, bool isLoggingEnabled = true)
        {

            if (!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);
            if (Directory.Exists(sourcePath))
            {
                string[] files = Directory.GetFiles(sourcePath);
                string[] subdirs = Directory.GetDirectories(sourcePath);
                Parallel.ForEach(files, file =>
                {
                    try
                    {
                        File.Move(file, destPath + "\\" + Path.GetFileName(file), true);
                        if (isLoggingEnabled) EasLog.Info("File moved: " + file);
                    }
                    catch (Exception ex)
                    {
                        if (isLoggingEnabled) EasLog.Exception("Error while moving file => Path: " + file, ex);
                    }
                });
                Parallel.ForEach(subdirs, subdir =>
                {
                    try
                    {
                        MoveAll(subdir, destPath + "\\" + Path.GetFileName(subdir), overwrite, isLoggingEnabled);
                    }
                    catch (Exception ex)
                    {
                        if (isLoggingEnabled) EasLog.Exception("Error while moving file => Source:" + sourcePath + " Destination: " + destPath, ex);
                    }

                });
                
            }
            else if(File.Exists(sourcePath))
            {
                try
                {
                    File.Move(sourcePath,destPath + "\\" + Path.GetFileName(sourcePath), true);
                    if (isLoggingEnabled) EasLog.Info("File moved => Source:" + sourcePath + " Destination: " + destPath);
                }
                catch (Exception ex)
                {
                    if (isLoggingEnabled) EasLog.Exception("Error while moving file => Source:" + sourcePath + " Destination: " + destPath, ex);
                }

            }
            else
            {
                throw new EasException("Error in MoveAll: Given source path not exist.");
                //if (isLoggingEnabled) EasLog.Error("Error while moving file. File or Directory not exist => Source:" + sourcePath + " Destination: " + destPath);
            }
        }

        /// <summary>
        /// Moves all file(s) to destination folder path. If source path is folder moves all files and sub folders inside not the actual folder.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        /// <param name="overwrite"></param>
        /// <param name="isLoggingEnabled"></param>
        public static void CopyAll(string sourcePath, string destPath, bool overwrite, bool isLoggingEnabled = true)
        {

            if (!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);
            if (Directory.Exists(sourcePath))
            {
                string[] files = Directory.GetFiles(sourcePath);
                string[] subdirs = Directory.GetDirectories(sourcePath);
                Parallel.ForEach(files, file =>
                {
                    try
                    {
                        File.Copy(file, destPath + "\\" + Path.GetFileName(file), true);
                        if (isLoggingEnabled) EasLog.Info("File copied: " + file);
                    }
                    catch (Exception ex)
                    {
                        if (isLoggingEnabled) EasLog.Exception("Error while copying file => Path: " + file, ex);
                    }
                });
                Parallel.ForEach(subdirs, subdir =>
                {
                    try
                    {
                        CopyAll(subdir, destPath + "\\" + Path.GetFileName(subdir), overwrite, isLoggingEnabled);
                    }
                    catch (Exception ex)
                    {
                        if (isLoggingEnabled) EasLog.Exception("Error while copying file => Source:" + sourcePath + " Destination: " + destPath, ex);
                    }

                });

            }
            else if (File.Exists(sourcePath))
            {
                try
                {
                    File.Copy(sourcePath, destPath + "\\" + Path.GetFileName(sourcePath), true);
                    if (isLoggingEnabled) EasLog.Info("File copying => Source: " + sourcePath + " Destination: " + destPath);
                }
                catch (Exception ex)
                {
                    if (isLoggingEnabled) EasLog.Exception("Error while copying file => Source:" + sourcePath + " Destination: " + destPath, ex);
                }

            }
            else
            {
                throw new EasException("Error in CopyAll: Given source path not exist.");
                
            }
        }
        
        
    }

}
