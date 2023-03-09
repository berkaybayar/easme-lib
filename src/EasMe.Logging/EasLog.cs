using EasMe.Extensions;
using EasMe.Logging.Internal;
using System;
using EasMe.Logging.Models;
using Microsoft.Extensions.Hosting;
using log4net;
using Microsoft.Extensions.Logging;

namespace EasMe.Logging
{


	/// <summary>
	/// Simple static json and console logger with useful options.
	/// </summary>
	public class EasLog : IEasLog
    {

		internal string _LogSource;
		private static readonly object _fileLock = new();

        private static readonly EasTask EasTask = new ();
        private static readonly List<Exception> _exceptions = new();
        public static IReadOnlyCollection<Exception> Exceptions => _exceptions;

        internal EasLog(string source)
		{
			_LogSource = source;
		}
	
		private static string GetExactLogPath(LogLevel severity)
		{
			var date = DateTime.Now.ToString(EasLogFactory.Config.DateFormatString);
			string path = EasLogFactory.Config.SeparateLogLevelToFolder
				? Path.Combine(EasLogFactory.Config.LogFolderPath, date , severity.ToString() + "_" + EasLogFactory.Config.LogFileName  + date + EasLogFactory.Config.LogFileExtension)
				: Path.Combine(EasLogFactory.Config.LogFolderPath, EasLogFactory.Config.LogFileName + date + EasLogFactory.Config.LogFileExtension);
			return path;

		}

		public void Info(params object[] param)
		{
			if (!LogLevel.Information.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Information, null, param);
		}
		public void Info(object obj1)
		{
			if (!LogLevel.Information.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Information, null, obj1);
		}
		public void Info(object obj1, object obj2)
		{
			if (!LogLevel.Information.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Information, null, obj1, obj2);
		}
		public void Info(object obj1, object obj2, object obj3)
		{
			if (!LogLevel.Information.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Information, null, obj1, obj2, obj3);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogLevel.Information.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Information, null, obj1, obj2, obj3, obj4);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogLevel.Information.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Information, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogLevel.Information.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Information, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogLevel.Information.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Information, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogLevel.Information.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Information, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}

		public void Error(params object[] param)
		{
			if (!LogLevel.Error.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Error, null, param);
		}
		public void Error(object obj1)
		{
			if (!LogLevel.Error.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Error, null, obj1);
		}
		public void Error(object obj1, object obj2)
		{
			if (!LogLevel.Error.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Error, null, obj1, obj2);
		}
		public void Error(object obj1, object obj2, object obj3)
		{
			if (!LogLevel.Error.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Error, null, obj1, obj2, obj3);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogLevel.Error.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Error, null, obj1, obj2, obj3, obj4);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogLevel.Error.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Error, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogLevel.Error.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Error, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogLevel.Error.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Error, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogLevel.Error.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Error, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}

		public void Warn(params object[] param)
		{
			if (!LogLevel.Warning.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Warning, null, param);
		}

		public void Warn(object obj1)
		{
			if (!LogLevel.Warning.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Warning, null, obj1);
		}
		public void Warn(object obj1, object obj2)
		{
			if (!LogLevel.Warning.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Warning, null, obj1, obj2);
		}
		public void Warn(object obj1, object obj2, object obj3)
		{
			if (!LogLevel.Warning.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Warning, null, obj1, obj2, obj3);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogLevel.Warning.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Warning, null, obj1, obj2, obj3, obj4);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogLevel.Warning.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Warning, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogLevel.Warning.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Warning, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogLevel.Warning.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Warning, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogLevel.Warning.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Warning, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}


		public void Exception(Exception ex)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex);
		}

		public void Exception(Exception ex, params object[] param)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, param);
		}
		public void Exception(Exception ex, object obj1)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1);
		}
		public void Exception(Exception ex, object obj1, object obj2)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3, obj4);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3, obj4, obj5);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}





		public void Fatal(params object[] param)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, null, param);
		}
		public void Fatal(object obj1)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, null, obj1);
		}
		public void Fatal(object obj1, object obj2)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, null, obj1, obj2);
		}
		public void Fatal(object obj1, object obj2, object obj3)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, null, obj1, obj2, obj3);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, null, obj1, obj2, obj3, obj4);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}




		public void Fatal(Exception ex, params object[] param)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, param);

		}
		public void Fatal(Exception ex, object obj1)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1);
		}
		public void Fatal(Exception ex, object obj1, object obj2)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3, obj4);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3, obj4, obj5);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogLevel.Critical.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Critical, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}






		public void Debug(params object[] param)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, null, param);
		}

		public void Debug(object obj1)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, null, obj1);
		}
		public void Debug(object obj1, object obj2)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, null, obj1, obj2);
		}
		public void Debug(object obj1, object obj2, object obj3)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, null, obj1, obj2, obj3);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, null, obj1, obj2, obj3, obj4);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}
		public void Debug(Exception ex, params object[] param)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, ex, param);
		}
		public void Debug(Exception ex, object obj1)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, ex, obj1);
		}
		public void Debug(Exception ex, object obj1, object obj2)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, ex, obj1, obj2);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, ex, obj1, obj2, obj3);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, ex, obj1, obj2, obj3, obj4);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, ex, obj1, obj2, obj3, obj4, obj5);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, ex, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogLevel.Debug.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Debug, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}



		public void Trace(params object[] param)
		{
			if (!LogLevel.Trace.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Trace, null, param);
		}
		public void Trace(object obj1)
		{
			if (!LogLevel.Trace.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Trace, null, obj1);
		}
		public void Trace(object obj1, object obj2)
		{
			if (!LogLevel.Trace.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Trace, null, obj1, obj2);
		}
		public void Trace(object obj1, object obj2, object obj3)
		{
			if (!LogLevel.Trace.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Trace, null, obj1, obj2, obj3);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogLevel.Trace.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Trace, null, obj1, obj2, obj3, obj4);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogLevel.Trace.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Trace, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogLevel.Trace.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Trace, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogLevel.Trace.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Trace, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogLevel.Trace.IsLoggable()) return;
			WriteLog(_LogSource, LogLevel.Trace, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}

        public bool IsLogLevelEnabled(LogLevel LogLevel)
        {
			return LogLevel.IsLoggable();
        }

        public static void Flush()
        {
			EasTask.Flush();
        }


        private static void WriteLog(string source, LogLevel severity, Exception? exception = null, params object[] param)
        {

	        WebInfo? webInfo = null;
			if(EasLogFactory.Config.WebInfoLogging) webInfo = new WebInfo();
            var loggingAction = new Action(() =>
            {

                var paramToLog = param.ToLogString();
                var log = LogModel.Create(severity, source, paramToLog, exception, webInfo);
                if (EasLogFactory.Config.ConsoleAppender) EasLogConsole.Log(log.Level, log.ToJsonString() ?? "");
                var logFilePath = GetExactLogPath(log.Level);
                try
                {

                    var folderPath = Path.GetDirectoryName(logFilePath);
                    if (folderPath is not null)
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }
                    }

                    lock (_fileLock)
                    {
                        File.AppendAllText(logFilePath, log.ToJsonString() + "\n");
                    }
                }
                catch (Exception ex)
                {
                    lock (_exceptions)
                    {
                        _exceptions.Add(ex);
                    }
                }
            });
            EasTask.AddToQueue(loggingAction);
        }
    }

  
}

