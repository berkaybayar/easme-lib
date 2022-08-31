using EasMe.Exceptions;
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
            StringReader reader = new(xElement.ToString().Replace("True", "true").Replace("False", "false"));
            XmlSerializer xmlSerializer = new(typeof(T));
            var item = (T?)xmlSerializer.Deserialize(reader);
            return item;
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
            Parallel.ForEach(xElements, el =>
            {
                var item = el.XmlDeserialize<T>();
                if (item != null) 
                {
                    lock (list)
                    {
                        list.Add(item);
                    }
                }

            });
            return list;
        }



    }
}
