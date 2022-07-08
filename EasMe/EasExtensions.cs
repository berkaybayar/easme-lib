using EasMe.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EasMe
{
    public static class EasExtensions
    {
        private static readonly HashSet<string> _booleanValues = new HashSet<string>((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase)
        {
          "true",
          "1",
          "on",
          "yes",
          "y"
        };
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
                    return (T)(object)int.Parse(str);
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


        public static bool IsNull(this object target) => target.IsNull<object>();

        public static bool IsNull<T>(this T target) => (object)target == DBNull.Value || (object)target == null;

        public static bool IsNotNull(this object target) => !target.IsNull();

        public static bool IsNullOrEmpty(this string target) => string.IsNullOrEmpty(target);

        public static bool IsNullOrWhiteSpace(this string target) => string.IsNullOrWhiteSpace(target);

        public static bool IsNotNullOrEmpty(this string target) => !string.IsNullOrEmpty(target);

        public static bool IsNotNullOrWhiteSpace(this string target) => !string.IsNullOrWhiteSpace(target);

        /// <summary>
        /// Truncates DbSet or Table, this action can not be undone.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbSet"></param>
        public static void Clear<T>(this DbSet<T> dbSet) where T : class
        {
            dbSet.RemoveRange(dbSet);
        }

        /// <summary>
        /// Returns true if given string is a valid database connection string.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool IsValidConnectionString(this string yourConn)
        {
            try
            {
                DbConnectionStringBuilder csb = new();
                csb.ConnectionString = yourConn;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Gets Database name from connection string.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ParseDbName(this string yourConn)
        {
            try
            {
                if (!yourConn.IsValidConnectionString()) return string.Empty;
                var start = yourConn.IndexOf("Catalog=");
                var sub = yourConn.Substring(start + 8);
                var end = sub.IndexOf(";");
                var dbName = sub.Substring(0, end);
                return dbName;
            }
            catch
            {
                return string.Empty;
            }
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
        /// Serializes given object to json string. Uses UnsafeRelaxedJsonEscaping JavaScriptEncoder.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static string JsonSerialize(this object obj, Newtonsoft.Json.Formatting formatting = Newtonsoft.Json.Formatting.None)
        {
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
        /// Deserializes given XElement to T type object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xElement"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static T? XmlDeserialize<T>(this XElement xElement)
        {
            try
            {
                StringReader reader = new(xElement.ToString().Replace("True", "true").Replace("False", "false"));
                XmlSerializer xmlSerializer = new(typeof(T));
                var item = (T)xmlSerializer.Deserialize(reader);
                return item;
            }
            catch (Exception ex)
            {
                throw new FailedToParseException("XMLMarketManager.Init error, failed to parse Xml.", ex);
            }
        }

        /// <summary>
        /// Deserializes given IEnumerable of XElement to T type object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xElements"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static List<T> XmlDeserialize<T>(this IEnumerable<XElement> xElements)
        {
            var list = new List<T>();
            foreach (var xelemet in xElements)
            {
                var item = xelemet.XmlDeserialize<T>();
                if (item != null)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// Converts List of T model to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            try
            {
                PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();
                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(row);
                }
                return table;
            }
            catch (Exception ex)
            {
                throw new FailedToConvertException("Exception occured while converting list to datatable.", ex);
            }
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
            if (value.ToLower().Trim() == "true") return true;
            if (value.ToLower().Trim() == "1") return true;
            return true;
        }

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

    }
}
