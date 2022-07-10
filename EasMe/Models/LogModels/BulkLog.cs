using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models.LogModels
{
    public class BulkLog
    {
        public Severity Severity { get; set; }
        public object Log { get; set; } = "";
        public Exception? Exception { get; set; }
    }
}
