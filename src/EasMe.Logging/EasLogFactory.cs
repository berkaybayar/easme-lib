using EasMe.Enums;
using log4net.Appender;

namespace EasMe.Logging
{
	public static class EasLogFactory
	{

		public readonly static EasLog StaticLogger = CreateLogger("StaticLogger");

		internal static EasLogConfiguration Config { get; set; } = new EasLogConfiguration();
		private static bool _isConfigured = false;
		/// <summary>
		/// Creates logger with given LogSource string.
		/// </summary>
		/// <param name="logSource"></param>
		/// <returns></returns>
		public static EasLog CreateLogger(string logSource)
		{
			return new EasLog(logSource);
		}

		/// <summary>
		/// EasLog logging configuration. Call this method in your startup. If you don't call it it will use default values.
		/// </summary>
		/// <param name="config"></param>
		public static void LoadConfig(EasLogConfiguration config)
		{
			if (_isConfigured) throw new InvalidOperationException("EasLog configuration already loaded.");
			Config = config;
			_isConfigured = true;
		}
		/// <summary>
		/// EasLog logging configuration. Call this method in your startup. If you don't call it it will use default values.
		/// </summary>
		/// <param name="config"></param>
		public static void LoadConfig(Action<EasLogConfiguration> action)
		{
			if (_isConfigured) throw new InvalidOperationException("EasLog configuration already loaded.");
			var config = new EasLogConfiguration();
			action(config);
			Config = config;
			_isConfigured = true;
		}
		
		public static void LoadConfig_WebLogging(LogSeverity severity, string name, bool traceLogging, bool seperateLogLevelToFolder)
		{
			if (_isConfigured) throw new InvalidOperationException("EasLog configuration already loaded.");
			Config = new EasLogConfiguration
			{
				AddRequestUrlToStart = true,
				ConsoleAppender = false,
				ExceptionHideSensitiveInfo = false,
				LogFileName = name,
				MinimumLogLevel = severity,
				WebInfoLogging = true,
				TraceLogging = traceLogging,
				SeparateLogLevelToFolder = seperateLogLevelToFolder,
			};
			_isConfigured = true;
		}
		public static void LoadConfig_Logging(
			LogSeverity severity, 
			string name, 
			bool traceLogging = false, 
			bool seperateLogLevelToFolder = false,
			bool consoleAppender = false,
			bool isJson = false,
			bool exceptionHideSensitiveInfo = false)
		{
			if (_isConfigured) throw new InvalidOperationException("EasLog configuration already loaded.");
			Config = new EasLogConfiguration
			{
				AddRequestUrlToStart = false,
				ConsoleAppender = consoleAppender,
				ExceptionHideSensitiveInfo = exceptionHideSensitiveInfo,
				LogFileName = name,
				MinimumLogLevel = severity,
				WebInfoLogging = false,
				TraceLogging = traceLogging,
				SeparateLogLevelToFolder = seperateLogLevelToFolder,
			};
			_isConfigured = true;
		}
		public static void LoadConfig_TraceDefault(bool isWeb)
		{
			if (_isConfigured) throw new InvalidOperationException("EasLog configuration already loaded.");
			Config = new EasLogConfiguration
			{
				AddRequestUrlToStart = isWeb,
				ConsoleAppender = true,
				ExceptionHideSensitiveInfo = false,
				LogFileName = "Debug_",
				MinimumLogLevel = LogSeverity.TRACE,
				WebInfoLogging = isWeb,
				TraceLogging = true,
				SeparateLogLevelToFolder = false,
			};
			_isConfigured = true;
		}
	}
}
