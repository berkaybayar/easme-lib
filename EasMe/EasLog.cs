
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
    public sealed class EasLog : IEasLog
    {
        //internal static EasLogConfiguration Configuration { get; set; } = IEasLog.Config;
        private static int? _OverSizeExt = 0;
        private static string ExactLogPath { get; set; } = GetExactLogPath();

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
            return IEasLog.Config.LogFolderPath + "\\" + IEasLog.Config.LogFileName + DateTime.Now.ToString(IEasLog.Config.DateFormatString) + IEasLog.Config.LogFileExtension;
        }

        public void Log(Severity severity, params object[] param)
        {
            WriteLog(severity, null, param);
        }
        public void WriteAll(Severity severity, IEnumerable<string> logArray)
        {
            foreach (var log in logArray)
            {
                var model = EasLogHelper.LogModelCreate(severity, _LogSource, log, null);
                WriteLog(severity, model.JsonSerialize());
            }
        }
        public void WriteAll(List<BulkLog> logs)
        {
            foreach (var log in logs)
            {
                var model = EasLogHelper.LogModelCreate(log.Severity, _LogSource, log.Log, log.Exception);
                WriteLog(log.Severity, model.JsonSerialize());
            }
        }

        public void Info(params object[] param)
        {
            WriteLog(Severity.INFO, null, param);
        }

        public void Error(params object[] param)
        {
            WriteLog(Severity.ERROR, null, param);
        }

        public void Warn(params object[] param)
        {
            WriteLog(Severity.WARN, null, param);
        }

        public void Exception(Exception ex)
        {
            WriteLog(Severity.EXCEPTION, ex);
        }

        public void Exception(Exception ex, params object[] param)
        {
            WriteLog(Severity.EXCEPTION, ex, param);
        }

        public void Fatal(params object[] param)
        {
            WriteLog(Severity.FATAL, null, param);
        }
        public void Fatal(Exception ex, params object[] param)
        {
            WriteLog(Severity.FATAL, ex, param);

        }

        public void Debug(params object[] param)
        {
            WriteLog(Severity.DEBUG, null, param);
        }
        public void Debug(Exception ex, params object[] param)
        {
            WriteLog(Severity.DEBUG, ex, param);
        }

        public void Trace(params object[] param)
        {
            WriteLog(Severity.TRACE, null, param);
        }
        private void WriteLog(Severity severity, Exception? exception = null, params object[] param)
        {
            Task.Run(() =>
            {
                var text = "";
                var paramToLog = param.ToLogString();
                if (IEasLog.Config.IsLogJson)
                {
                    var model = EasLogHelper.LogModelCreate(severity, _LogSource, paramToLog, exception);
                    text = model.JsonSerialize();
                }
                else
                {
                    var dateStr = DateTime.Now.ToString(IEasLog.Config.DateFormatString);
                    text = $"[{dateStr}] [{severity}] " + paramToLog;
                    if (exception != null) text = text + " Exception:" + exception.Message;
                }
                WriteLog(severity, text);
            });
        }
        public void WriteLog(Severity severity, string log)
        {
            Task.Run(() =>
            {
                if (IEasLog.Config.DontLog) return;
                try
                {
                    if (IEasLog.Config.ConsoleAppender) EasLogConsole.Log(severity, log);
                    if(IEasLog.Config.StackLogCount > 0)
                    {
                        if(IEasLog.Config.StackLogCount >= _stackLogs.Count)
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
                        WriteToFile(log);
                    }
                }
                catch(Exception ex) 
                {
                    lock (Errors)
                    {
                        Errors.Add(ex);
                    }
                }
            });
        }
        private void WriteLines(List<string> logs)
        {
            if (!Directory.Exists(IEasLog.Config.LogFolderPath)) Directory.CreateDirectory(IEasLog.Config.LogFolderPath);
            lock (_fileLock)
            {
                File.WriteAllLines(ExactLogPath, logs);
            }
        }
        private void WriteToFile(string log)
        {
            if (!Directory.Exists(IEasLog.Config.LogFolderPath)) Directory.CreateDirectory(IEasLog.Config.LogFolderPath);
            lock (_fileLock)
            {
                File.AppendAllText(ExactLogPath, log + "\n");
            }
        }
        private static List<string> _stackLogs = new();
        public static List<Exception> Errors { get; set; } = new();

        /// <summary>
        /// This method must be called before application exit. If 
        /// </summary>
        public void Flush()
        {
            WriteLines(_stackLogs);
        }

    }

}

