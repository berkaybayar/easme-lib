
using EasMe.Exceptions;
using EasMe.Extensions;
using EasMe.InternalUtils;
using EasMe.Models.LogModels;
using log4net;
using System.Reflection;
using System.Runtime.CompilerServices;
namespace EasMe
{

    /*
     Things to keep note;
    1. This class is not thread safe.
    2. This logger will work on multiple threads
    3. For .NET Web projects it is better to initialize HttpContext in the constructor. 
        This will print out the request information. 
        Which contains request url etc. so you dont really need to put method name in log
     */

    //private static readonly EasLog logger = IEasLog.CreateLogger(typeof(AdminManager).Namespace + "." + typeof(AdminManager).Name);
    /// <summary>
    /// Simple static json and console logger with useful options.
    /// </summary>
    public sealed class EasLog
    {
        //internal static EasLogConfiguration Configuration { get; set; } = IEasLog.Config;
        private static int? _OverSizeExt = 0;

        internal static bool _IsCreated = false;
        internal string _LogSource;
        private static readonly object _fileLock = new object();
        internal EasLog(string source)
        {
            _LogSource = source;
        }
        internal EasLog()
        {
            _LogSource = "Sys";
        }
        private static string GetExactLogPath()
        {
            return Path.Combine(EasLogFactory.Config.LogFolderPath, EasLogFactory.Config.LogFileName + DateTime.Now.ToString(EasLogFactory.Config.DateFormatString) + EasLogFactory.Config.LogFileExtension);
        }
        private static string GetExactLogPath(Severity severity)
        {
            if (EasLogFactory.Config.SeperateLogLevelToFolder)
            {
				return Path.Combine(EasLogFactory.Config.LogFolderPath, DateTime.Now.ToString(EasLogFactory.Config.DateFormatString), EasLogFactory.Config.LogFileName + severity.ToString() + EasLogFactory.Config.LogFileExtension);
            }
			return Path.Combine(EasLogFactory.Config.LogFolderPath, EasLogFactory.Config.LogFileName + DateTime.Now.ToString(EasLogFactory.Config.DateFormatString) + EasLogFactory.Config.LogFileExtension);
        }

		public void Info(params object[] param)
        {
			if (!Severity.INFO.IsLoggable()) return;
			WriteLog(Severity.INFO, null, param);
        }
		public void Info(object obj1)
		{
			if (!Severity.INFO.IsLoggable()) return;
			WriteLog(Severity.INFO, null, obj1);
		}
		public void Info(object obj1,object obj2)
		{
			if (!Severity.INFO.IsLoggable()) return;
			WriteLog(Severity.INFO, null, obj1,obj2 );
		}
		public void Info(object obj1, object obj2, object obj3)
		{
			if (!Severity.INFO.IsLoggable()) return;
			WriteLog(Severity.INFO, null, obj1, obj2,obj3);
		}
		public void Info(object obj1, object obj2, object obj3,object obj4)
		{
			if (!Severity.INFO.IsLoggable()) return;
			WriteLog(Severity.INFO, null, obj1, obj2, obj3,obj4);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4,object obj5)
		{
			if (!Severity.INFO.IsLoggable()) return;
			WriteLog(Severity.INFO, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4, object obj5,object obj6)
		{
			if (!Severity.INFO.IsLoggable()) return;
			WriteLog(Severity.INFO, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!Severity.INFO.IsLoggable()) return;
			WriteLog(Severity.INFO, null, obj1, obj2, obj3, obj4, obj5, obj6,obj7);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!Severity.INFO.IsLoggable()) return;
			WriteLog(Severity.INFO, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}
		
		public void Error(params object[] param)
        {
			if (!Severity.ERROR.IsLoggable()) return;
			WriteLog(Severity.ERROR, null, param);
        }
		public void Error(object obj1)
		{
			if (!Severity.ERROR.IsLoggable()) return;
			WriteLog(Severity.ERROR, null, obj1);
		}
		public void Error(object obj1, object obj2)
		{
			if (!Severity.ERROR.IsLoggable()) return;
			WriteLog(Severity.ERROR, null, obj1, obj2);
		}
		public void Error(object obj1, object obj2, object obj3)
		{
			if (!Severity.ERROR.IsLoggable()) return;
			WriteLog(Severity.ERROR, null, obj1, obj2, obj3);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4)
		{
			if (!Severity.ERROR.IsLoggable()) return;
			WriteLog(Severity.ERROR, null, obj1, obj2, obj3, obj4);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!Severity.ERROR.IsLoggable()) return;
			WriteLog(Severity.ERROR, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!Severity.ERROR.IsLoggable()) return;
			WriteLog(Severity.ERROR, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!Severity.ERROR.IsLoggable()) return;
			WriteLog(Severity.ERROR, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!Severity.ERROR.IsLoggable()) return;
			WriteLog(Severity.ERROR, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7,obj8);
		}
		
        public void Warn(params object[] param)
        {
			if (!Severity.WARN.IsLoggable()) return;
			WriteLog(Severity.WARN, null, param);
        }

		public void Warn(object obj1)
		{
			if (!Severity.WARN.IsLoggable()) return;
			WriteLog(Severity.WARN, null, obj1);
		}
		public void Warn(object obj1, object obj2)
		{
			if (!Severity.WARN.IsLoggable()) return;
			WriteLog(Severity.WARN, null, obj1, obj2);
		}
		public void Warn(object obj1, object obj2, object obj3)
		{
			if (!Severity.WARN.IsLoggable()) return;
			WriteLog(Severity.WARN, null, obj1, obj2, obj3);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4)
		{
			if (!Severity.WARN.IsLoggable()) return;
			WriteLog(Severity.WARN, null, obj1, obj2, obj3, obj4);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!Severity.WARN.IsLoggable()) return;
			WriteLog(Severity.WARN, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!Severity.WARN.IsLoggable()) return;
			WriteLog(Severity.WARN, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!Severity.WARN.IsLoggable()) return;
			WriteLog(Severity.WARN, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!Severity.WARN.IsLoggable()) return;
			WriteLog(Severity.WARN, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}

        
		public void Exception(Exception ex)
        {
			if (!Severity.EXCEPTION.IsLoggable()) return;
			WriteLog(Severity.EXCEPTION, ex);
        }

        public void Exception(Exception ex, params object[] param)
        {
			if (!Severity.EXCEPTION.IsLoggable()) return;
			WriteLog(Severity.EXCEPTION, ex, param);
        }
		public void Exception(Exception ex, object obj1)
		{
			if (!Severity.EXCEPTION.IsLoggable()) return;
			WriteLog(Severity.EXCEPTION, ex, obj1);
		}
		public void Exception(Exception ex, object obj1, object obj2)
		{
			if (!Severity.EXCEPTION.IsLoggable()) return;
			WriteLog(Severity.EXCEPTION, ex, obj1, obj2);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3)
		{
			if (!Severity.EXCEPTION.IsLoggable()) return;
			WriteLog(Severity.EXCEPTION, ex, obj1, obj2, obj3);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4)
		{
			if (!Severity.EXCEPTION.IsLoggable()) return;
			WriteLog(Severity.EXCEPTION, ex, obj1, obj2, obj3, obj4);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!Severity.EXCEPTION.IsLoggable()) return;
			WriteLog(Severity.EXCEPTION, ex, obj1, obj2, obj3, obj4, obj5);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!Severity.EXCEPTION.IsLoggable()) return;
			WriteLog(Severity.EXCEPTION, ex, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!Severity.EXCEPTION.IsLoggable()) return;
			WriteLog(Severity.EXCEPTION, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!Severity.EXCEPTION.IsLoggable()) return;
			WriteLog(Severity.EXCEPTION, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}





		public void Fatal(params object[] param)
        {
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, null, param);
        }
		public void Fatal(object obj1)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, null, obj1);
		}
		public void Fatal(object obj1, object obj2)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, null, obj1, obj2);
		}
		public void Fatal(object obj1, object obj2, object obj3)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, null, obj1, obj2, obj3);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, null, obj1, obj2, obj3, obj4);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}




		public void Fatal(Exception ex, params object[] param)
        {
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, ex, param);

        }
		public void Fatal(Exception ex, object obj1)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, ex, obj1);
		}
		public void Fatal(Exception ex, object obj1, object obj2)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, ex, obj1, obj2);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, ex, obj1, obj2, obj3);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, ex, obj1, obj2, obj3, obj4);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, ex, obj1, obj2, obj3, obj4, obj5);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, ex, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!Severity.FATAL.IsLoggable()) return;
			WriteLog(Severity.FATAL, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}






		public void Debug(params object[] param)
        {
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, null, param);
        }

		public void Debug(object obj1)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, null, obj1);
		}
		public void Debug(object obj1, object obj2)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, null, obj1, obj2);
		}
		public void Debug(object obj1, object obj2, object obj3)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, null, obj1, obj2, obj3);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, null, obj1, obj2, obj3, obj4);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}
		public void Debug(Exception ex, params object[] param)
        {
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, ex, param);
        }
		public void Debug(Exception ex, object obj1)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, ex, obj1);
		}
		public void FaDebugtal(Exception ex, object obj1, object obj2)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, ex, obj1, obj2);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, ex, obj1, obj2, obj3);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, ex, obj1, obj2, obj3, obj4);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, ex, obj1, obj2, obj3, obj4, obj5);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, ex, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!Severity.DEBUG.IsLoggable()) return;
			WriteLog(Severity.DEBUG, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}



		public void Trace(params object[] param)
        {
			if (!Severity.TRACE.IsLoggable()) return;
			WriteLog(Severity.TRACE, null, param);
        }
		public void Trace(object obj1)
		{
			if (!Severity.TRACE.IsLoggable()) return;
			WriteLog(Severity.TRACE, null, obj1);
		}
		public void Trace(object obj1, object obj2)
		{
			if (!Severity.TRACE.IsLoggable()) return;
			WriteLog(Severity.TRACE, null, obj1, obj2);
		}
		public void Trace(object obj1, object obj2, object obj3)
		{
			if (!Severity.TRACE.IsLoggable()) return;
			WriteLog(Severity.TRACE, null, obj1, obj2, obj3);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4)
		{
			if (!Severity.TRACE.IsLoggable()) return;
			WriteLog(Severity.TRACE, null, obj1, obj2, obj3, obj4);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!Severity.TRACE.IsLoggable()) return;
			WriteLog(Severity.TRACE, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!Severity.TRACE.IsLoggable()) return;
			WriteLog(Severity.TRACE, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!Severity.TRACE.IsLoggable()) return;
			WriteLog(Severity.TRACE, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!Severity.TRACE.IsLoggable()) return;
			WriteLog(Severity.TRACE, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}


		private void WriteLog(Severity severity, Exception? exception = null, params object[] param)
        {
            Task.Run(() =>
            {
                if (EasLogFactory.Config.DontLog) return;
                
                var log = "";
                var paramToLog = param.ToLogString();
                if (EasLogFactory.Config.IsLogJson)
                {
                    var model = EasLogHelper.LogModelCreate(severity, _LogSource, paramToLog, exception);
                    log = model.JsonSerialize();
                }
                else
                {
                    var dateStr = DateTime.Now.ToString(EasLogFactory.Config.DateFormatString);
                    log = $"[{dateStr}] [{severity}] " + paramToLog;
                    if (exception != null) log = log + " Exception:" + exception.Message;
                }
                try
                {
                    if (EasLogFactory.Config.ConsoleAppender) EasLogConsole.Log(severity, log);
                    if (EasLogFactory.Config.StackLogCount > 0)
                    {
                        if (EasLogFactory.Config.StackLogCount >= _stackLogs.Count)
                        {
                            WriteLines(_stackLogs);
                            _stackLogs.Clear();
                        }
                        else
                        {
                            lock (_stackLogs)
                            {
                                _stackLogs.Add(log);
                            }
                        }
                    }
                    else
                    {
                        WriteToFile(severity, log);
                    }
                }
                catch (Exception ex)
                {
                    lock (Errors)
                    {
                        Errors.Add(ex);
                    }
                }
            });
            void WriteToFile(Severity severity, string log)
            {
                var logFilePath = GetExactLogPath(severity);
                Console.WriteLine(logFilePath);
                var folderPath = Path.GetDirectoryName(logFilePath);
				Console.WriteLine(folderPath);
				if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
                lock (_fileLock)
                {
                    File.AppendAllText(logFilePath, log + "\n");
                }
            }
        }

        private void WriteLines(List<string> logs)
        {
            if (!Directory.Exists(EasLogFactory.Config.LogFolderPath)) Directory.CreateDirectory(EasLogFactory.Config.LogFolderPath);
            lock (_fileLock)
            {
                File.WriteAllLines(GetExactLogPath(), logs);
            }
        }

        private static List<string> _stackLogs = new();
        public static List<Exception> Errors { get; set; } = new();

        /// <summary>
        /// This method must be called before application exit. If 
        /// </summary>
        public void Flush()
        {
            if (EasLogFactory.Config.StackLogCount > 0)
                WriteLines(_stackLogs);
        }

    }

}

