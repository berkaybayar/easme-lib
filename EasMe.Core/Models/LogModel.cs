using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models
{
    public class LogModel
    {
        public DateTime Date { get; private set; } = DateTime.Now;
        public string Severity { get; set; }
        public string? Status { get; set; }
        public string? TraceAction { get; set; }
        public string? TraceClass { get; set; }
        public object? Message { get; set; }
        public int? ErrorNo { get; set; }        
        public string? Ip { get; set; }
        public string? HttpMethod { get; set; }
        public string? RequestUrl { get; set; }
        public string? Headers { get; set; }
        public string? ExceptionMessage { get; set; }
        public string? ExceptionInner { get; set; }
        public string? ExceptionSource { get; set; }
        public string? ExceptionStackTrace { get; set; }
        public string? HWID { get; set; }    

    }

 
}
