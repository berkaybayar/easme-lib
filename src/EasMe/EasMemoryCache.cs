using EasMe.Extensions;

namespace EasMe;


public class EasMemoryCache
{

    private EasMemoryCache()
    {
        ClearLoop();
    }

    public static EasMemoryCache This
    {
        get
        {
            Instance ??= new();
            return Instance;
        }
    }
    private static EasMemoryCache? Instance;
    private class CacheData
    {
        public CacheData(object value, DateTime expireDateTime)
        {
            Value = value;
            ExpireDateTime = expireDateTime;
        }
        internal DateTime ExpireDateTime { get; init; }
        internal object Value { get; init; }
    }

    private static readonly Dictionary<string, CacheData> cacheDictionary = new Dictionary<string, CacheData>();

    private void ClearLoop()
    {
        Task.Run(() =>
        {
            while (true)
            {
                var items = cacheDictionary.Where(x => x.Value.ExpireDateTime < DateTime.Now).Select(x => x.Key).ToList();
                if (items.Count > 0)
                {
                    lock (cacheDictionary)
                    {
                        foreach (var key in items)
                        {
                            cacheDictionary.Remove(key);
                        }
                    }
                }
                Thread.Sleep(1000);
            }

        });
    }
    public T? Get<T>(string key) 
    {
        //return cacheDictionary.ContainsKey(key) ? cacheDictionary[key].Value.ToString().StringConversion<T>() : default;
        return cacheDictionary.ContainsKey(key) ? (T?)cacheDictionary[key].Value : default;
    }

    public object? Get(string key)
    {
        return cacheDictionary.ContainsKey(key) ? cacheDictionary[key].Value : null;
    }

    public void Set(string key, object value, int expireSeconds = 60)
    {
        if (cacheDictionary.ContainsKey(key))
        {
            lock (cacheDictionary)
            {
                cacheDictionary.Remove(key);

            }
        }

        var data = new CacheData(value, DateTime.Now + TimeSpan.FromSeconds(expireSeconds));
        lock (cacheDictionary)
        {
            cacheDictionary.Add(key, data);

        }
    }

    public bool Exists(string key)
    {
        return cacheDictionary.ContainsKey(key);
    }

    public void Remove(string key)
    {
        if (cacheDictionary.ContainsKey(key))
        {
            lock (cacheDictionary)
            {
                cacheDictionary.Remove(key);

            }
        }
    }

    public void Clear()
    {
        lock (cacheDictionary)
        {
            cacheDictionary.Clear();

        }
    }
}