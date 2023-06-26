namespace EasMe;

/// <summary>
///     Simple thread-safe variable cache helper.
/// </summary>
/// <typeparam name="TData"></typeparam>
public class EasCache<TData>
{
    private static readonly object _locker = new();
    private readonly Func<TData> _action;
    private readonly int _interval;
    private DateTime _lastUpdate;
    private TData _result;

    public EasCache(Func<TData> action, int intervalMinutes) {
        _interval = intervalMinutes;
        _action = action;
        //_result = _action();
        _lastUpdate = DateTime.MinValue;
    }

    public TData Get() {
        var isUpdateTime = _lastUpdate.AddMinutes(_interval) < DateTime.Now;
        if (!isUpdateTime) return _result;
        lock (_locker) {
            if (!isUpdateTime) return _result;
            Refresh();
        }

        return _result;
    }

    public void Refresh() {
        _result = _action();
        _lastUpdate = DateTime.Now;
    }
}

/// <summary>
///     Simple thread-safe variable cache helper. With In data is key of the caches.
/// </summary>
/// <typeparam name="TData"></typeparam>
public class EasCache<TIn, TData>
    where TData : class
    where TIn : class
{
    private static readonly object _locker = new();
    private readonly Func<TIn, TData> _action;
    private readonly int INTERVAL;
    private Dictionary<TIn, TData> _result;
    private DateTime LAST_UPDATE;

    public EasCache(Func<TIn, TData> action, int intervalMinutes) {
        INTERVAL = intervalMinutes;
        _action = action;
        LAST_UPDATE = DateTime.UnixEpoch;
    }

    public TData? Get(TIn inVal) {
        var isUpdateTime = LAST_UPDATE.AddMinutes(INTERVAL) < DateTime.Now;
        if (_result is not null && !isUpdateTime) return _result.GetValueOrDefault(inVal);
        lock (_locker) {
            if (_result is not null && !isUpdateTime) return _result.GetValueOrDefault(inVal);
            Refresh(inVal);
        }

        return _result.GetValueOrDefault(inVal);
        ;
    }

    public void Refresh(TIn inVal) {
        _result.Remove(inVal);
        _result.Add(inVal, _action(inVal));
        LAST_UPDATE = DateTime.Now;
    }
}