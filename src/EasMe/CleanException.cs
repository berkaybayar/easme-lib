using System.Collections;
using Newtonsoft.Json;

namespace EasMe;

public sealed class CleanException
{
  private readonly Exception _exception;

  public CleanException(Exception exception) {
    _exception = exception;
  }

  public string Message => _exception.Message;

  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public string? StackTrace => _exception.StackTrace;

  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public string? Source => _exception.Source;

  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public string? HelpLink => _exception.HelpLink;

  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public int HResult => _exception.HResult;

  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public CleanException? InnerException => _exception.InnerException is null
                                             ? null
                                             : new CleanException(_exception.InnerException);

  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public IDictionary? Data => _exception.Data;

  public Exception GetException() {
    return _exception;
  }
}