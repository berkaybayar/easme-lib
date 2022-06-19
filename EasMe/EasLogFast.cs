using EasMe.Models;
using EasMe.Models.LogModels;

namespace EasMe
{
    /// <summary>
    /// Static class for fast loggin with EasLog, uses default LogConfiguartion.
    /// </summary>
    public static class EasLogFast 
    {
        static EasLog _log = new EasLog(new LogConfiguration());
        public static string Info(string Message)
        {
            return _log.Info(Message);
        }       
        public static string Error(string Message,ErrorType.TypeList ErrorNo = ErrorType.TypeList.ERROR)
        {
            return _log.Error(Message);
        }
        
        public static string Error(Exception ex)
        {
            return _log.Error(ex);
        }
        public static string Warn(string Message)
        {
            return _log.Warn(Message);
        }
        
        public static string Serialize(object obj)
        {
            return _log.Serialize(obj);
        }
        
    }
}
