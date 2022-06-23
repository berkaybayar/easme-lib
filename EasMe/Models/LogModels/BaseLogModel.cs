using System.Text.Json.Serialization;

namespace EasMe.Models.LogModels
{
    public class BaseLogModel
    {
        public DateTime Date { get; private set; } = DateTime.Now;
        public int LogType { get; set; }
        public string Logger { get; set; } = "EasLog";
        public string Severity { get; set; }
        public string? ErrorNo { get; set; }
        public object? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TraceAction { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TraceClass { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ErrorLogModel? Exception { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public WebLogModel? WebLog { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ClientLogModel? ClientLog { get; set; }

    }
}
