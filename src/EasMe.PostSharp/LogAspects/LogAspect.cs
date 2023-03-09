using PostSharp.Aspects;
using PostSharp.Extensibility;
using System.Reflection;
using EasMe.Extensions;
using EasMe.Logging;
using Microsoft.Extensions.Logging;

namespace EasMe.PostSharp.LogAspects
{
    [Serializable]
    [MulticastAttributeUsage(MulticastTargets.Method, TargetMemberAttributes = MulticastAttributes.Instance)]
    public class LogAspect : OnMethodBoundaryAspect
    {
        private readonly IEasLog _logger;
        public LogAspect(IEasLog logger)
        {
            _logger = logger;
        }
        public LogAspect()
        {
            _logger = EasLogFactory.StaticLogger;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!_logger.IsLogLevelEnabled(LogLevel.Information))
            {
                return;
            }

            try
            {
                var logParameters = args.Method.GetParameters().Select((t, i) => args.Arguments.GetArgument(i)).ToList();
                _logger.Info($"FullName:{args.Method.DeclaringType?.Name}", $"MethodName:{args.Method.Name}", $"Params:{logParameters.ToJsonString()}");
            }
            catch (Exception e)
            {
                // ignored
            }

            base.OnEntry(args);
        }
    }
}
