
using EasMe.Exceptions;
using EasMe.Extensions;
using EasMe.Models.LogModels;
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
        internal static LogModel LogModelCreate(Severity severity, string source, object log, Exception? exception = null, bool forceDebug = false)
        {
            //if (!EasLog._IsCreated)
            //    throw new NotInitializedException("EasLog.Create() must be called before any other method.");
            try
            {
                var logModel = new LogModel();
                logModel.LogLevel = severity.ToString();
                logModel.Source = source;
                logModel.Log = log;
                logModel.LogType = (int)LogType.BASE;
                if (IEasLog.Config.TraceLogging || forceDebug)
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
                        var conAndAction = logModel.WebLog?.RequestUrl?.Replace("/api/", "");
                        if(conAndAction != null)
                        {
                            logModel.Log = $"[{conAndAction ?? "UnkownUrl"}]";
                            //var index = conAndAction?.LastIndexOf("/", StringComparison.Ordinal);
                            //if(index != null)
                            //{
                            //    var action = conAndAction?[(index.Value + 1)..];
                            //    logModel.Source += "." + action;
                            //}
                        }
                       
                    }
                    
                }
                if (exception != null)
                {
                    if (IEasLog.Config.ExceptionHideSensitiveInfo) exception = new Exception(exception.Message);
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
        internal static WebLogModel? WebModelCreate()
        {
            if (EasHttpContext.Current == null) return null;
            try
            {
                var log = new WebLogModel();
                log.Ip = EasHttpContext.Current.Request.GetRemoteIpAddress();
                log.HttpMethod = EasHttpContext.Current.Request.Method;
                log.RequestUrl = EasHttpContext.Current.Request.GetRequestQuery();
                log.Headers = EasHttpContext.Current.Request.GetHeaderValues().JsonSerialize();
                return log;
            }
            catch (Exception e)
            {
                throw new FailedToCreateException("WebLogModel creation failed.", e);
            }

        }

    }
}
