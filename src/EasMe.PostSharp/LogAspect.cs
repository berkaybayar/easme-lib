using EasMe.Extensions;
using EasMe.Logging;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace EasMe.PostSharp;

[Serializable]
[MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
public class LogAspect : OnMethodBoundaryAspect
{
    private readonly IEasLog _logger;

    public LogAspect(IEasLog logger) {
        _logger = logger;
    }

    public LogAspect() {
        _logger = EasLogFactory.StaticLogger;
    }

    public override void OnEntry(MethodExecutionArgs args) {
        if (!_logger.IsLogLevelEnabled(EasLogLevel.Information)) return;

        try {
            var logParameters = args.Method.GetParameters().Select((t, i) => args.Arguments.GetArgument(i)).ToList();
            _logger.Info($"FullName:{args.Method.DeclaringType?.Name}", $"MethodName:{args.Method.Name}",
                         $"Params:{logParameters.ToJsonString()}");
        }
        catch (Exception e) {
            // ignored
        }

        base.OnEntry(args);
    }
}