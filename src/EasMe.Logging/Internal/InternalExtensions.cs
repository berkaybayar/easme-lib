using System.Diagnostics;

namespace EasMe.Logging.Internal
{
	internal static class InternalExtensions
	{
		internal static string ToLogString(this object[] param)
		{
            if (param.Length > 0)
            {
                var last = param.Last();
                var paramStr = "";
                foreach (var item in param)
                {
                    if (item is null) continue;
                    if (last != item)
                    {
                        paramStr += " [" + item.ToString() + "]";
                    }
                    else paramStr += " " + item.ToString();
                }
                return paramStr.Trim();
            }
            return string.Empty;
        }
		internal static string ToLogString(this object[] param, LogSeverity severity)
		{
			var paramStr = $"[{severity.ToString().ToUpper()}] [{DateTime.Now:MM/dd/yyyy HH:mm:ss}]";
			foreach (var item in param)
			{
				paramStr += " [" + item + "]";
			}
			return paramStr;
		}
	}
}
