using System.Diagnostics;
using EasMe.Extensions;
using EasMe.Logging.Internal;
using Microsoft.AspNetCore.Http;

namespace EasMe.Logging.Models;

public class WebInfo
{
  public WebInfo() {
    var context = new HttpContextAccessor().HttpContext;
    if (context is null) return;
    try {
      Ip = context.Request.GetRemoteIpAddress();
      HttpMethod = context.Request.Method;
      RequestUrl = context.Request.GetRequestQuery();
      Headers = EasLogHelper.GetHeadersJson(context);
    }
    catch (Exception e) {
      Debug.WriteLine(e.Message);
    }
  }

  public string? Ip { get; set; }
  public string? HttpMethod { get; set; }
  public string? RequestUrl { get; set; }
  public string? Headers { get; set; }
}