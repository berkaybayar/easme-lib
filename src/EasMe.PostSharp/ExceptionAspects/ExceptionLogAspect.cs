using EasMe.Logging;
using PostSharp.Aspects;

namespace EasMe.PostSharp.ExceptionAspects;

[Serializable]
public class ExceptionLogAspect : OnExceptionAspect {
    [NonSerialized] private readonly IEasLog _logger;


    public ExceptionLogAspect(IEasLog logger) {
        _logger = logger;
    }

    public ExceptionLogAspect() {
        _logger = EasLogFactory.StaticLogger;
    }

    public override void OnException(MethodExecutionArgs args) {
        var methodName = args.Method.Name;
        _logger.Exception(args.Exception, methodName);
    }
}