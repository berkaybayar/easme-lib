using System;

namespace EasMe.Models
{
    internal class BaseLogModel
    {
        public DateTime Date { get; private set; } = DateTime.Now;
        public int LogType { get; set; }
        public string Severity { get; set; }
        public string ErrorNo { get; set; }
        public object Message { get; set; }
        public string TraceAction { get; set; }
        public string TraceClass { get; set; }
        public ErrorLogModel Exception { get; set; }

    }
}
