
using EasMe.Exceptions;
using EasMe.Extensions;
using EasMe.InternalUtils;
using EasMe.Models.LogModels;
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
        private static readonly object _lock = new object();
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
            var model = EasLogHelper.LogModelCreate(severity, _LogSource, param.ToLogString(), null, false);
            WriteLog(severity, model);
        }
        public void WriteAll(Severity severity, IEnumerable<string> logArray)
        {
            foreach (var log in logArray)
            {
                var model = EasLogHelper.LogModelCreate(severity, _LogSource, log, null, false);
                WriteLog(severity, model);
            }
        }
        public void WriteAll(List<BulkLog> logs)
        {
            foreach (var log in logs)
            {
                var model = EasLogHelper.LogModelCreate(log.Severity, _LogSource, log.Log, log.Exception, false);
                WriteLog(log.Severity, model);
            }
        }

        public void Info(params object[] param)
        {
            var model = EasLogHelper.LogModelCreate(Severity.INFO, _LogSource, param.ToLogString(), null, false);
            WriteLog(Severity.INFO, model);
        }

        public void Error(params object[] param)
        {
            var model = EasLogHelper.LogModelCreate(Severity.ERROR, _LogSource, param.ToLogString(), null, false);
            WriteLog(Severity.ERROR, model);
        }

        public void Warn(params object[] param)
        {
            var model = EasLogHelper.LogModelCreate(Severity.WARN, _LogSource, param.ToLogString(), null, false);
            WriteLog(Severity.WARN, model);
        }

        public void Exception(Exception ex)
        {
            var model = EasLogHelper.LogModelCreate(Severity.EXCEPTION, _LogSource, ex.Message, ex, false);
            Exception(ex, ex.Message);
        }

        public void Exception(Exception ex, params object[] param)
        {
            var model = EasLogHelper.LogModelCreate(Severity.EXCEPTION, _LogSource, param.ToLogString(), ex, false);
            WriteLog(Severity.EXCEPTION, model);
        }
        //public void Exception(object logMessage, Exception ex)
        //{
        //    var model = EasLogHelper.LogModelCreate(Severity.EXCEPTION, _LogSource, logMessage, ex, false);
        //    WriteLog(Severity.EXCEPTION, model);
        //    if (IEasLog.Config.ThrowException) throw new EasException(EasMe.Error.Exception, ex.Message, ex);
        //}
        //public void Exception(string message, Exception ex,params string[] param)
        //{
        //    var content = param.ToLogString() + " " + message;
        //    Exception(ex, content);
        //}
        public void Fatal( params object[] param)
        {
            var model = EasLogHelper.LogModelCreate(Severity.FATAL, _LogSource, param.ToLogString(), null, false);
            WriteLog(Severity.FATAL, model);
        }
        public void Fatal(Exception ex, params object[] param)
        {
            var model = EasLogHelper.LogModelCreate(Severity.FATAL, _LogSource, param.ToLogString(), ex, false);
            WriteLog(Severity.FATAL, model);
        }

        public void Debug( params object[] param)
        {
            var model = EasLogHelper.LogModelCreate(Severity.DEBUG, _LogSource, param.ToLogString(), null, false);
            WriteLog(Severity.DEBUG, model);
        }
        public void Debug(Exception ex, params object[] param)
        {
            var model = EasLogHelper.LogModelCreate(Severity.DEBUG, _LogSource, param.ToLogString(), ex, false);
            WriteLog(Severity.DEBUG, model);
        }

        public void Trace( params object[] param)
        {
            var model = EasLogHelper.LogModelCreate(Severity.TRACE, _LogSource, param.ToLogString(), null, false);
            WriteLog(Severity.TRACE, model);
        }

        public void WriteLog(Severity severity, object obj)
        {
            if (IEasLog.Config.DontLog) return;
            if (obj == null) throw new EasException(EasMe.Error.NullReference, "Log content is null");
            try
            {
                var serialized = obj.JsonSerialize();
                //Create log folder 
                if (!Directory.Exists(IEasLog.Config.LogFolderPath)) Directory.CreateDirectory(IEasLog.Config.LogFolderPath);
                //To avoid multithread access and exceptions
                lock (_lock)
                {
                    File.AppendAllText(ExactLogPath, serialized + "\n");
                }
                if (IEasLog.Config.ConsoleAppender) EasLogConsole.Log(severity, serialized);
            }
            catch (Exception e)
            {
                throw new LoggingFailedException("Exception occured while writing log to log file.", e);
            }
        }


    }

}

