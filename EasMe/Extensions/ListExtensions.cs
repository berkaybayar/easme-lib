using EasMe.Exceptions;
using System.ComponentModel;
using System.Data;

namespace EasMe.Extensions
{
    public static class ListExtensions
    {
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
        public static T SelectRandom<T>(this IEnumerable<T> list) where T : class 
        {
            var random = new Random();
            var maxIdx = list.Count();
            var num = random.Next(maxIdx);
            return list.ElementAt(num);
        }
        public static T SelectRandom<T>(this List<T> list) 
        {
            var random = new Random();
            var maxIdx = list.Count;
            var num = random.Next(maxIdx);
            return list.ElementAt(num);
        }
        public static T SelectRandom<T>(this T[] list)
        {
            var random = new Random();
            var maxIdx = list.Length;
            var num = random.Next(maxIdx);
            return list.ElementAt(num);
        }
    }
}
