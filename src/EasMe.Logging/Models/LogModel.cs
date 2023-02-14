using log4net;
using Newtonsoft.Json;
using System;
using EasMe.Logging.Internal;

namespace EasMe.Logging.Models
{
    public class LogModel
    {
        private LogModel()
        {

        }
        public LogModel(LogSeverity severity, string source, object log, Exception? exception = null)
        {
            Severity = severity;
            Source = source;
            Log = log;
            LogType = (int)Logging.LogType.BASE;
            if (EasLogFactory.Config.TraceLogging || severity == LogSeverity.TRACE)
            {
                TraceMethod = EasLogHelper.GetActionName();
                TraceClass = EasLogHelper.GetClassName();
            }
            if (EasLogFactory.Config.WebInfoLogging)
            {
                WebLog = new WebLogModel();
                LogType = (int)Logging.LogType.WEB;
                if (EasLogFactory.Config.AddRequestUrlToStart)
                {
                    var conAndAction = WebLog?.RequestUrl?.Replace("/api", "");
                    if (conAndAction != null)
                    {
                        if (conAndAction.StartsWith("/")) conAndAction = conAndAction[1..];
                        if (conAndAction.Length != 0)
                            Log = $"[{conAndAction ?? "UnknownUrl"}] " + Log;
                    }
                }
            }
            if (exception != null)
            {
                if (EasLogFactory.Config.ExceptionHideSensitiveInfo) Exception = new Exception(exception.Message);
                else Exception = exception;
                LogType = (int)Logging.LogType.EXCEPTION;
            }
        }
        public DateTime Date { get; private set; } = DateTime.Now;
        public int LogType { get; set; }
        public string SeverityStr => Severity.ToString();
        public LogSeverity Severity { get; set; }
        public string? Source { get; set; }
        public object? Log { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? TraceMethod { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? TraceClass { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Exception? Exception { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WebLogModel? WebLog { get; set; }

    }
}
