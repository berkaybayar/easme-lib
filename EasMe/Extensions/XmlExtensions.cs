using EasMe.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EasMe.Extensions
{
    public static class XmlExtensions
    {
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
            foreach (var element in xElements)
            {
                var item = element.XmlDeserialize<T>();
                if (item != null) list.Add(item);
            }
            return list;
        }

        /// <summary>
        /// Deserializes given IEnumerable of XElement to T type object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xElements"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static async Task<List<T>> XmlDeserializeAsync<T>(this IEnumerable<XElement> xElements)
        {
            var list = new List<T?>();
            var tasks = new List<Task<T?>>();

            foreach (var element in xElements)
            {
                tasks.Add(Task.Run(() =>
                {
                    var item = element.XmlDeserialize<T>();

                    return item;

                }));
            }
            var results = await Task.WhenAll(tasks);
            list.AddRange(results);
            list.RemoveAll(x => x == null);
            return list;
        }
        /// <summary>
        /// Deserializes given IEnumerable of XElement to T type object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xElements"></param>
        /// <returns></returns>
        /// <exception cref="EasException"></exception>
        public static HashSet<T> XmlDeserializeParallel<T>(this IEnumerable<XElement> xElements)
        {
            var array = new HashSet<T>();

            var result = Parallel.ForEach(xElements, element =>
            {
                var item = element.XmlDeserialize<T>();
                if (item != null) array.Add(item);
            });
        check:
            if (result.IsCompleted) return array;
            goto check;
        }
    }
}
