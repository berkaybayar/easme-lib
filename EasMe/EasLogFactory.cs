using log4net.Appender;

namespace EasMe
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
		
		public static void LoadConfig_WebLogging(Severity severity, string name, bool traceLogging, bool seperateLogLevelToFolder)
		{
			if (_isConfigured) throw new InvalidOperationException("EasLog configuration already loaded.");
			Config = new EasLogConfiguration
			{
				AddRequestUrlToStart = true,
				ConsoleAppender = false,
				ExceptionHideSensitiveInfo = false,
				IsLogJson = true,
				LogFileName = name,
				MinimumLogLevel = severity,
				WebInfoLogging = true,
				TraceLogging = traceLogging,
				SeperateLogLevelToFolder = seperateLogLevelToFolder,
			};
			_isConfigured = true;
		}
		public static void LoadConfig_Logging(
			Severity severity, 
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
				IsLogJson = isJson,
				LogFileName = name,
				MinimumLogLevel = severity,
				WebInfoLogging = false,
				TraceLogging = traceLogging,
				SeperateLogLevelToFolder = seperateLogLevelToFolder,
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
				IsLogJson = true,
				LogFileName = "Debug_",
				MinimumLogLevel = Severity.TRACE,
				WebInfoLogging = isWeb,
				TraceLogging = true,
				SeperateLogLevelToFolder = false,
			};
			_isConfigured = true;
		}
	}
}
