using System.Collections;
using System.Reflection;

namespace EasMe.Logging.Models;

public class CleanException 
{
	public CleanException()
	{
		
	}
	public CleanException( string message)
	{
		Message = message;
	}
	public CleanException(Exception exception)
	{
		StackTrace = exception.StackTrace;
		Message = exception.Message;
		Source = exception.Source;
		InnerException = exception.InnerException != null ? new CleanException(exception.InnerException) : null;
		Data	= exception.Data;
		HResult = exception.HResult;
		HelpLink = exception.HelpLink;
	}

	public string? StackTrace { get; set; }
	public string Message { get; set; }
	public string? Source { get; set; }
	public string? HelpLink { get; set; }
	public int HResult { get; set; }
	public CleanException? InnerException { get; set; }
	public IDictionary? Data { get; set; }

}