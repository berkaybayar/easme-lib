using EasMe.Models.LogModels;
using Newtonsoft.Json;

namespace EasMe
{

    public static class EasLogReader
    {
        private static string? _logFilePath;
        private static string? _logFileContent;

        private static void CheckIfLoaded()
        {
            if (string.IsNullOrEmpty(_logFilePath)) throw new EasException(Error.NULL_REFERENCE, "Log file path not loaded.");
            if (string.IsNullOrEmpty(_logFileContent)) throw new EasException(Error.NULL_REFERENCE, "Log file content not loaded or NULL.");
        }
        public static void Load(string logFilePath)
        {
            _logFilePath = logFilePath;
            if (!File.Exists(_logFilePath)) throw new EasException(Error.NOT_EXISTS, "Could not locate log file with given path. => Path:" + _logFilePath);
            try
            {
                _logFileContent = File.ReadAllText(_logFilePath);
                if (string.IsNullOrEmpty(_logFileContent)) throw new EasException(Error.NULL_REFERENCE, "Error occured while loading log file, Log file content is NULL.");
            }
            catch (Exception e)
            {
                throw new EasException(Error.FAILED_TO_READ, "Failed reading log file with given path => Path:" + _logFilePath, e);
            }
        }
        /// <summary>
        /// Gets all logs in string array.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static string[] GetLogFileContentAsString()
        {
            if (string.IsNullOrEmpty(_logFileContent)) throw new EasException(Error.NULL_REFERENCE, "Failed getting log file content as string, Log file content is NULL");
            string[] lines = _logFileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return lines;
        }
        /// <summary>
        /// Gets deserialized list of all logs.
        /// </summary>
        /// <returns</returns>
        /// <exception cref="EasException"></exception>
        public static List<BaseLogModel> GetLogFileContent()
        {
            try
            {
                var list = new List<BaseLogModel>();
                string[] lines = _logFileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (var line in lines)
                {
                    var deserialized = JsonConvert.DeserializeObject<BaseLogModel>(line);
                    if (deserialized == null) throw new EasException(Error.DESERIALIZATION_ERROR);
                    list.Add(deserialized);
                }
                if (list.Count == 0) throw new EasException(Error.NOT_FOUND, "Failed getting log file content as List<BaseModel>, log file does not have logs recorded.");
                return list;
            }
            catch (Exception ex)
            {
                throw new EasException(Error.DESERIALIZATION_ERROR, ex);
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
