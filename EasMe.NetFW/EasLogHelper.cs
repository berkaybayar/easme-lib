using EasMe.Models.LogModels;
using System;
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
                if (split.Length == 0) throw new EasException("Given LogFileSize is not valid.");
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
                throw new EasException(EasMe.Error.FAILED_TO_PARSE, "Failed to parse configuration file size.", ex);
            }
        }
        /// <summary>
        /// Converts System.Exception model to custom Exception model.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="ForceDebug"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        internal static ErrorLogModel ConvertExceptionToLogModel(Exception ex, bool ForceDebug = false)
        {
            if (EasLog.Configuration == null) return new ErrorLogModel();
            var model = new ErrorLogModel();
            try
            {
                model.ExceptionMessage = ex.Message;
                if (EasLog.Configuration.DebugMode || ForceDebug)
                {
                    model.ExceptionSource = ex.Source;
                    model.ExceptionStackTrace = ex.StackTrace;
                    var inner = ex.InnerException;
                    if (inner != null)
                        model.ExceptionInner = inner.ToString();
                }

            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.FAILED_TO_CONVERT, "Failed to convert System.Exception model to Custom Exception model.", e);
            }
            return model;

        }
    }
}
