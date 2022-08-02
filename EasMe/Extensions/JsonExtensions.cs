using EasMe.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace EasMe.Extensions
{
    public static class JsonExtensions
    {
        /// <summary>
        /// Gets one of the values from Json Object.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string? ParseFromJson(this JObject jObject, string key)
        {
            try
            {
                var isValid = jObject.TryGetValue(key, out var value);
                if (isValid)
                {
                    if (value != null)
                        return value.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new FailedToParseException("Failed to parse from Json.", ex);
            }
        }
        /// <summary>
        /// Serializes given object to json string. Returns "null" string if object is null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static string JsonSerialize(this object? obj, Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.None)
        {
            if (obj == null) return "null";
            return JsonConvert.SerializeObject(obj, formatting);
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
            try
            {
                var jObj = JObject.Parse(jsonStr.ToString());
                if (jObj == null) return null;
                return jObj.ParseFromJson(key);
            }
            catch (Exception ex)
            {
                throw new FailedToParseException("Failed to parse from Json response.", ex);
            }
        }
    }
}
