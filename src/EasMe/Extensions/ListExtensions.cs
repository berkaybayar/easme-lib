using System.ComponentModel;
using System.Data;

namespace EasMe.Extensions;

public static class ListExtensions
{
    public static void ForEach<T>(this IEnumerable<T> list, Action<T> action) {
        foreach (var item in list)
            action(item);
    }

    public static string JoinString<T>(this List<T> list, string separator) {
        return string.Join(separator, list);
    }

    public static List<TResult> ForEachResult<T, TResult>(this IEnumerable<T> list, Func<T, TResult> action) {
        var result = new List<TResult>();
        foreach (var item in list)
            result.Add(action(item));
        return result;
    }

    /// <summary>
    ///     Converts List of T model to DataTable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static DataTable ToDataTable<T>(this IList<T> data) {
        var properties =
            TypeDescriptor.GetProperties(typeof(T));
        var table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (var item in data) {
            var row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }

        return table;
    }

    public static T SelectRandom<T>(this IEnumerable<T> list) where T : class {
        var random = new Random();
        var maxIdx = list.Count();
        var num = random.Next(maxIdx);
        return list.ElementAt(num);
    }

    public static T SelectRandom<T>(this List<T> list) {
        var random = new Random();
        var maxIdx = list.Count;
        var num = random.Next(maxIdx);
        return list.ElementAt(num);
    }

    public static T SelectRandom<T>(this T[] list) {
        var random = new Random();
        var maxIdx = list.Length;
        var num = random.Next(maxIdx);
        return list.ElementAt(num);
    }

    public static List<T> Shuffle<T>(this List<T> list) {
        var random = new Random();
        return list.OrderBy(x => random.Next()).ToList();
    }

    public static List<object?> ToObjectList<T>(this List<T> list) {
        return list.Select(x => x as object).ToList() ?? new List<object>();
    }

    public static T SingleOrThrow<T>(this IEnumerable<T> list, string message = "Item not found") {
        var item = list.SingleOrDefault();
        if (item is null)
            throw new ArgumentNullException(message);
        return item;
    }

    public static T SingleOrThrow<T>(this IEnumerable<T> list, Func<T, bool> predicate,
                                     string message = "Item not found") {
        var item = list.SingleOrDefault(predicate);
        if (item is null)
            throw new ArgumentNullException(message);
        return item;
    }

    public static T SingleOrThrow<T>(this IQueryable<T> list, string message = "Item not found") {
        var item = list.SingleOrDefault();
        if (item is null)
            throw new ArgumentNullException(message);
        return item;
    }

    public static T SingleOrThrow<T>(this IQueryable<T> list, Func<T, bool> predicate,
                                     string message = "Item not found") {
        var item = list.SingleOrDefault(predicate);
        if (item is null)
            throw new ArgumentNullException(message);
        return item;
    }

    public static T FirstOrThrow<T>(this IEnumerable<T> list, string message = "Item not found") {
        var item = list.FirstOrDefault();
        if (item is null)
            throw new ArgumentNullException(message);
        return item;
    }

    public static T FirstOrThrow<T>(this IEnumerable<T> list, Func<T, bool> predicate,
                                    string message = "Item not found") {
        var item = list.FirstOrDefault(predicate);
        if (item is null)
            throw new ArgumentNullException(message);
        return item;
    }

    public static T FirstOrThrow<T>(this IQueryable<T> list, string message = "Item not found") {
        var item = list.FirstOrDefault();
        if (item is null)
            throw new ArgumentNullException(message);
        return item;
    }

    public static T FirstOrThrow<T>(this IQueryable<T> list, Func<T, bool> predicate,
                                    string message = "Item not found") {
        var item = list.FirstOrDefault(predicate);
        if (item is null)
            throw new ArgumentNullException(message);
        return item;
    }

    public static void UpdateAll<T>(this IQueryable<T> list, Action<T> action) {
        foreach (var item in list) action(item);
    }

    public static void UpdateAll<T>(this List<T> list, Action<T> action) {
        foreach (var item in list) action(item);
    }

    public static void UpdateAll<T>(this T[] array, Action<T> action) {
        foreach (var item in array) action(item);
    }

    public static void UpdateAllWhere<T>(this List<T> list, Predicate<T> predicate, Action<T> action) {
        foreach (var item in list)
            if (predicate(item))
                action(item);
    }

    public static void UpdateAllWhere<T>(this IQueryable<T> list, Predicate<T> predicate, Action<T> action) {
        foreach (var item in list)
            if (predicate(item))
                action(item);
    }

    public static void UpdateAllWhere<T>(this T[] list, Predicate<T> predicate, Action<T> action) {
        foreach (var item in list)
            if (predicate(item))
                action(item);
    }
}