using System.IO.Compression;

namespace Domain.Utils
{
    public static class EasZip
    {
        public static void MakeZip(string[] files, string destination)
        {
            var index = destination.LastIndexOf("\\");
            var destinationFolder = destination[..index];
            if (!Directory.Exists(destinationFolder)) Directory.CreateDirectory(destinationFolder);

            using var archive = ZipFile.Open(destination, ZipArchiveMode.Create);
            foreach (var fPath in files)
            {
                archive.CreateEntryFromFile(fPath, Path.GetFileName(fPath));
            }
        }
        public static void UnZip(string sourceZip, string extractFolder)
        {
            var index = extractFolder.LastIndexOf("\\");
            var destinationFolder = extractFolder[..index];
            if (!Directory.Exists(destinationFolder)) Directory.CreateDirectory(destinationFolder);
            ZipFile.ExtractToDirectory(sourceZip, extractFolder);
        }
    }
}
