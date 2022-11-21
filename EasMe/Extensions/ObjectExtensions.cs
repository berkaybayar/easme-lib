using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace EasMe.Extensions
{
    public static class ObjectExtensions
    {
        public static bool ValidateModel<T>(this T model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results, true);
            return isValid;
        }
        public static bool IsNull(this object? target) => target.IsNull<object>();

        public static bool IsNull<T>(this T? target) => (object)target == DBNull.Value || (object)target == null;

        public static bool IsNotNull(this object? target) => !target.IsNull();

        public static IDictionary<string, object?> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );

        }
        /// <summary>
        /// Converts object to T type variable. Returns default(T) if fails or if is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        //public static T? ToModel<T>(this object? obj)
        //{
        //    try
        //    {
        //        if (obj.IsNull())
        //            return default;
        //        return (T?)obj;
        //    }
        //    catch
        //    {
        //        return default;
        //    }
        //}
        /// <summary>
        /// Converts object to string with its properties. Name:Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToLineString<T>(this T obj)
        {
            if (obj is null)
                return "null";
            Type? t = obj.GetType();
            var text = "";
            foreach (var prop in t.GetProperties())
            {
                var value = prop.GetValue(obj, null);
                var name = prop.Name;
                text += $"{name}:{value?.ToString() ?? "null"} ";
            }
            return text.TrimEnd();
        }
    }
}
