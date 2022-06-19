using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models.LogModels
{
    internal static class LogType
    {
        public enum TypeList
        {
            Debug = -2,
            Error = -1,
            Base = 0,
            Web = 1,
            Client = 2,
            
        }
    }
}
