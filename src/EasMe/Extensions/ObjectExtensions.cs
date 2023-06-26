using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EasMe.Extensions;

public static class ObjectExtensions
{
    public static TDestination As<TSource, TDestination>(this TSource source, Func<TSource, TDestination> action) {
        return action(source);
    }

    public static bool IsValidModel<T>(this T model) {
        var validationResults = new List<ValidationResult>();
        var validationContext = new ValidationContext(model, null, null);
        return Validator.TryValidateObject(model, validationContext, validationResults, true);
    }

    public static bool IsNull(this object? target) {
        return target.IsNull<object>();
    }

    public static bool IsNull<T>(this T? target) {
        return (object)target == DBNull.Value || target == null;
    }

    public static bool IsNotNull(this object? target) {
        return !target.IsNull();
    }

    public static Dictionary<string, object?>? AsDictionary<T>(this T source,
                                                               BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance) {
        return source?.GetType().GetProperties(bindingAttr).ToDictionary
            (
             propInfo => propInfo.Name,
             propInfo => propInfo.GetValue(source, null)
            );
    }

    public static object? ChangeType(this object value, Type t) {
        if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) {
            if (value == null) return default;

            t = Nullable.GetUnderlyingType(t);
        }

        var isJson = false;
        return Convert.ChangeType(value, t);
    }

    public static bool IsDefault<T>(this T value) {
        return EqualityComparer<T>.Default.Equals(value, default);
    }
}