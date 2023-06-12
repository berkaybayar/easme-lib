using System.Collections;
using Newtonsoft.Json;

namespace EasMe.Models;

public class CleanException {
    public CleanException() {
    }

    public CleanException(string message) {
        Message = message;
    }

    public CleanException(Exception exception) {
        StackTrace = exception.StackTrace;
        Message = exception.Message;
        Source = exception.Source;
        InnerException = exception.InnerException != null ? new CleanException(exception.InnerException) : null;
        Data = exception.Data;
        HResult = exception.HResult;
        HelpLink = exception.HelpLink;
    }

    public string Message { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? StackTrace { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? Source { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? HelpLink { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int HResult { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public CleanException? InnerException { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public IDictionary? Data { get; set; }
}