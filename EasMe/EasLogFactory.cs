namespace EasMe
{
    public static class EasLogFactory
    {

        public readonly static EasLog StaticLogger = CreateLogger("StaticLogger");

        internal static EasLogConfiguration Config { get; set; } = new EasLogConfiguration();

        /// <summary>
        /// Creates logger with given LogSource variable.
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
            Config = config;
        }
		/// <summary>
		/// EasLog logging configuration. Call this method in your startup. If you don't call it it will use default values.
		/// </summary>
		/// <param name="config"></param>
		public static void LoadConfig(Action<EasLogConfiguration> action)
		{
			var config = new EasLogConfiguration();
			action(config);
			Config = config;
		}
	}
}
