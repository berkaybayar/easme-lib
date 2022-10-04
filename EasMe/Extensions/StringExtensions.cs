using EasMe.Exceptions;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace EasMe.Extensions
{
    public static class StringExtensions
    {
        private static readonly HashSet<string> _booleanValues = new HashSet<string>((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase)
        {
          "true",
          "1",
          "on",
          "yes",
          "y"
        };
        public static byte[] ConvertToByteArray(this string yourStr) => Encoding.UTF8.GetBytes(yourStr);
        /// <summary>
        /// Converts string to Type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T? StringConversion<T>(this string? str)
        {
            try
            {
                if (str == null)
                    return default;
                if (typeof(T) == typeof(string))
                    return (T)(object)str;
                if (typeof(T) == typeof(int))
                {
                    var result = int.TryParse(str, out var value);
                    if (result) return (T)(object)value;
                    return default;
                }
                if (typeof(T) == typeof(bool))
                {
                    if (_booleanValues.Contains(str.ToLower()))
                        return (T)(object)true;
                    return (T)(object)false;
                }
                if (typeof(T) == typeof(float))
                    return (T)(object)float.Parse(str);
                if (typeof(T) == typeof(double))
                    return (T)(object)double.Parse(str);
                if (typeof(T) == typeof(decimal))
                    return (T)(object)decimal.Parse(str);
                if (typeof(T) == typeof(long))
                    return (T)(object)long.Parse(str);
                if (typeof(T) == typeof(short))
                    return (T)(object)short.Parse(str);
                if (typeof(T) == typeof(byte))
                    return (T)(object)byte.Parse(str);
                if (typeof(T) == typeof(sbyte))
                    return (T)(object)sbyte.Parse(str);
                if (typeof(T) == typeof(uint))
                    return (T)(object)uint.Parse(str);
                if (typeof(T) == typeof(ulong))
                    return (T)(object)ulong.Parse(str);
                if (typeof(T) == typeof(ushort))
                    return (T)(object)ushort.Parse(str);
                if (typeof(T) == typeof(ulong))
                    return (T)(object)ulong.Parse(str);
                if (typeof(T) == typeof(DateTime))
                    return (T)(object)DateTime.Parse(str);
                if (typeof(T) == typeof(TimeSpan))
                    return (T)(object)TimeSpan.Parse(str);
                if (typeof(T) == typeof(Guid))
                    return (T)(object)Guid.Parse(str);
                if (typeof(T) == typeof(XmlDocument))
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(str);
                    return (T)(object)doc;
                }
                if (typeof(T) == typeof(XDocument))
                    return (T)(object)XDocument.Parse(str);
                if (typeof(T) == typeof(XmlNode))
                    return (T)(object)new XmlDocument().CreateElement(str);
                if (typeof(T) == typeof(JObject))
                    return (T)(object)JObject.Parse(str);
                if (typeof(T) == typeof(JArray))
                    return (T)(object)JArray.Parse(str);
                if (typeof(T) == typeof(JValue))
                    return (T)(object)JValue.Parse(str);
                if (typeof(T) == typeof(JToken))
                    return (T)(object)JToken.Parse(str);
                if (typeof(T) == typeof(DataSet))
                    return (T)(object)new DataSet().ReadXml(new StringReader(str));
                if (typeof(T) == typeof(DataTable))
                    return (T)(object)new DataSet().Tables[0];
                if (typeof(T) == typeof(DataRow))
                    return (T)(object)new DataTable().NewRow();
                if (typeof(T) == typeof(DataColumn))
                    return (T)(object)new DataTable().Columns.Add(str);
                if (typeof(T) == typeof(DataRowView))
                    return (T)(object)new DataView().AddNew();
                if (typeof(T) == typeof(DataView))
                    return (T)(object)new DataView();
                if (typeof(T) == typeof(DataRelation))
                    return (T)(object)new DataRelation("", new DataColumn[0], new DataColumn[0]);
                if (typeof(T) == typeof(DataColumn[]))
                    return (T)(object)new DataTable().Columns.Cast<DataColumn>().ToArray();
                if (typeof(T) == typeof(DataRowBuilder))
                    return (T)(object)new DataTable().NewRow();
                return default;

            }
            catch (Exception ex)
            {
                throw new FailedToConvertException("StringConversion failed type: " + typeof(T), ex);
            }

        }
        public static bool IsNullOrEmpty(this string? target) => string.IsNullOrEmpty(target);

        public static bool IsNullOrWhiteSpace(this string? target) => string.IsNullOrWhiteSpace(target);

        public static bool IsNotNullOrEmpty(this string? target) => !string.IsNullOrEmpty(target);

        public static bool IsNotNullOrWhiteSpace(this string? target) => !string.IsNullOrWhiteSpace(target);

        /// <summary>
        /// Converts string to Int32, returns parsed value if success.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int? ToInt32(this string? value)
        {
            if (int.TryParse(value, out int i))
            {
                return i;
            }
            return null;
        }
        /// <summary>
        /// Converts string to Int32, returns parsed value if success.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long? ToLong(this string? value)
        {
            if (long.TryParse(value, out long i))
            {
                return i;
            }
            return null;
        }

        /// <summary>
        /// Replaces every space char in given string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TrimAbsolute(this string str)
        {
            return str.Replace(" ", "");
        }

        /// <summary>
        /// Returns false if string is equalt to false or null or empty. Returns true otherwise.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string? value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if (value.ToLower().Trim() == "false") return false;
            if (value.ToLower().Trim() == "0") return false;
            return true;
        }
        /// <summary>
        /// Returns false if string is equalt to false or null or empty. Returns true otherwise.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string? value, byte val)
        {
            switch (val)
            {
                case 0:
                    return value.ToBoolean();
                case 1:
                    if (string.IsNullOrEmpty(value)) return false;
                    return true;
                default:
                    return value.ToBoolean();
            }

        }

        /// <summary>
        /// Gets Database name from connection string.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ParseDatabaseName(this string yourConn)
        {
            try
            {
                if (!yourConn.IsValidConnectionString()) return string.Empty;
                var start = yourConn.IndexOf("Catalog=");
                if (start == -1) return string.Empty;
                var sub = yourConn[(start + 8)..];
                var end = sub.IndexOf(";");
                if (end == -1) return string.Empty;
                var dbName = sub[..end];
                return dbName;
            }
            catch
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Capitalizes first char in given string and returns new string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstToUpper(this string str) => char.ToUpper(str[0]) + str.Substring(1);
        /// <summary>
        /// Converts first char in given string to lowercase and returns new string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstToLower(this string str) => char.ToLower(str[0]) + str.Substring(1);

        /// <summary>
        /// Capitalizes first char in given string and returns new string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstToUpperRestToLower(this string str) => char.ToUpper(str[0]) + str.Substring(1).ToLower();

        /// <summary>
        /// Capitalizes first char in given string and returns new string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string LastToUpper(this string str) => char.ToUpper(str[^1]) + str[..^2];

        /// <summary>
        /// Converts last char in given string to lowercase and returns new string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string LastToLower(this string str) => char.ToLower(str[^1]) + str[..^2];

        /// <summary>
        /// Truncates the given string with the given value of max char and adds "..." to end
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxChars"></param>
        /// <returns></returns>
        public static string TruncateString(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }

    }
}
