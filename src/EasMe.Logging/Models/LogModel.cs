using EasMe.Logging.Internal;
using EasMe.Models;
using Newtonsoft.Json;

namespace EasMe.Logging.Models;

public class LogModel
{
    private LogModel()
    {
    }

    private LogModel(EasLogLevel logLevel, string source, object log, Exception? exception = null,
        WebInfo? webInfo = null)
    {
        Level = logLevel;
        Source = source;
        Log = log;
        if (EasLogFactory.Config.TraceLogging || logLevel == EasLogLevel.Trace)
        {
            var traceInfo = EasLogHelper.GetTraceInfo();
            TraceMethod = traceInfo.MethodName;
            TraceClass = traceInfo.ClassName;
        }

        WebInfo = webInfo;
        if (exception == null) return;
        Exception = EasLogFactory.Config.ExceptionHideSensitiveInfo
            ? new CleanException(exception.Message)
            : new CleanException(exception);
    }

    public DateTime Date { get; } = DateTime.Now;
    public EasLogLevel Level { get; set; }
    public string? Source { get; set; }
    public object? Log { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? TraceMethod { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? TraceClass { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public CleanException? Exception { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public WebInfo? WebInfo { get; set; }

    public static LogModel Create(EasLogLevel logLevel, string source, object log, Exception? exception = null,
        WebInfo? webInfo = null)
    {
        return new LogModel(logLevel, source, log, exception, webInfo);
    }
}