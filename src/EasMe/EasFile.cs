using System.Data.HashFunction.xxHash;
using System.Security.Cryptography;

namespace EasMe;

/// <summary>
///     File or folder deletion with logging options, uses EasLog
/// </summary>
public static class EasFile
{
    private const int BytesToRead = sizeof(long);

    private static readonly ReaderWriterLock _locker = new();

    //public static string DirCurrent = Directory.GetCurrentDirectory();//gets current directory 
    //public static string DirUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);//gets user directory C:\Users\%username%
    //public static string DirDesktop = DirUser + "\\Desktop";//gets desktop directory C:\Users\%username%\desktop
    //public static string DirAppData = DirUser + "\\AppData\\Roaming";//gets appdata directory C:\Users\%username%\AppData\\Roaming
    //public static string DirSystem = Environment.GetFolderPath(Environment.SpecialFolder.System);//gets system32 directory C:\Windows\System32
    //public static string DirLog = DirCurrent + "\\Logs\\"; //log file directory


    /// <summary>
    ///     Deletes file or folder, if it is folder it will delete all files and subfolders.
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="isLog"></param>
    public static void DeleteAll(string filePath) {
        if (Directory.Exists(filePath))
            Directory.Delete(filePath, true);
        else if (File.Exists(filePath))
            File.Delete(filePath);
        else
            throw new FileNotFoundException("File or folder not found", filePath);
    }

    /// <summary>
    ///     Moves all files to destination folder path. If source path is folder moves all files and sub folders inside not the
    ///     actual folder.
    /// </summary>
    /// <param name="sourcePath"></param>
    /// <param name="destPath"></param>
    /// <param name="overwrite"></param>
    public static void MoveAll(string sourcePath, string destPath, bool overwrite) {
        if (!Directory.Exists(destPath)) Directory.CreateDirectory(destPath);
        if (Directory.Exists(sourcePath)) {
            var files = Directory.GetFiles(sourcePath);
            var subdirs = Directory.GetDirectories(sourcePath);
            Parallel.ForEach(files, file => { File.Move(file, destPath + "\\" + Path.GetFileName(file), true); });
            Parallel.ForEach(subdirs,
                             subdir => { MoveAll(subdir, destPath + "\\" + Path.GetFileName(subdir), overwrite); });
        }
        else if (File.Exists(sourcePath)) {
            File.Move(sourcePath, destPath + "\\" + Path.GetFileName(sourcePath), true);
        }
        else {
            throw new FileNotFoundException("Given source path not exist.", sourcePath);
        }
    }

    /// <summary>
    ///     Moves all file(s) to destination folder path. If source path is folder moves all files and sub folders inside not
    ///     the actual folder.
    /// </summary>
    /// <param name="sourcePath"></param>
    /// <param name="destinationPath"></param>
    public static void CopyAll(string sourcePath, string destinationPath, bool overwrite = false) {
        if (!Directory.Exists(destinationPath)) Directory.CreateDirectory(destinationPath);
        if (Directory.Exists(sourcePath)) {
            var files = Directory.GetFiles(sourcePath);
            var subdirs = Directory.GetDirectories(sourcePath);
            Parallel.ForEach(files, file => { File.Copy(file, destinationPath + "\\" + Path.GetFileName(file), overwrite); });
            Parallel.ForEach(subdirs, subdir => { CopyAll(subdir, destinationPath + "\\" + Path.GetFileName(subdir)); });
        }
        else if (File.Exists(sourcePath)) {
            File.Copy(sourcePath, destinationPath + "\\" + Path.GetFileName(sourcePath), overwrite);
        }
        else {
            throw new FileNotFoundException("Given source path not exist.", sourcePath);
        }
    }


 
    public static string GetFileDirectory(string filename) {
        var index = filename.LastIndexOf('\\');
        if (index == -1) index = filename.LastIndexOf("/");
        if (index == -1) return string.Empty;
        var res = filename[..index];
        return res;
    }

    public static bool FilesAreEqual(FileInfo first, FileInfo second) {
        if (first.Length != second.Length)
            return false;
        var iterations = (int)Math.Ceiling((double)first.Length / BytesToRead);
        using var fs1 = first.OpenRead();
        using var fs2 = second.OpenRead();
        var one = new byte[BytesToRead];
        var two = new byte[BytesToRead];
        for (var i = 0; i < iterations; i++) {
            var r1 = fs1.Read(one, 0, BytesToRead);
            var r2 = fs2.Read(two, 0, BytesToRead);
            if (r1 != r2)
                return false;
        }

        return true;
    }

    public static bool FilesAreEqual_OneByte(FileInfo first, FileInfo second) {
        if (first.Length != second.Length)
            return false;

        if (string.Equals(first.FullName, second.FullName, StringComparison.OrdinalIgnoreCase))
            return true;

        using var fs1 = first.OpenRead();
        using var fs2 = second.OpenRead();
        for (var i = 0; i < first.Length; i++)
            if (fs1.ReadByte() != fs2.ReadByte())
                return false;

        return true;
    }

    public static bool FilesAreEqual_MD5Hash(FileInfo first, FileInfo second) {
        var firstHash = MD5.Create().ComputeHash(first.OpenRead());
        var secondHash = MD5.Create().ComputeHash(second.OpenRead());
        return !firstHash.Where((t, i) => t != secondHash[i]).Any();
    }

    public static bool FilesAreEqual_XXHash(FileInfo first, FileInfo second) {
        var factory = xxHashFactory.Instance.Create();
        using var firstStream = File.OpenRead(first.FullName);
        var firstHash = factory.ComputeHash(firstStream).AsHexString();
        using var secondStream = File.OpenRead(second.FullName);
        var secondHash = factory.ComputeHash(secondStream).AsHexString();
        return firstHash == secondHash;
    }

    // /// <summary>
    // ///     Reads file with given path and returns byte array. This does support multiple threading. Uses ReaderWriterLock by
    // ///     System.Threading. Allows multiple readers and a single writer
    // /// </summary>
    // /// <param name="path"></param>
    // /// <returns></returns>
    // public static byte[] ReadBytesSafe(string path) {
    //     try {
    //         _locker.AcquireReaderLock(int.MaxValue);
    //         return File.ReadAllBytes(path);
    //     }
    //     finally {
    //         _locker.ReleaseWriterLock();
    //     }
    // }
    //
    // /// <summary>
    // ///     Reads file with given path and returns BaseStream. This does support multiple threading. Uses ReaderWriterLock by
    // ///     System.Threading. Allows multiple readers and a single writer
    // /// </summary>
    // /// <param name="path"></param>
    // /// <returns></returns>
    // public static Stream ReadStreamSafe(string path) {
    //     try {
    //         _locker.AcquireReaderLock(int.MaxValue);
    //         var reader = new StreamReader(path);
    //         return reader.BaseStream;
    //     }
    //     finally {
    //         _locker.ReleaseWriterLock();
    //     }
    // }


    public static void DeleteDirectoryWhere(this string dirPath, Func<DirectoryInfo, bool> condition, bool recurisive) {
        if (!Directory.Exists(dirPath)) throw new InvalidOperationException("Directory does not exist");
        var dirs = Directory.GetDirectories(dirPath);
        foreach (var path in dirs) {
            var dir = new DirectoryInfo(path);
            if (condition(dir)) {
                dir.Delete(recurisive);
            }
        }

    }

    public static void DeleteFileWhere(this string dirPath, Func<FileInfo, bool> condition) {
        if (!Directory.Exists(dirPath)) throw new InvalidOperationException("Directory does not exist");
        var files = Directory.GetFiles(dirPath);
        foreach (var path in files) {
            var file = new FileInfo(path);
            if (condition(file)) {
                file.Delete();
            }
        }

    }
}