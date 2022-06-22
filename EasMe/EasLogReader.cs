using EasMe.Models.LogModels;
using Newtonsoft.Json;

namespace EasMe
{

    public class EasLogReader
    {
        private string? _logFilePath;
        private string? _logFileContent;
        public EasLogReader(string logFilePath)
        {
            _logFilePath = logFilePath;
            if (!File.Exists(_logFilePath)) throw new EasException(Error.FILE_NOT_EXIST, _logFilePath);
            try
            {
                _logFileContent = File.ReadAllText(_logFilePath);
                if (string.IsNullOrEmpty(_logFileContent)) throw new EasException(Error.NULL_REFERENCE, "[EasLogReader] Log file content");
            }
            catch (Exception e)
            {
                throw new EasException(Error.FAILED_TO_READ_FILE, _logFilePath, e);
            }
        }
        /// <summary>
        /// Gets all logs in string array.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public string[] GetLogFileContentAsString()
        {
            if (string.IsNullOrEmpty(_logFileContent)) throw new EasException(Error.NULL_REFERENCE, "[GetLogFileContentAsString] Log file content");
            string[] lines = _logFileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return lines;
        }
        /// <summary>
        /// Gets deserialized list of all logs.
        /// </summary>
        /// <returns</returns>
        /// <exception cref="EasException"></exception>
        public List<BaseLogModel> GetLogFileContent()
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
                if (list.Count == 0) throw new EasException(Error.NOT_FOUND, "[GetLogFileContent] logs not found");
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
        public List<BaseLogModel> GetLogFileContent(string Severity)
        {
            var list = GetLogFileContent();
            return list.Where(x => x.Severity == Severity).ToList();
        }
        /// <summary>
        /// Gets filtered logs by LogType.
        /// </summary>
        /// <param name="LogType"></param>
        /// <returns></returns>
        public List<BaseLogModel> GetLogFileContent(int LogType)
        {
            var list = GetLogFileContent();
            return list.Where(x => x.LogType == LogType).ToList();
        }
        /// <summary>
        /// Gets filtered logs by TraceClass.
        /// </summary>
        /// <param name="TraceClass"></param>
        /// <returns></returns>
        public List<BaseLogModel> GetLogFileContentByTrace(string TraceClass)
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
        public List<BaseLogModel> GetLogFileContentByTrace(string TraceClass, string TraceAction)
        {
            var list = GetLogFileContent();
            return list.Where(x => x.TraceAction == TraceAction && x.TraceClass == TraceClass).ToList();
        }
    }
}
