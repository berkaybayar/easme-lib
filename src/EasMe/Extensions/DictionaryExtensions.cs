namespace EasMe.Extensions;

public static class DictionaryExtensions
{
    public static T ToObject<T>(this IDictionary<string, object> source) {
        var targetType = typeof(T);
        var properties = targetType.GetProperties();
        var instance = Activator.CreateInstance<T>();
        foreach (var property in properties) {
            if (!source.TryGetValue(property.Name, out var value)) continue;
            if (!property.PropertyType.IsInstanceOfType(value)) continue;
            property.SetValue(instance, value);
        }

        return instance;
    }
}