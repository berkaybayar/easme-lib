using System.Collections.Concurrent;

namespace EasMe;

public class EasCache
{
  private static EasCache? _instance;
  private static readonly object Lock = new();
  private static readonly ConcurrentDictionary<string, CacheData> CacheDictionary = new();
  private readonly Timer _timer;

  private const int TIMER_REPEAT_SEC = 3;
  private EasCache() {
    _timer = new Timer(ClearLoop, null, TimeSpan.Zero, TimeSpan.FromSeconds(TIMER_REPEAT_SEC));
  }

  public static EasCache This {
    get {
      if (_instance is not null) return _instance;
      lock (Lock) {
        if (_instance is not null) return _instance;
        _instance = new EasCache();
      }

      return _instance;
    }
  }

  private static void ClearLoop(object? sender = null) {
    var items = CacheDictionary.Where(x => x.Value.ExpireDateTime < DateTime.Now)
                               .Select(x => x.Key)
                               .ToList();
    if (items.Count <= 0) return;
    lock (CacheDictionary) {
      foreach (var key in items) CacheDictionary.Remove(key, out _);
    }
  }

  public T? Get<T>(string key) {
    return CacheDictionary.TryGetValue(key, out var value)
             ? (T?)value.Value
             : default;
  }

  public T GetOrSet<T>(string key, Func<T> func, int expireSeconds = 60) {
    if (CacheDictionary.TryGetValue(key, out var value)) return (T)value.Value;
    var res = func();
    if (res is null) return default;
    Set(key, res, expireSeconds);
    return res;
  }

  public object? Get(string key) {
    return CacheDictionary.TryGetValue(key, out var value)
             ? value.Value
             : null;
  }

  public void Set(string key, object value, int expireSeconds = 60) {
    CacheDictionary.TryRemove(key, out _);
    var data = new CacheData(value, DateTime.Now + TimeSpan.FromSeconds(expireSeconds));
    CacheDictionary.TryAdd(key, data);
  }

  public bool Exists(string key) {
    return CacheDictionary.ContainsKey(key);
  }

  public void Remove(string key) {
    CacheDictionary.TryRemove(key, out _);
  }

  public void Clear() {
    CacheDictionary.Clear();
  }

  private class CacheData
  {
    public CacheData(object value, DateTime expireDateTime) {
      Value = value;
      ExpireDateTime = expireDateTime;
    }

    internal DateTime ExpireDateTime { get; }
    internal object Value { get; }
  }
}