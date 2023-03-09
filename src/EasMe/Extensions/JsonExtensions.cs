using EasMe.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace EasMe.Extensions
{
    public static class JsonExtensions
    {
	    class Resolver : DefaultContractResolver
	    {
		    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		    {
			    if (!typeof(Exception).IsAssignableFrom(type)) return base.CreateProperties(type, memberSerialization);
			    IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
			    return props.Where(p => p.PropertyName != "WatsonBuckets").ToList();
		    }
	    }
		/// <summary>
		/// Gets one of the values from Json Object.
		/// </summary>
		/// <param name="response"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static string? FromJObject(this JObject jObject, string key)
        {
            var isValid = jObject.TryGetValue(key, out var value);
            if (isValid)
            {
                if (value != null)
                    return value.ToString();
            }
            return null;
        }
        /// <summary>
        /// Serializes given object to json string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        //[Obsolete("Use AsJson instead")]
        //public static string JsonSerialize(this object? obj, Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.None)
        //{
        //    return obj.ToJsonString(formatting);
        //}
        public static string ToJsonString(this object? obj, Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.None)
        {
            if (obj == null) return default;
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
	            ContractResolver = new Resolver(),
                Formatting = formatting
            };
            string json = JsonConvert.SerializeObject(obj, settings);
			return json.Replace("\n", "").Replace("\r", "");
        }
        /// <summary>
        /// Deserializes given json string to T model. Uses UnsafeRelaxedJsonEscaping JavaScriptEncoder.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static T? FromJsonString<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        /// Deserializes given json string to T model. Uses UnsafeRelaxedJsonEscaping JavaScriptEncoder.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static T? JsonDeserialize<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        /// <summary>
        /// Gets one of the values from Json string.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string? ParseFromJson(this string jsonStr, string key)
        {
            var jObj = JObject.Parse(jsonStr.ToString());
            if (jObj == null) return null;
            return jObj.FromJObject(key);
        }
    }
}
