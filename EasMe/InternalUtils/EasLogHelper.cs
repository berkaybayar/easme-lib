
using EasMe.Exceptions;
using EasMe.Extensions;
using EasMe.Models.LogModels;
using Microsoft.AspNetCore.Http;
using System.Configuration;
using System.Diagnostics;
namespace EasMe.InternalUtils
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


        /// <summary>
        /// Converts given parameters to BaseLogModel.
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <returns>EasMe.Models.BaseLogModel</returns>
        internal static LogModel LogModelCreate(Severity severity, string source, object log, Exception? exception = null)
        {
            try
            {
                var logModel = new LogModel();
                logModel.LogLevel = severity.ToString();
                logModel.Source = source;
                logModel.Log = log;
                logModel.LogType = (int)LogType.BASE;
                if (IEasLog.Config.TraceLogging || severity == Severity.TRACE)
                {
                    logModel.TraceMethod = GetActionName();
                    logModel.TraceClass = GetClassName();
                }
                if (IEasLog.Config.WebInfoLogging)
                {
                    logModel.WebLog = WebModelCreate();
                    logModel.LogType = (int)LogType.WEB;
                    if (IEasLog.Config.AddRequestUrlToStart)
                    {
                        var conAndAction = logModel.WebLog?.RequestUrl?.Replace("/api", "");
                        if (conAndAction != null)
                        {
                            if (conAndAction.StartsWith("/")) conAndAction = conAndAction[1..];
                            if(conAndAction.Length != 0)
                                logModel.Log = $"[{conAndAction ?? "UnkownUrl"}] " + logModel.Log;
                        }
                    }
                }
                if (exception != null)
                {
                    if (IEasLog.Config.ExceptionHideSensitiveInfo) logModel.Exception = new Exception(exception.Message);
                    else logModel.Exception = exception;
                    logModel.LogType = (int)LogType.EXCEPTION;
                }
                return logModel;
            }
            catch (Exception e)
            {
                throw new FailedToCreateException("LogModel creation failed.", e);
            }
        }
        /// <summary>
        /// Converts given parameters to WebLogModel.
        /// </summary>
        /// <param name="Severity"></param>
        /// <param name="logMessage"></param>
        /// <param name="ErrorNo"></param>
        /// <param name="ip"></param>
        /// <param name="HttpMethod"></param>
        /// <param name="RequestUrl"></param>
        /// <param name="Headers"></param>
        /// <param name="ex"></param>
        /// <returns>EasMe.Models.WebLogModel</returns>
        //internal static WebLogModel WebModelCreate()
        //{
        //    if (EasHttpContext.Current is null) return new();
        //    try
        //    {
        //        var log = new WebLogModel();
        //        log.Ip = EasHttpContext.Current.Request.GetRemoteIpAddress();
        //        log.HttpMethod = EasHttpContext.Current.Request.Method;
        //        log.RequestUrl = EasHttpContext.Current.Request.GetRequestQuery();
        //        log.Headers = GetHeadersJson(EasHttpContext.Current);
        //        return log;
        //    }
        //    catch (Exception)
        //    {
        //        return new();
        //    }

        //}
        internal static WebLogModel WebModelCreate()
        {
            if (LogHttpContext.Current is null) return new();
            try
            {
                var log = new WebLogModel();
                log.Ip = LogHttpContext.Current.Request.GetRemoteIpAddress();
                log.HttpMethod = LogHttpContext.Current.Request.Method;
                log.RequestUrl = LogHttpContext.Current.Request.GetRequestQuery();
                log.Headers = GetHeadersJson(LogHttpContext.Current);
                return log;
            }
            catch (Exception)
            {
                return new();
            }
        }
        private static string GetHeadersJson(HttpContext ctx)
        {
            try
            {
                var req = ctx.Request;
				var headers = req.Headers;
                if (headers is null) return string.Empty;
				headers.Remove("Authorization");
				headers.Remove("Cookie");
				var res = headers.JsonSerialize()?.RemoveLineEndings() ?? "";
				return res;
			}
            catch { return string.Empty; }
        }
        internal static List<Severity> GetLoggableLevels()
        {
            var list = new List<Severity>();
            var min = IEasLog.Config.MinimumLogLevel;
            var num = (int)min;
            foreach (var item in Enum.GetValues(typeof(Severity)))
            {
                if ((int)item >= num) list.Add((Severity)item);
            }
            return list;
        }
        internal static bool IsLoggable(this Severity severity)
        {
            var list = GetLoggableLevels();
            return list.Contains(severity);
        }

    }
}
