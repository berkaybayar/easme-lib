using EasMe.Exceptions;
using EasMe.Models.LogModels;
using System.Diagnostics;
namespace EasMe
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
                    logModel.TraceMethod = EasLogHelper.GetActionName();
                    logModel.TraceClass = EasLogHelper.GetClassName();
                }
                if (IEasLog.Config.WebInfoLogging)
                {
                    logModel.WebLog = WebModelCreate();
                    logModel.LogType = (int)LogType.WEB;
                }
                if (exception != null)
                {
                    logModel.Exception = exception;
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

        ///// <summary>
        ///// Converts System.Exception model to custom Exception model.
        ///// </summary>
        ///// <param name="ex"></param>
        ///// <param name="ForceDebug"></param>
        ///// <returns></returns>
        ///// <exception cref="EasException"></exception>
        //internal static ErrorLogModel ConvertExceptionToLogModel(Exception ex, bool ForceDebug = false)
        //{
        //    if (EasLog.Configuration == null) return new ErrorLogModel();
        //    var model = new ErrorLogModel();
        //    try
        //    {
        //        model.ExceptionMessage = ex.Message;
        //        if (EasLog.Configuration.DebugMode || ForceDebug)
        //        {
        //            model.ExceptionSource = ex.Source;
        //            model.ExceptionStackTrace = ex.StackTrace;
        //            var inner = ex.InnerException;
        //            if (inner != null)
        //                model.ExceptionInner = inner.ToString();
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        throw new FailedToConvertException("Failed to convert System.Exception model to Custom Exception model.", e);
        //    }
        //    return model;

        //}
    }
}
