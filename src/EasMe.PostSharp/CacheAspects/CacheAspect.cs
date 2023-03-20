using Microsoft.Extensions.Caching.Memory;
using PostSharp.Aspects;

namespace EasMe.PostSharp.CacheAspects;

[Serializable]
public class CacheAspect : MethodInterceptionAspect
{
    private int _cacheBySeconds;
    private IMemoryCache _memoryCache;

    public CacheAspect(IMemoryCache memoryCache, int cacheBySeconds = 60)
    {
        _memoryCache = memoryCache;
        _cacheBySeconds = cacheBySeconds;
    }

    public override void OnInvoke(MethodInterceptionArgs args)
    {
        var methodName = string.Format("{0}.{1}.{2}",
            args.Method.ReflectedType?.Namespace,
            args.Method.ReflectedType?.Name,
            args.Method.Name);
        var arguments = args.Arguments.ToList();

        var key = $"{methodName}({string.Join(",", arguments.Select(x => x != null ? x.ToString() : "<Null>"))})";

        var current = _memoryCache.Get(key);
        var contains = current != null;
        if (contains) args.ReturnValue = current;

        base.OnInvoke(args);
        if (args.ReturnValue is not null)
        {
            var entry = _memoryCache.CreateEntry(key);
            entry.SetAbsoluteExpiration(DateTime.Now + TimeSpan.FromSeconds(_cacheBySeconds));
            entry.SetValue(args.ReturnValue);
        }
    }
}