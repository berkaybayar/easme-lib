using System.CodeDom;
using System.ComponentModel;

namespace EasMe.Extensions
{
    public static class DictionaryExtensions
    {
        public static T ToObject<T>(this IDictionary<string, object?> source) where T : class, new()
        {
            var obj = new T();
            var type = obj.GetType();
            foreach (var item in source)
            {
                var prop = type.GetProperty(item.Key);
				var str = item.Value?.ToString();
				if (prop is not null && str is not null && prop.CanWrite)
                {
					var ptype = prop.PropertyType;
					var converter = TypeDescriptor.GetConverter(ptype);
					var convertedObject = converter.ConvertFromString(str);
					prop.SetValue(obj, convertedObject);
				}
            }

            return obj;
        }
    }
}
