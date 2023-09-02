using System.Web;

namespace EasMe.Extensions;

public static class DictionaryExtensions
{
  // public static T ToObject<T>(this IDictionary<string, object> source) {
  //     var targetType = typeof(T);
  //     var properties = targetType.GetProperties();
  //     var instance = Activator.CreateInstance<T>();
  //     foreach (var property in properties) {
  //         if (!source.TryGetValue(property.Name, out var value)) continue;
  //         if (!property.PropertyType.IsInstanceOfType(value)) continue;
  //         property.SetValue(instance, value);
  //     }
  //
  //     return instance;
  // }
  public static T ToObject<T>(this IDictionary<string, object> source) {
    var targetType = typeof(T);
    var properties = targetType.GetProperties();
    var instance = Activator.CreateInstance<T>();
    foreach (var kv in source) {
      var property = properties.FirstOrDefault(p => p.Name == kv.Key);
      if (property is null || property == null) continue;
      if (!property.PropertyType.IsInstanceOfType(kv.Value)) continue;
      if (!property.CanWrite) continue;
      property.SetValue(instance, kv.Value);
    }

    return instance;
  }

  public static string ToQueryString(this IDictionary<string, string> dictionary) {
    var array = dictionary.Keys.Select(key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(dictionary[key])}")
                          .ToArray();
    return "?" + string.Join("&", array);
  }
}