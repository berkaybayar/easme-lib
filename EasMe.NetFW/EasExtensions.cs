using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data;
using Newtonsoft.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Collections.Generic;

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
        /// Serializes given object to json string. Uses UnsafeRelaxedJsonEscaping JavaScriptEncoder.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static string Serialize(this object obj)
        {
            try
            {
                
                return JsonConvert.SerializeObject(obj);
                //var o = new JsonSerializerOptions
                //{
                //    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                //};
                //return JsonSerializer.Serialize(obj, o);
            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.SERIALIZATION_ERROR, "Exception occured while serializing object.", e);
            }
        }

        /// <summary>
        /// Deserializes given json string to T model. Uses UnsafeRelaxedJsonEscaping JavaScriptEncoder.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static T? Deserialize<T>(this string str)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(str);
                //var o = new JsonSerializerOptions
                //{
                //    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                //};
                //return JsonSerializer.Deserialize<T>(str, o);
            }
            catch (Exception e)
            {
                throw new EasException(EasMe.Error.SERIALIZATION_ERROR, "Exception occured while serializing object.", e);
            }
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
                StringReader reader = new StringReader(xElement.ToString());
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                var item = (T)xmlSerializer.Deserialize(reader);
                return item;
            }
            catch (Exception ex)
            {
                throw new EasException(Error.FAILED_TO_PARSE, "XMLMarketManager.Init error, failed to parse Xml.", ex);
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
            try
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
            catch (Exception ex)
            {
                throw new EasException(Error.FAILED_TO_PARSE, "XMLMarketManager.Init error, failed to parse Xml.", ex);
            }
        }
        
        /// <summary>
        /// Converts List of T model to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
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
        /// <summary>
        /// Returns false if string is null or empty. Returns true otherwise.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ToBoolean(this string? value)
        {
            if (string.IsNullOrEmpty(value)) return false;
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
        public static long? ToLong(this string value)
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
