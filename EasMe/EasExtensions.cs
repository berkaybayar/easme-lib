using EasMe.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EasMe
{
    public static class EasExtensions
    {
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
                
                var isValid = jObject.TryGetValue(key,out var value);
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
        public static string JsonSerialize(this object obj, Formatting formatting = Formatting.None)
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
            if (value.ToLower().Trim() == "true") return true;
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
