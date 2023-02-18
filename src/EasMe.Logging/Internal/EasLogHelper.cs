using EasMe.Exceptions;
using EasMe.Extensions;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
namespace EasMe.Logging.Internal
{
    internal static class EasLogHelper
    {
        /// <summary>
        /// Gets the name of the function this function is called from.
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        internal static string GetActionName(int frame = 3)
        {
            var trace = new StackTrace().GetFrame(frame);
            if (trace == null) return "Unkown";
            var method = trace.GetMethod();
            if (method != null) return method.Name;
            return "Unkown";
        }

        /// <summary>
        /// Gets the name of the class this function is called from.
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        internal static string GetClassName(int frame = 3)
        {
            var trace = new StackTrace().GetFrame(frame);
            if (trace == null) return "Unkown";
            var method = trace.GetMethod();
            if (method == null) return "Unkown";
            var reflected = method.ReflectedType;
            if (reflected != null) return reflected.Name;
            var declaring = method.DeclaringType;
            if (declaring != null) return declaring.Name;
            return "Unkown";
        }

        internal static int ConvertConfigFileSize(string value)
        {

            try
            {
                var split = value.Split("-");
                if (split.Length == 0) throw new NotValidException("Given LogFileSize is not valid.");
                var size = Convert.ToInt32(split[0].Trim());
                var unit = split[1].Trim().ToLower();
                return unit switch
                {
                    "kb" => size * 1024,
                    "mb" => size * 1024 * 1024,
                    "gb" => size * 1024 * 1024 * 1024,
                    _ => size,
                };
            }
            catch (Exception ex)
            {
                throw new FailedToParseException("Failed to parse configuration file size.", ex);
            }
        }



     
        internal static string GetHeadersJson(HttpContext ctx)
        {
            try
            {
                var req = ctx.Request;
				var headers = req.Headers;
                if (headers is null) return string.Empty;
				headers.Remove("Authorization");
				headers.Remove("Cookie");
				var res = headers.ToJsonString()?.RemoveLineEndings() ?? "";
				return res;
			}
            catch { return string.Empty; }
        }
        internal static List<LogSeverity> GetLoggableLevels()
        {
            var list = new List<LogSeverity>();
            var min = EasLogFactory.Config.MinimumLogLevel;
            var num = (int)min;
            foreach (var item in Enum.GetValues(typeof(LogSeverity)))
            {
                if ((int)item >= num) list.Add((LogSeverity)item);
            }
            return list;
        }
        internal static bool IsLoggable(this LogSeverity severity)
        {
            var list = GetLoggableLevels();
            return list.Contains(severity);
        }

    }
}
