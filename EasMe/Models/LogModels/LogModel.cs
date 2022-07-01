using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace EasMe.Models.LogModels
{
    public class LogModel
    {
        public DateTime Date { get; private set; } = DateTime.Now;
        public int LogType { get; set; }
        
        public string? Severity { get; set; }
        public object? Log { get; set; }
        public string? ThreadId { get; private set; } = EasSystem.GetThreadId();
                
        public string? TraceMethod { get; set; } = "NotEnabled";
        
        public string? TraceClass { get; set; } = "NotEnabled";

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Exception? Exception { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public WebLogModel? WebLog { get; set; }

    }
}
