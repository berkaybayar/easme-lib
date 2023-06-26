namespace EasMe;

public class EasMemoryCache
{
    private static EasMemoryCache? _instance;

    private static readonly Dictionary<string, CacheData> CacheDictionary = new();

    private EasMemoryCache() {
        ClearLoop();
    }

    public static EasMemoryCache This {
        get {
            _instance ??= new EasMemoryCache();
            return _instance;
        }
    }

    private void ClearLoop() {
        Task.Run(() => {
                     while (true) {
                         var items = CacheDictionary.Where(x => x.Value.ExpireDateTime < DateTime.Now).Select(x => x.Key)
                                                    .ToList();
                         if (items.Count > 0)
                             lock (CacheDictionary) {
                                 foreach (var key in items) CacheDictionary.Remove(key);
                             }

                         Thread.Sleep(1000);
                     }
                 });
    }

    public T? Get<T>(string key) {
        //return cacheDictionary.ContainsKey(key) ? cacheDictionary[key].Value.ToString().StringConversion<T>() : default;
        return CacheDictionary.TryGetValue(key, out var value) ? (T?)value.Value : default;
    }

    public T GetOrSet<T>(string key, Func<T> func, int expireSeconds = 60) {
        if (CacheDictionary.TryGetValue(key, out var value)) return (T)value.Value;
        var res = func();
        if (res is null) return default;
        Set(key, res, expireSeconds);
        return res;
    }

    public object? Get(string key) {
        return CacheDictionary.TryGetValue(key, out var value) ? value.Value : null;
    }

    public void Set(string key, object value, int expireSeconds = 60) {
        if (CacheDictionary.ContainsKey(key))
            lock (CacheDictionary) {
                CacheDictionary.Remove(key);
            }

        var data = new CacheData(value, DateTime.Now + TimeSpan.FromSeconds(expireSeconds));
        lock (CacheDictionary) {
            CacheDictionary.Add(key, data);
        }
    }

    public bool Exists(string key) {
        return CacheDictionary.ContainsKey(key);
    }

    public void Remove(string key) {
        if (!CacheDictionary.ContainsKey(key)) return;
        lock (CacheDictionary) {
            CacheDictionary.Remove(key);
        }
    }

    public void Clear() {
        lock (CacheDictionary) {
            CacheDictionary.Clear();
        }
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