using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe
{
    internal static class SelfLog
    {
        internal static EasLog Logger { get; set; } = IEasLog.CreateLogger("EasMe.SelfLogging");
        
    }
}
