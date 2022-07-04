using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe
{
    public interface IEasLog
    {
        
        internal static EasLogConfiguration Config { get; set; } = new EasLogConfiguration();
       
        /// <summary>
        /// Creates logger with given LogSource variable.
        /// </summary>
        /// <param name="logSource"></param>
        /// <returns></returns>
        public static EasLog CreateLogger(string logSource)
        {
            //SelfLog.Logger.Info("Logger Created: " + logSource);
            return new EasLog(logSource);
        }
        
        /// <summary>
        /// EasLog logging configuration. Call this method in your startup. If you don't call it it will use default values.
        /// </summary>
        /// <param name="config"></param>
        public static void LoadConfig(EasLogConfiguration config)
        {
            SelfLog.Logger.Info("Logger config loaded: "+ config.JsonSerialize());
            Config = config;
        }
        
    }
}
