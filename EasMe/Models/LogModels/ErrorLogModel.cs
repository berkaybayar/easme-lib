using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models.LogModels
{
    internal class ErrorLogModel
    {
        
        public string? ExceptionMessage { get; set; }   
        public string? ExceptionInner { get; set; }
        public string? ExceptionSource { get; set; }
        public string? ExceptionStackTrace { get; set; }
    }
}
