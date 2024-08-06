using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EasMe;

public static class JsonExtensions
{
  /// <summary>
  ///   Serializes given object to json string.
  /// </summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  //[Obsolete("Use AsJson instead")]
  //public static string JsonSerialize(this object? obj, Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.None)
  //{
  //    return obj.ToJsonString(formatting);
  //}
  public static string ToJsonString(this object? obj,
                                    Formatting formatting = Formatting.None,
                                    ReferenceLoopHandling referenceLoopHandling = ReferenceLoopHandling.Ignore) {
    if (obj == null) return default;
    var settings = new JsonSerializerSettings {
      ContractResolver = new Resolver(),
      Formatting = formatting,
      ReferenceLoopHandling = referenceLoopHandling
    };
    var json = JsonConvert.SerializeObject(obj, settings);
    return json.RemoveLineEndings();
  }

  /// <summary>
  ///   Deserializes given json string to T model. Uses UnsafeRelaxedJsonEscaping JavaScriptEncoder.
  /// </summary>
  /// <param name="str"></param>
  /// <returns></returns>
  public static T? FromJsonString<T>(this string str) {
    return JsonConvert.DeserializeObject<T>(str);
  }


  private class Resolver : DefaultContractResolver
  {
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization) {
      if (!typeof(Exception).IsAssignableFrom(type)) return base.CreateProperties(type, memberSerialization);
      var props = base.CreateProperties(type, memberSerialization);
      return props.Where(p => p.PropertyName != "WatsonBuckets").ToList();
    }
  }
}