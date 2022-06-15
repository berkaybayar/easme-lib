using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe
{
    /// <summary>
    /// Static class for fast loggin with EasLog
    /// </summary>
    public static class EasLogFast 
    {
        static EasLog _log = new EasLog();
        public static string Info(string message)
        {
            return _log.Info(message);
        }
        public static string Error(string message)
        {
            return _log.Error(message);
        }
        public static string Exception(Exception ex)
        {
            return _log.Exception(ex);
        }
        public static string Warn(string message)
        {
            return _log.Warn(message);
        }
    }
}
