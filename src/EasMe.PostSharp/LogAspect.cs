using EasMe.Extensions;
using Microsoft.Extensions.Logging;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace EasMe.PostSharp;

[Serializable]
[MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
public class LogAspect : OnMethodBoundaryAspect
{
    private readonly ILogger _logger;
    public LogAspect(ILogger logger) {
        _logger = logger;
    }

    public override void OnEntry(MethodExecutionArgs args) {
        //if (!_logger.IsLogLevelEnabled(EasLogLevel.Information)) return;

        try {
            var logParameters = args.Method.GetParameters().Select((t, i) => args.Arguments.GetArgument(i)).ToList();
            _logger.LogInformation($"FullName:{args.Method.DeclaringType?.Name}", $"MethodName:{args.Method.Name}",
                         $"Params:{logParameters.ToJsonString()}");
        }
        catch (Exception e) {
            // ignored
        }

        base.OnEntry(args);
    }
}