using EasMe.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Data;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data.Common;

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

        //public static T ConvertTo<T>(this object value) => value.ConvertTo<T>(default(T));

        //public static T ConvertTo<T>(this object value, T defaultValue) => value.ConvertTo<T>(defaultValue, true);

        //public static T ConvertTo<T>(this object value, T defaultValue, bool ignoreException)
        //{
        //    if (!ignoreException)
        //        return value.Convert<T>(defaultValue);
        //    try
        //    {
        //        return value.Convert<T>(defaultValue);
        //    }
        //    catch
        //    {
        //        return typeof(T).Equals(typeof(string)) && defaultValue.IsNull<T>() ? (T)string.Empty : defaultValue;
        //    }
        //}

        //private static T Convert<T>(this object value, T defaultValue)
        //{
        //    Type type = typeof(T);
        //    if (value.IsNull())
        //    {
        //        if (type.Equals(typeof(string)) && defaultValue.IsNull<T>())
        //            return (T);
        //    }
        //    else
        //    {
        //        if (type.Equals(typeof(bool)))
        //            return (T)(ValueType)._booleanValues.Contains(value.ToString().ToLower());
        //        if (type.Equals(typeof(Decimal)))
        //        {
        //            Decimal result;
        //            return Decimal.TryParse(value.ToString(), NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result) ? (T)(ValueType)result : default(T);
        //        }
        //        if (value.GetType() == type)
        //            return (T)value;
        //        TypeConverter converter1 = TypeDescriptor.GetConverter(value);
        //        if (converter1.IsNotNull())
        //        {
        //            if (converter1.CanConvertTo(type))
        //                return (T)converter1.ConvertTo(value, type);
        //            if (converter1.GetType() == typeof(EnumConverter) && type == typeof(int))
        //                return (T)value;
        //        }
        //        TypeConverter converter2 = TypeDescriptor.GetConverter(type);
        //        if (converter2.IsNotNull() && converter2.CanConvertFrom(value.GetType()))
        //            return (T)converter2.ConvertFrom(value);
        //    }
        //    return defaultValue;
        //}

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
