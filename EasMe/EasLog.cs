using EasMe.Exceptions;
using EasMe.Models.LogModels;

namespace EasMe
{

    //private static readonly EasLog logger = IEasLog.CreateLogger(typeof(AdminManager).Namespace + "." + typeof(AdminManager).Name);
    /// <summary>
    /// Simple static json and console logger with useful options.
    /// </summary>
    public sealed class EasLog : IEasLog
    {
        //internal static EasLogConfiguration Configuration { get; set; } = IEasLog.Config;
        private static int? _OverSizeExt  = 0;

        private static string ExactLogPath { get; set; } = GetExactLogPath();

        internal static bool _IsCreated = false;
        internal string _LogSource;

        internal EasLog(string source)
        {
            _LogSource = source;
        }
        internal EasLog()
        {
            _LogSource = "System";
        }

        private static string GetExactLogPath()
        {
            return IEasLog.Config.LogFolderPath + "\\" + IEasLog.Config.LogFileName + DateTime.Now.ToString(IEasLog.Config.DateFormatString) + IEasLog.Config.LogFileExtension;
        }

        public void Log(Severity severity, object log)
        {
            var model = EasLogHelper.LogModelCreate(severity, _LogSource, log, null, false);
            WriteLog(model);
        }

        public void WriteAll(Severity severity, object[] logArray)
        {
            foreach (var log in logArray)
            {
                var model = EasLogHelper.LogModelCreate(severity, _LogSource, log, null, false);
                WriteLog(model);
            }
        }
        
        /// <summary>
        /// Creates log with Info severity and success state.
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public void Info( object log)
        {
            var model = EasLogHelper.LogModelCreate(Severity.INFO, _LogSource, log, null,false );
            WriteLog(model);
        }


        /// <summary>
        /// Creates log with Error severity.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public  void Error(object logMessage)
        {
            var model = EasLogHelper.LogModelCreate(Severity.ERROR, _LogSource, logMessage, null, false);
            WriteLog(model);
        }

        /// <summary>
        /// Creates log with Error severity.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public void Error(Error err , object logMessage)
        {
            var model = EasLogHelper.LogModelCreate(Severity.ERROR, _LogSource, err.ToString() + ": " + logMessage, null, false );
            WriteLog(model);
        }


        
        /// <summary>
        /// Creates log with warning severity.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public  void Warn(object logMessage)
        {
            
            var model = EasLogHelper.LogModelCreate(Severity.WARN, _LogSource, logMessage, null, false);
            WriteLog(model);
        }

        /// <summary>
        /// Creates log with Exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public  void Exception(Exception ex)
        {
            var model = EasLogHelper.LogModelCreate(Severity.EXCEPTION, _LogSource, ex.Message, ex, false);
            WriteLog(model);
            if (IEasLog.Config.ThrowException) throw new EasException(EasMe.Error.EXCEPTION, ex.Message, ex);
        }
        
        /// <summary>
        /// Creates log with Exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public  void Exception(object logMessage, Exception ex)
        {
            var model = EasLogHelper.LogModelCreate(Severity.EXCEPTION, _LogSource, logMessage, ex, false);
            WriteLog(model);
            if (IEasLog.Config.ThrowException) throw new EasException(EasMe.Error.EXCEPTION, ex.Message, ex);
        }


        /// <summary>
        /// Creates log with Fatal severity.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public  void Fatal(object logMessage)
        {
            var model = EasLogHelper.LogModelCreate(Severity.FATAL, _LogSource, logMessage, null, false);
            WriteLog(model);
        }
        /// <summary>
        /// Creates log with Fatal severity.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>

        public void Fatal(object logMessage, Exception ex)
        {
            var model = EasLogHelper.LogModelCreate(Severity.FATAL, _LogSource, logMessage, ex, false);
            WriteLog(model);
            if (IEasLog.Config.ThrowException) throw new EasException(EasMe.Error.EXCEPTION, ex.Message, ex);
        }
        
        /// <summary>
        /// Creates log with Debug severity.
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public void Debug(object logMessage)
        {
            var model = EasLogHelper.LogModelCreate(Severity.DEBUG, _LogSource, logMessage, null, true);
            WriteLog(model);
        }
        
        /// <summary>
        /// Creates log with Debug severity.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <returns></returns>
        public  void Debug(object logMessage, Exception ex)
        {
            var model = EasLogHelper.LogModelCreate(Severity.DEBUG, _LogSource, logMessage, ex, true);
            WriteLog(model);

        }
        /// <summary>
        /// Creates log with given object. If its not string it will log serialized object.
        /// </summary>
        /// <param name="LogContent"></param>
        /// <param name="UseDefaultDate"></param>
        /// <returns>LogContent</returns>
        public void WriteLog(object obj)
        {

            if (obj == null) throw new EasException(EasMe.Error.NULL_REFERENCE, "Log content is null");
            try
            { 
                var serialized = obj.JsonSerialize();
                //Create log folder 
                if (!Directory.Exists(IEasLog.Config.LogFolderPath)) Directory.CreateDirectory(IEasLog.Config.LogFolderPath);

                if (File.Exists(ExactLogPath))
                {
                    //var size = File.ReadAllBytes(ExactLogPath).Length;
                    //if (size > EasLogHelper.ConvertConfigFileSize(IEasLog.Config.MaxLogFileSize))
                    //{
                    //    _OverSizeExt++;                        
                    //    //var logFileCount = Directory.GetFiles(IEasLog.Config.LogFolderPath).Length;
                    //    //var oldestLogFileName = ExactLogPath.Replace(IEasLog.Config.LogFileExtension, "") + "_" + _OverSizeExt + IEasLog.Config.LogFileExtension;
                    //    //if (logFileCount >= IEasLog.Config.MaxLogFileCount && File.Exists())
                    //    File.Move(ExactLogPath, ExactLogPath.Replace(IEasLog.Config.LogFileExtension, "") + "_" + _OverSizeExt + IEasLog.Config.LogFileExtension);
                    //    ExactLogPath = ExactLogPath.Replace(IEasLog.Config.LogFileExtension, $"_{_OverSizeExt}{IEasLog.Config.LogFileExtension}");

                    //    for (int i = 1; i < IEasLog.Config.MaxLogFileCount; i++)
                    //    {
                    //        var oldLogPath = ExactLogPath.Replace(".log", "(" + i + ").log");
                    //        if (File.Exists(oldLogPath))
                    //        {
                    //            File.Delete(oldLogPath);
                    //        }
                    //    }
                       
                    //}
                }
                File.AppendAllText(ExactLogPath, serialized + "\n");
                if (IEasLog.Config.ConsoleAppender)
                    Console.WriteLine(serialized);
            }
            catch (System.Exception e)
            {
                throw new LoggingFailedException("Exception occured while writing log to log file.", e);
            }

        }


    }
    
}

