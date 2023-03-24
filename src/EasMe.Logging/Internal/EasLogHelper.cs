using System.Diagnostics;
using EasMe.Exceptions;
using EasMe.Extensions;
using EasMe.Logging.Models;
using Microsoft.AspNetCore.Http;


namespace EasMe.Logging.Internal;

internal static class EasLogHelper
{
    internal static TraceInfo GetTraceInfo()
    {
        const int frame = 4;
        var info = new TraceInfo();
        var trace = new StackTrace().GetFrame(frame);
        if (trace == null) return info;
        var method = trace.GetMethod();
        if (method == null) return info;
        info.MethodName = method.Name;
        var reflected = method.ReflectedType;
        if (reflected != null)
        {
            info.ClassName = reflected.Name;
            return info;
        }

        var declaring = method.DeclaringType;
        if (declaring != null) info.ClassName = declaring.Name;
        return info;
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
                _ => size
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
        catch
        {
            return string.Empty;
        }
    }

    internal static List<EasLogLevel> GetLoggableLevels()
    {
        var list = new List<EasLogLevel>();
        var min = EasLogFactory.Config.MinimumLogLevel;
        var num = (int)min;
        foreach (var item in Enum.GetValues(typeof(EasLogLevel)))
            if ((int)item >= num)
                list.Add((EasLogLevel)item);
        return list;
    }


}