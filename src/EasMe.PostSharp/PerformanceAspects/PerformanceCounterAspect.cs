using System.Diagnostics;
using System.Reflection;
using PostSharp.Aspects;

namespace EasMe.PostSharp.PerformanceAspects;

[Serializable]
public class PerformanceCounterAspect : OnMethodBoundaryAspect
{
    private static Action<string>? _logAction;
    private int _interval;

    [NonSerialized] private Stopwatch _stopwatch;

    public PerformanceCounterAspect(int intervalSeconds = 5)
    {
        _interval = intervalSeconds;
    }

    public override void RuntimeInitialize(MethodBase method)
    {
        _stopwatch = Activator.CreateInstance<Stopwatch>();
    }

    public override void OnEntry(MethodExecutionArgs args)
    {
        _stopwatch.Start();
        base.OnEntry(args);
    }

    public override void OnExit(MethodExecutionArgs args)
    {
        _stopwatch.Stop();
        if (_stopwatch.Elapsed.TotalSeconds > _interval)
        {
            if (_logAction is null)
                Debug.WriteLine("Performance:{0}.{1}-->{2}", args.Method.DeclaringType?.FullName, args.Method.Name,
                    _stopwatch.Elapsed.TotalSeconds);
            else
                _logAction(
                    $"Performance:{args.Method.DeclaringType?.FullName}.{args.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
        }

        _stopwatch.Reset();
        base.OnExit(args);
    }

    public static void SetLogAction(Action<string> logAction)
    {
        _logAction = logAction;
    }
}