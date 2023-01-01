using EasMe.Exceptions;
using System.Data.HashFunction.xxHash;
using System.IO;
using System.Security.Cryptography;

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
                if (isLoggingEnabled) SelfLog.Logger.Info("Folder deleted: " + filePath);
            }
            else if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    if (isLoggingEnabled) SelfLog.Logger.Info("File deleted: " + filePath);
                }
                catch (Exception ex)
                {
                    if (isLoggingEnabled) SelfLog.Logger.Exception(ex, "Error deleting file => Path: " + filePath);
                }

            }
            else
            {
                throw new NotExistException("Error in DeleteAll: Given File or folder does not exist => Path: " + filePath);
            }



        }

        /// <summary>
        /// Moves all files to destination folder path. If source path is folder moves all files and sub folders inside not the actual folder.
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        /// <param name="overwrite"></param>
        /// <param name="isLoggingEnabled"></param>
        public static void MoveAll(string sourcePath, string destPath, bool overwrite, bool isLoggingEnabled = true)
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
                        if (isLoggingEnabled) SelfLog.Logger.Info("File moved: " + file);
                    }
                    catch (Exception ex)
                    {
                        if (isLoggingEnabled) SelfLog.Logger.Exception(ex, "Error while moving file => Path: " + file);
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
                        if (isLoggingEnabled) SelfLog.Logger.Exception(ex, "Error while moving file => Source:" + sourcePath + " Destination: " + destPath);
                    }

                });

            }
            else if (File.Exists(sourcePath))
            {
                try
                {
                    File.Move(sourcePath, destPath + "\\" + Path.GetFileName(sourcePath), true);
                    if (isLoggingEnabled) SelfLog.Logger.Info("File moved => Source:" + sourcePath + " Destination: " + destPath);
                }
                catch (Exception ex)
                {
                    if (isLoggingEnabled) SelfLog.Logger.Exception(ex, "Error while moving file => Source:" + sourcePath + " Destination: " + destPath);
                }
            }
            else
            {
                throw new NotExistException("Error in MoveAll: Given source path not exist.");
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
        public static void CopyAll(string sourcePath, string destPath,bool overWrite = false)
        {

            if (!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);
            if (Directory.Exists(sourcePath))
            {
                string[] files = Directory.GetFiles(sourcePath);
                string[] subdirs = Directory.GetDirectories(sourcePath);
                Parallel.ForEach(files, file =>
                {
                    File.Copy(file, destPath + "\\" + Path.GetFileName(file), overWrite);
                });
                Parallel.ForEach(subdirs, subdir =>
                {
                    CopyAll(subdir, destPath + "\\" + Path.GetFileName(subdir));

                });

            }
            else if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, destPath + "\\" + Path.GetFileName(sourcePath), overWrite);
            }
            else
            {
                throw new NotExistException("Error in CopyAll: Given source path not exist.");

            }
        }


        public static string GetFileExtension(string filename)
        {
            var index = filename.LastIndexOf('.');
            if (index == -1) return filename;
            var res = filename[(index + 1)..];
            return res;
        }
       
        public static string GetFileNameWithExtension(string filename)
        {
            var index = filename.LastIndexOf('\\');
            if (index == -1) return filename;
            var res = filename[(index + 1)..];
            return res;
        }
        public static string GetFileDirectory(string filename)
        {
            var index = filename.LastIndexOf('\\');
            if (index == -1) index = filename.LastIndexOf("/");
            if (index == -1) return string.Empty;
            var res = filename[..index];
            return res;
        }

        private const int BYTES_TO_READ = sizeof(Int64);
        public static bool FilesAreEqual(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
                return false;
            int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                byte[] one = new byte[BYTES_TO_READ];
                byte[] two = new byte[BYTES_TO_READ];

                for (int i = 0; i < iterations; i++)
                {
                    fs1.Read(one, 0, BYTES_TO_READ);
                    fs2.Read(two, 0, BYTES_TO_READ);

                    if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                        return false;
                }
            }

            return true;
        }
        public static bool FilesAreEqual_OneByte(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
                return false;

            if (string.Equals(first.FullName, second.FullName, StringComparison.OrdinalIgnoreCase))
                return true;

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                for (int i = 0; i < first.Length; i++)
                {
                    if (fs1.ReadByte() != fs2.ReadByte())
                        return false;
                }
            }

            return true;
        }

        public static bool FilesAreEqual_MD5Hash(FileInfo first, FileInfo second)
        {
            byte[] firstHash = MD5.Create().ComputeHash(first.OpenRead());
            byte[] secondHash = MD5.Create().ComputeHash(second.OpenRead());

            for (int i = 0; i < firstHash.Length; i++)
            {
                if (firstHash[i] != secondHash[i])
                    return false;
            }
            return true;
        }
        
        public static bool FilesAreEqual_XXHash(FileInfo first, FileInfo second)
        {
            var factory = xxHashFactory.Instance.Create();
            using var firstStream = File.OpenRead(first.FullName);
            var firstHash = factory.ComputeHash(firstStream).AsHexString();
            using var secondStream = File.OpenRead(second.FullName);
            var secondHash = factory.ComputeHash(secondStream).AsHexString();
            return firstHash == secondHash;
        }

        static ReaderWriterLock _locker = new();

        /// <summary>
        /// Reads file with given path and returns byte array. This does support multiple threading. Uses ReaderWriterLock by System.Threading. Allows multiple readers and a single writer
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ReadBytesSafe(string path)
        {
            try
            {
                _locker.AcquireReaderLock(int.MaxValue);
                return File.ReadAllBytes(path);
            }
            finally
            {
                _locker.ReleaseWriterLock();
            }
        }
        /// <summary>
        /// Reads file with given path and returns BaseStream. This does support multiple threading. Uses ReaderWriterLock by System.Threading. Allows multiple readers and a single writer
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Stream ReadStreamSafe(string path)
        {
            try
            {
                _locker.AcquireReaderLock(int.MaxValue);
                var reader = new StreamReader(path);
                return reader.BaseStream;
            }
            finally
            {
                _locker.ReleaseWriterLock();
            }
        }


       
        public static int DeleteDirectoryWhere(this string dirPath, Func<DirectoryInfo, bool> condition,bool recurisive)
        {
            if (!Directory.Exists(dirPath)) throw new InvalidOperationException("Directory does not exist");
            var dirs = Directory.GetDirectories(dirPath);
            var deleted = 0;
            foreach (var path in dirs)
            {
                var dir = new DirectoryInfo(path);
                if (condition(dir))
                {
                    dir.Delete(recurisive);
                    deleted++;
                }
            }
            return deleted;
        }
        public static int DeleteFileWhere(this string dirPath, Func<FileInfo, bool> condition)
        {
            if (!Directory.Exists(dirPath)) throw new InvalidOperationException("Directory does not exist");
            var files = Directory.GetFiles(dirPath);
            var deleted = 0;
            foreach(var path in files)
            {
                var file = new FileInfo(path);
                if (condition(file))
                {
                    file.Delete();
                    deleted++;
                }
            }
            return deleted;
        }
    }

}
