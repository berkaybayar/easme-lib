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
    }
}
