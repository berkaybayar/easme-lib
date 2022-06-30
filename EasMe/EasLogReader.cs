using EasMe.Exceptions;
using EasMe.Models.LogModels;
using Newtonsoft.Json;

namespace EasMe
{

    public static class EasLogReader
    {
        public static string? LogFilePath { get; set; }
        public static string[] LogFileContent { get; set; }


        private static void CheckLoaded()
        {
            if (string.IsNullOrEmpty(LogFilePath)) throw new EasException(Error.NOT_LOADED, "Log file path not loaded.");
        }
        
        public static void Load(string logFilePath)
        {
            LogFilePath = logFilePath;
            if (!File.Exists(LogFilePath)) throw new EasException(Error.NOT_EXISTS, "Could not locate log file with given path: " + LogFilePath);
            try
            {
                var fileContent = File.ReadAllText(LogFilePath);
                string[] lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                LogFileContent = lines;
            }
            catch (Exception e)
            {
                throw new FailedToReadException("Failed reading log file with given path: " + LogFilePath, e);
            }
        }

        /// <summary>
        /// Gets deserialized list of all logs.
        /// </summary>
        /// <returns</returns>
        /// <exception cref="EasException"></exception>
        public static List<BaseLogModel> GetLogFileContent()
        {
            CheckLoaded();
            try
            {
                var list = new List<BaseLogModel>();
                foreach (var line in LogFileContent)
                {
                    var deserialized = line.Deserialize<BaseLogModel>();
                    if (deserialized == null) throw new EasException(Error.DESERIALIZATION_ERROR);
                    list.Add(deserialized);
                }
                if (list.Count == 0) throw new NotFoundException( "Failed getting log file content as List<BaseModel>, log file does not have logs recorded.");
                return list;
            }
            catch (Exception ex)
            {
                throw new DeserializationFailedException("Failed to deserialize log file content.", ex);
            }
        }
        /// <summary>
        /// Gets log file content by severity.
        /// </summary>
        /// <param name="Severity"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static List<BaseLogModel> GetLogFileContent(string Severity)
        {
            var list = GetLogFileContent();
            return list.Where(x => x.Severity == Severity).ToList();
        }
        /// <summary>
        /// Gets filtered logs by LogType.
        /// </summary>
        /// <param name="LogType"></param>
        /// <returns></returns>
        public static List<BaseLogModel> GetLogFileContent(int LogType)
        {
            var list = GetLogFileContent();
            return list.Where(x => x.LogType == LogType).ToList();
        }
        /// <summary>
        /// Gets filtered logs by TraceClass.
        /// </summary>
        /// <param name="TraceClass"></param>
        /// <returns></returns>
        public static List<BaseLogModel> GetLogFileContentByTrace(string TraceClass)
        {
            var list = GetLogFileContent();
            return list.Where(x => x.TraceClass == TraceClass).ToList();
        }
        /// <summary>
        /// Gets filtered logs by TraceClass and TraceAction.
        /// </summary>
        /// <param name="TraceClass"></param>
        /// <param name="TraceAction"></param>
        /// <returns></returns>
        public static List<BaseLogModel> GetLogFileContentByTrace(string TraceClass, string TraceAction)
        {
            var list = GetLogFileContent();
            return list.Where(x => x.TraceAction == TraceAction && x.TraceClass == TraceClass).ToList();
        }
    }
}
