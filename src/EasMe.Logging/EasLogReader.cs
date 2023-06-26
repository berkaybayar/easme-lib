using EasMe.Extensions;
using EasMe.Logging.Models;

namespace EasMe.Logging;

public static class EasLogReader
{
    public static string? LogFilePath { get; set; }
    public static string[] LogFileContent { get; set; }


    private static void CheckLoaded() {
        if (string.IsNullOrEmpty(LogFilePath)) throw new Exception("Log file path not loaded.");
    }

    public static void Load(string logFilePath) {
        LogFilePath = logFilePath;
        if (!File.Exists(LogFilePath)) throw new Exception("Could not locate log file with given path: " + LogFilePath);
        try {
            var fileContent = File.ReadAllText(LogFilePath);
            var lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            LogFileContent = lines;
        }
        catch (Exception e) {
            throw new Exception("Failed reading log file with given path: " + LogFilePath, e);
        }
    }

    /// <summary>
    ///     Gets deserialized list of all logs.
    /// </summary>
    /// <returns
    /// </returns>
    /// <exception cref="EasException"></exception>
    public static List<LogModel> GetLogFileContent() {
        CheckLoaded();
        try {
            var list = new List<LogModel>();
            foreach (var line in LogFileContent) {
                var deserialized = line.FromJsonString<LogModel>();
                if (deserialized == null) throw new Exception("Failed to deseralize");
                list.Add(deserialized);
            }

            if (list.Count == 0)
                throw new Exception(
                                    "Failed getting log file content as List<BaseModel>, log file does not have logs recorded.");
            return list;
        }
        catch (Exception ex) {
            throw new Exception("Failed to deserialize log file content.", ex);
        }
    }

    /// <summary>
    ///     Gets log file content by LogLevel.
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    /// <exception cref="EasException"></exception>
    public static List<LogModel> GetLogFileContent(EasLogLevel logLevel) {
        var list = GetLogFileContent();
        return list.Where(x => x.Level == logLevel).ToList();
    }
}