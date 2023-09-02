using EasMe.Extensions;
using Microsoft.AspNetCore.Http;

namespace EasMe.Logging.Internal;

internal static class EasLogHelper
{
  internal static int ConvertConfigFileSize(string value) {
    try {
      var split = value.Split("-");
      if (split.Length == 0) throw new InvalidDataException("Given LogFileSize is not valid.");
      var size = Convert.ToInt32(split[0].Trim());
      var unit = split[1].Trim().ToLower();
      return unit switch {
        "kb" => size * 1024,
        "mb" => size * 1024 * 1024,
        "gb" => size * 1024 * 1024 * 1024,
        _ => size
      };
    }
    catch (Exception ex) {
      throw new Exception("Failed to parse configuration file size.", ex);
    }
  }


  internal static string GetHeadersJson(HttpContext ctx) {
    try {
      var req = ctx.Request;
      var headers = req.Headers;
      if (headers is null) return string.Empty;
      headers.Remove("Authorization");
      headers.Remove("Cookie");
      var res = headers.ToJsonString()?.RemoveLineEndings() ?? "";
      return res;
    }
    catch {
      return string.Empty;
    }
  }

  internal static List<EasLogLevel> GetLoggableLevels() {
    var list = new List<EasLogLevel>();
    var min = EasLogFactory.Config.MinimumLogLevel;
    var num = (int)min;
    foreach (var item in Enum.GetValues(typeof(EasLogLevel)))
      if ((int)item >= num)
        list.Add((EasLogLevel)item);
    return list;
  }
}