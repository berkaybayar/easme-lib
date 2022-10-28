using EasMe.Extensions;

namespace EasMe
{
    public interface IEasLog
    {

        public static EasLog StaticLogger { get; set; } = CreateLogger("StaticLogger");

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
        /// Creates logger and uses "NameSpace.Name" as LogSource from given T Model.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static EasLog CreateLogger<T>()
        {
            return new EasLog(typeof(T).Namespace + "." + typeof(T).Name);
        }

        /// <summary>
        /// EasLog logging configuration. Call this method in your startup. If you don't call it it will use default values.
        /// </summary>
        /// <param name="config"></param>
        public static void LoadConfig(EasLogConfiguration config)
        {
            Config = config;
        }

        public static void ConfigureHttpContext(Microsoft.AspNetCore.Http.IHttpContextAccessor? httpContextAccessor) => EasHttpContext.Configure(httpContextAccessor);

    }
}
