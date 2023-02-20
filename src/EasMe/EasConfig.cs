using System.ComponentModel;
using System.Configuration;

namespace EasMe;

public static class EasConfig
{
    public static string? GetConnectionString(string key)
    {
        return ConfigurationManager.ConnectionStrings[key]?.ConnectionString?.ToString();
    }
    public static string? GetString(string key)
    {
        return ConfigurationManager.AppSettings[key]?.ToString();
    }

    public static T? Get<T>(string key)
    {
        var strValue = GetString(key);
        var converted = Convert.ChangeType(strValue, typeof(T));
        return (T?)converted;
    }
}