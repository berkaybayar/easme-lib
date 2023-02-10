using EasMe.Extensions;
using EasMe.Logging.Internal;

namespace EasMe.Logging
{


	//private static readonly EasLog logger = IEasLog.CreateLogger(typeof(AdminManager).Namespace + "." + typeof(AdminManager).Name);
	/// <summary>
	/// Simple static json and console logger with useful options.
	/// </summary>
	public sealed class EasLog
	{
		//internal static EasLogConfiguration Configuration { get; set; } = IEasLog.Config;
		//private static int? _OverSizeExt = 0;

		//internal static bool _IsCreated = false;
		internal string _LogSource;
		private static readonly object _fileLock = new();
		internal EasLog(string source)
		{
			_LogSource = source;
		}
		internal EasLog()
		{
			_LogSource = "Sys";
		}
		
		private static string GetExactLogPath(LogSeverity severity)
		{
			var date = DateTime.Now.ToString(EasLogFactory.Config.DateFormatString);
			string path = EasLogFactory.Config.SeperateLogLevelToFolder
				? Path.Combine(EasLogFactory.Config.LogFolderPath, date , severity.ToString() + "_" + EasLogFactory.Config.LogFileName  + date + EasLogFactory.Config.LogFileExtension)
				: Path.Combine(EasLogFactory.Config.LogFolderPath, EasLogFactory.Config.LogFileName + date + EasLogFactory.Config.LogFileExtension);
			return path;

		}

		public void Info(params object[] param)
		{
			if (!LogSeverity.INFO.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.INFO, null, param);
		}
		public void Info(object obj1)
		{
			if (!LogSeverity.INFO.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.INFO, null, obj1);
		}
		public void Info(object obj1, object obj2)
		{
			if (!LogSeverity.INFO.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.INFO, null, obj1, obj2);
		}
		public void Info(object obj1, object obj2, object obj3)
		{
			if (!LogSeverity.INFO.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.INFO, null, obj1, obj2, obj3);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogSeverity.INFO.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.INFO, null, obj1, obj2, obj3, obj4);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogSeverity.INFO.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.INFO, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogSeverity.INFO.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.INFO, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogSeverity.INFO.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.INFO, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Info(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogSeverity.INFO.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.INFO, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}

		public void Error(params object[] param)
		{
			if (!LogSeverity.ERROR.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.ERROR, null, param);
		}
		public void Error(object obj1)
		{
			if (!LogSeverity.ERROR.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.ERROR, null, obj1);
		}
		public void Error(object obj1, object obj2)
		{
			if (!LogSeverity.ERROR.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.ERROR, null, obj1, obj2);
		}
		public void Error(object obj1, object obj2, object obj3)
		{
			if (!LogSeverity.ERROR.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.ERROR, null, obj1, obj2, obj3);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogSeverity.ERROR.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.ERROR, null, obj1, obj2, obj3, obj4);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogSeverity.ERROR.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.ERROR, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogSeverity.ERROR.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.ERROR, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogSeverity.ERROR.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.ERROR, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Error(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogSeverity.ERROR.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.ERROR, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}

		public void Warn(params object[] param)
		{
			if (!LogSeverity.WARN.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.WARN, null, param);
		}

		public void Warn(object obj1)
		{
			if (!LogSeverity.WARN.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.WARN, null, obj1);
		}
		public void Warn(object obj1, object obj2)
		{
			if (!LogSeverity.WARN.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.WARN, null, obj1, obj2);
		}
		public void Warn(object obj1, object obj2, object obj3)
		{
			if (!LogSeverity.WARN.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.WARN, null, obj1, obj2, obj3);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogSeverity.WARN.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.WARN, null, obj1, obj2, obj3, obj4);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogSeverity.WARN.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.WARN, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogSeverity.WARN.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.WARN, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogSeverity.WARN.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.WARN, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Warn(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogSeverity.WARN.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.WARN, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}


		public void Exception(Exception ex)
		{
			if (!LogSeverity.EXCEPTION.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.EXCEPTION, ex);
		}

		public void Exception(Exception ex, params object[] param)
		{
			if (!LogSeverity.EXCEPTION.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.EXCEPTION, ex, param);
		}
		public void Exception(Exception ex, object obj1)
		{
			if (!LogSeverity.EXCEPTION.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.EXCEPTION, ex, obj1);
		}
		public void Exception(Exception ex, object obj1, object obj2)
		{
			if (!LogSeverity.EXCEPTION.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.EXCEPTION, ex, obj1, obj2);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3)
		{
			if (!LogSeverity.EXCEPTION.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.EXCEPTION, ex, obj1, obj2, obj3);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogSeverity.EXCEPTION.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.EXCEPTION, ex, obj1, obj2, obj3, obj4);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogSeverity.EXCEPTION.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.EXCEPTION, ex, obj1, obj2, obj3, obj4, obj5);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogSeverity.EXCEPTION.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.EXCEPTION, ex, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogSeverity.EXCEPTION.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.EXCEPTION, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Exception(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogSeverity.EXCEPTION.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.EXCEPTION, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}





		public void Fatal(params object[] param)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, null, param);
		}
		public void Fatal(object obj1)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, null, obj1);
		}
		public void Fatal(object obj1, object obj2)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, null, obj1, obj2);
		}
		public void Fatal(object obj1, object obj2, object obj3)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, null, obj1, obj2, obj3);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, null, obj1, obj2, obj3, obj4);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Fatal(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}




		public void Fatal(Exception ex, params object[] param)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, ex, param);

		}
		public void Fatal(Exception ex, object obj1)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, ex, obj1);
		}
		public void Fatal(Exception ex, object obj1, object obj2)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, ex, obj1, obj2);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, ex, obj1, obj2, obj3);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, ex, obj1, obj2, obj3, obj4);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, ex, obj1, obj2, obj3, obj4, obj5);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, ex, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Fatal(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogSeverity.FATAL.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.FATAL, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}






		public void Debug(params object[] param)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, null, param);
		}

		public void Debug(object obj1)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, null, obj1);
		}
		public void Debug(object obj1, object obj2)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, null, obj1, obj2);
		}
		public void Debug(object obj1, object obj2, object obj3)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, null, obj1, obj2, obj3);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, null, obj1, obj2, obj3, obj4);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Debug(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}
		public void Debug(Exception ex, params object[] param)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, ex, param);
		}
		public void Debug(Exception ex, object obj1)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, ex, obj1);
		}
		public void Debug(Exception ex, object obj1, object obj2)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, ex, obj1, obj2);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, ex, obj1, obj2, obj3);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, ex, obj1, obj2, obj3, obj4);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, ex, obj1, obj2, obj3, obj4, obj5);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, ex, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Debug(Exception ex, object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogSeverity.DEBUG.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.DEBUG, ex, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}



		public void Trace(params object[] param)
		{
			if (!LogSeverity.TRACE.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.TRACE, null, param);
		}
		public void Trace(object obj1)
		{
			if (!LogSeverity.TRACE.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.TRACE, null, obj1);
		}
		public void Trace(object obj1, object obj2)
		{
			if (!LogSeverity.TRACE.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.TRACE, null, obj1, obj2);
		}
		public void Trace(object obj1, object obj2, object obj3)
		{
			if (!LogSeverity.TRACE.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.TRACE, null, obj1, obj2, obj3);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4)
		{
			if (!LogSeverity.TRACE.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.TRACE, null, obj1, obj2, obj3, obj4);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5)
		{
			if (!LogSeverity.TRACE.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.TRACE, null, obj1, obj2, obj3, obj4, obj5);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6)
		{
			if (!LogSeverity.TRACE.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.TRACE, null, obj1, obj2, obj3, obj4, obj5, obj6);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7)
		{
			if (!LogSeverity.TRACE.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.TRACE, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7);
		}
		public void Trace(object obj1, object obj2, object obj3, object obj4, object obj5, object obj6, object obj7, object obj8)
		{
			if (!LogSeverity.TRACE.IsLoggable()) return;
			WriteLog(_LogSource, LogSeverity.TRACE, null, obj1, obj2, obj3, obj4, obj5, obj6, obj7, obj8);
		}


		private static void WriteLog(string source, LogSeverity severity, Exception? exception = null, params object[] param)
		{
			Task.Run(() =>
			{
				try
				{
					var log = CreateLogString(source, severity, exception, param);
					if (EasLogFactory.Config.ConsoleAppender) EasLogConsole.Log(severity, log);
					var logFilePath = GetExactLogPath(severity);
					var folderPath = Path.GetDirectoryName(logFilePath);
					if (folderPath is not null) CheckAndCreateLogFolder(folderPath);
					WriteFile_Safe_NoCheck(logFilePath, log);
				}
				catch (Exception ex)
				{
					lock (Errors)
					{
						Errors.Add(ex);
					}
				}
			});
			string CreateLogString(string source, LogSeverity severity, Exception? ex, params object[] param)
			{
				var paramToLog = param.ToLogString();
				if (EasLogFactory.Config.IsLogJson)
				{
					var model = EasLogHelper.LogModelCreate(severity, source, paramToLog, ex);
					return model.ToJsonString();
				}
				var dateStr = DateTime.Now.ToString(EasLogFactory.Config.DateFormatString);
				var log = $"[{dateStr}] [{severity}] " + paramToLog;
				if (ex != null) log = log + " Exception:" + ex.Message;
				return log;
			}
			void WriteFile_Safe_NoCheck(string logFilePath, string log)
			{
				lock (_fileLock)
				{
					File.AppendAllText(logFilePath, log + "\n");
				}
			}
			void CheckAndCreateLogFolder(string folderPath)
			{
				if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
			}
		}
		
	    
		public static List<Exception> Errors { get; set; } = new();

	}

}

