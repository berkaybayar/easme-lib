using Microsoft.Extensions.Logging;
using PostSharp.Aspects;

namespace EasMe.PostSharp;

[Serializable]
public class ExceptionLogAspect : OnExceptionAspect
{
  private readonly ILogger _logger;

  public ExceptionLogAspect(ILogger logger) {
    _logger = logger;
  }

  public override void OnException(MethodExecutionArgs args) {
    var methodName = args.Method.Name;
    _logger.LogError(args.Exception, methodName);
  }
}