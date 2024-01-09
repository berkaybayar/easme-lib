using System.Net;
using Microsoft.AspNetCore.Http;

namespace EasMe;

public static class HttpContextExtensions
{
  public static string[] _HeaderList = new string[21] {
    "HOST",
    "ORIGIN",
    "X-FORWARDED-FOR",
    "ACCEPT",
    "ACCESSTOKEN",
    "CONTENT-LENGTH",
    "CONTENT-TYPE",
    //"COOKIE",
    "REFERER",
    "SEC-CH-UA",
    "SEC-CH-UA-MOBILE",
    "SEC-FETCH-SITE",
    "SERVERREGION",
    "USER-AGENT",
    "X-ORIGINAL-HOST",
    "X-ORIGINAL-URL",
    "X-ORIGINAL-IP",
    "X-FORWARDED-PORT",
    "X-FORWARDED-PROTO",
    "PC-Real-IP",
    "CF-Connecting-IP",
    "X-Real-IP"
  };

  public static readonly string[] _realIpHeaderList = new string[5] {
    "X-FORWARDED-FOR",
    "X-Real-IP",
    "X-ORIGINAL-IP",
    "PC-Real-IP",
    "CF-Connecting-IP"
  };


  /// <summary>
  ///   Gets header values by HttpRequest.
  /// </summary>
  /// <param name="httpRequest"></param>
  /// <returns></returns>
  public static Dictionary<string, string> GetHeaderValues(this HttpRequest httpRequest) {
    var headerValues = new Dictionary<string, string>();
    foreach (var header in _HeaderList) {
      var value = httpRequest.Headers[header];
      if (!string.IsNullOrEmpty(value)) headerValues.Add(header, value);
    }

    return headerValues;
  }

  public static string GetXForwardedForOrRemoteIp(this HttpRequest req) {
    var ip = req.Headers["X-Forwarded-For"].FirstOrDefault();
    if (string.IsNullOrEmpty(ip)) ip = req.HttpContext.Connection.RemoteIpAddress.ToString();
    return ip;
  }

  public static string[] GetHeaderIpAddressList(this HttpRequest req) {
    var ipList = new List<string>();
    foreach (var item in _realIpHeaderList)
      try {
        var ip = req.Headers[item].ToString();
        if (ip != "") ipList.Add(ip);
      }
      catch {
        // ignored
      }

    ipList.Add(req.HttpContext.Connection.RemoteIpAddress.ToString());
    return ipList.ToArray();
  }

  // /// <summary>
  // ///     Gets IP's in http request headers by HttpRequest.
  // /// </summary>
  // /// <param name="httpRequest"></param>
  // /// <returns></returns>
  // public static List<string> GetIpAddresses(this HttpRequest httpRequest) {
  //     var list = new List<string>();
  //     var headers = httpRequest.GetHeaderValues();
  //     var remoteIp = httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();
  //     list.Add(remoteIp);
  //     foreach (var item in headers)
  //         if (item.Key == "X-FORWARDED-FOR" || item.Key == "PC-Real-IP" || item.Key == "X-Real-IP")
  //             list.Add(item.Value);
  //     return list;
  // }

  /// <summary>
  ///   Get Remote IP Address by HttpRequest.
  /// </summary>
  /// <param name="httpRequest"></param>
  /// <returns></returns>
  public static string GetRemoteIpAddress(this HttpRequest httpRequest) {
    try {
      foreach (var item in httpRequest.Headers)
        if (_realIpHeaderList.Any(x => x == item.Key))
          if (!string.IsNullOrEmpty(item.Value))
            return item.Value.ToString() ?? "";
      return httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();
    }
    catch {
      return "N/A";
    }
  }

  /// <summary>
  ///   Whether the request is coming from local.
  /// </summary>
  /// <param name="httpRequest"></param>
  /// <returns></returns>
  public static bool IsLocal(this HttpRequest httpRequest) {
    var connection = httpRequest.HttpContext.Connection;
    if (connection.RemoteIpAddress == null) return true;
    if (connection.LocalIpAddress != null) return connection.RemoteIpAddress.Equals(connection.LocalIpAddress);
    return IPAddress.IsLoopback(connection.RemoteIpAddress);
  }

  public static string GetUserAgent(this HttpRequest httpRequest) {
    {
      try {
        return httpRequest.Headers["User-Agent"].ToString();
      }
      catch {
        return "N/A";
      }
    }
  }

  /// <summary>
  ///   Gets request URL by HttpRequest.
  /// </summary>
  /// <param name="httpRequest"></param>
  /// <returns></returns>
  public static string GetRequestQuery(this HttpRequest httpRequest) {
    return httpRequest.Path.ToUriComponent();
  }

  public static IDictionary<string, string> GetQueryAsDictionary(this HttpRequest httpRequest) {
    return httpRequest.Query.ToDictionary(x => x.Key, x => x.Value.ToString());
  }

  public static string? GetQueryValue(this HttpRequest httpRequest, string key) {
    var queryDic = httpRequest.GetQueryAsDictionary();
    if (queryDic.TryGetValue(key, out var value)) return value;
    return null;
  }
}