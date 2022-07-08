using Newtonsoft.Json;
namespace EasMe.Models.LogModels
{
    public class LogModel
    {
        public DateTime Date { get; private set; } = DateTime.Now;
        public int LogType { get; set; }

        public string? LogLevel { get; set; }
        public string? Source { get; set; }
        public object? Log { get; set; }
        public string? ThreadId { get; private set; } = EasSystem.GetThreadId();

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
