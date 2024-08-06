﻿using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

// using log4net.Util.TypeConverters;

namespace EasMe;

public static class StringExtensions
{
  private static readonly HashSet<string> _booleanValues = new(StringComparer.OrdinalIgnoreCase) {
    "true",
    "1",
    "on",
    "yes",
    "y"
  };


  public static string RemoveText(this string value, string removeText) {
    return value.Replace(removeText, string.Empty);
  }

  public static string GetBefore(this string value, string endString) {
    var num = value.IndexOf(endString, StringComparison.Ordinal);
    if (num == -1)
      return string.Empty;
    return value[..num];
  }

  public static string GetAfter(this string value, string startString) {
    var startIndex = value.IndexOf(startString, StringComparison.Ordinal);
    if (startIndex == -1)
      return string.Empty;
    return value[(startIndex + startString.Length)..];
  }

  public static string GetBetween(this string value, string startString, string endString) {
    var startIndex = value.IndexOf(startString, StringComparison.Ordinal);
    if (startIndex == -1)
      return string.Empty;
    var num = value.IndexOf(endString, startIndex + startString.Length, StringComparison.Ordinal);
    return num == -1
             ? string.Empty
             : value.Substring(startIndex + startString.Length, num - startIndex - startString.Length);
  }

  public static byte[] GetBytes(this string yourStr) {
    return Encoding.UTF8.GetBytes(yourStr);
  }

  /// <summary>
  ///   Converts string to Type T.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="str"></param>
  /// <returns></returns>
  public static T? ConvertTo<T>(this string? str) {
    try {
      if (str == null)
        return default;
      if (typeof(T) == typeof(string))
        return (T)(object)str;
      if (typeof(T) == typeof(int)) {
        var result = int.TryParse(str, out var value);
        if (result) return (T)(object)value;
        return default;
      }

      if (typeof(T) == typeof(bool)) {
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
      if (typeof(T) == typeof(XmlDocument)) {
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
        return (T)(object)JToken.Parse(str);
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
    catch (Exception ex) {
      throw new Exception("StringConversion failed type: " + typeof(T), ex);
    }
  }

  public static string Format(this string str, params object[] args) {
    return string.Format(str, args);
  }

  public static bool IsNullOrEmpty(this string? target) {
    return string.IsNullOrEmpty(target);
  }

  public static bool IsNullOrWhiteSpace(this string? target) {
    return string.IsNullOrWhiteSpace(target);
  }

  public static bool IsNotNullOrEmpty(this string? target) {
    return !string.IsNullOrEmpty(target);
  }

  public static bool IsNotNullOrWhiteSpace(this string? target) {
    return !string.IsNullOrWhiteSpace(target);
  }


  /// <summary>
  ///   Replaces every space char in given string.
  /// </summary>
  /// <param name="str"></param>
  /// <returns></returns>
  public static string RemoveWhiteSpace(this string str) {
    var res = str.Replace(" ", "");
    return res;
  }

  /// <summary>
  ///   Returns false if string is equalt to false or null or empty. Returns true otherwise.
  /// </summary>
  /// <param name="value"></param>
  /// <returns></returns>
  public static bool ToBoolean(this string? value) {
    if (string.IsNullOrEmpty(value)) return false;
    if (value.ToLower().Trim() == "false") return false;
    if (value.ToLower().Trim() == "0") return false;
    return true;
  }

  // /// <summary>
  // ///     Gets Database name from connection string.
  // /// </summary>
  // /// <param name="response"></param>
  // /// <param name="key"></param>
  // /// <returns></returns>
  // public static string ParseDatabaseName(this string yourConn) {
  //     try {
  //         if (!yourConn.IsValidConnectionString()) return string.Empty;
  //         var start = yourConn.IndexOf("Catalog=");
  //         if (start == -1) return string.Empty;
  //         var sub = yourConn[(start + 8)..];
  //         var end = sub.IndexOf(";");
  //         if (end == -1) return string.Empty;
  //         var dbName = sub[..end];
  //         return dbName;
  //     }
  //     catch {
  //         return string.Empty;
  //     }
  // }

  /// <summary>
  ///   Capitalizes first char in given string and returns new string.
  /// </summary>
  /// <param name="str"></param>
  /// <returns></returns>
  public static string FirstToUpper(this string str) {
    return char.ToUpper(str[0]) + str.Substring(1);
  }

  /// <summary>
  ///   Converts first char in given string to lowercase and returns new string.
  /// </summary>
  /// <param name="str"></param>
  /// <returns></returns>
  public static string FirstToLower(this string str) {
    return char.ToLower(str[0]) + str.Substring(1);
  }

  /// <summary>
  ///   Capitalizes first char in given string and returns new string.
  /// </summary>
  /// <param name="str"></param>
  /// <returns></returns>
  public static string FirstToUpperRestToLower(this string str) {
    return char.ToUpper(str[0]) + str.Substring(1).ToLower();
  }

  /// <summary>
  ///   Capitalizes first char in given string and returns new string.
  /// </summary>
  /// <param name="str"></param>
  /// <returns></returns>
  public static string LastToUpper(this string str) {
    return char.ToUpper(str[^1]) + str[..^2];
  }

  /// <summary>
  ///   Converts last char in given string to lowercase and returns new string.
  /// </summary>
  /// <param name="str"></param>
  /// <returns></returns>
  public static string LastToLower(this string str) {
    return char.ToLower(str[^1]) + str[..^2];
  }

  /// <summary>
  ///   Truncates the given string with the given value of max char and adds "..." to end
  /// </summary>
  /// <param name="value"></param>
  /// <param name="maxChars"></param>
  /// <returns></returns>
  public static string Truncate(this string str, int maxLength) {
    return str[..Math.Min(str.Length, maxLength)] + (str.Length > maxLength
                                                       ? "..."
                                                       : null);
  }

  public static string RemoveLineEndings(this string str) {
    return str.Replace("\n", "").Replace("\r", "").Replace("\t", "");
  }

  public static string ToHexString(this string str) {
    var bytes = Encoding.Unicode.GetBytes(str);
    return Convert.ToHexString(bytes);
  }

  public static string FromHexString(this string hexString) {
    var bytes = new byte[hexString.Length / 2];
    for (var i = 0; i < bytes.Length; i++) bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
    return Encoding.Unicode.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
  }

  public static string ToBase64String(this string base64String) {
    var plainTextBytes = Encoding.UTF8.GetBytes(base64String);
    return Convert.ToBase64String(plainTextBytes);
  }

  public static string FromBase64String(this string base64String) {
    var base64EncodedBytes = Convert.FromBase64String(base64String);
    return Encoding.UTF8.GetString(base64EncodedBytes);
  }


  public static string Replace_Reverse(this string value, string? newValue, string oldValue) {
    return value.Replace(oldValue, newValue);
  }

  public static SecureString ToSecureString(this string value) {
    if (value == null) throw new ArgumentNullException(nameof(value));
    var secureString = new SecureString();
    foreach (var c in value) secureString.AppendChar(c);
    secureString.MakeReadOnly();
    return secureString;
  }

  public static SecureString ToInsecureString(this SecureString secureString) {
    if (secureString == null) throw new ArgumentNullException(nameof(secureString));
    var unmanagedString = IntPtr.Zero;
    try {
      unsafe {
        unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
        return new SecureString((char*)unmanagedString.ToPointer(), secureString.Length);
      }
    }
    finally {
      Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
    }
  }

  public static bool SecureStringEqual(this SecureString s1, SecureString s2) {
    if (s1 == null) throw new ArgumentNullException(nameof(s1));
    if (s2 == null) throw new ArgumentNullException(nameof(s2));
    if (s1.Length != s2.Length) return false;
    var bstr1 = IntPtr.Zero;
    var bstr2 = IntPtr.Zero;
    RuntimeHelpers.PrepareConstrainedRegions();

    try {
      bstr1 = Marshal.SecureStringToBSTR(s1);
      bstr2 = Marshal.SecureStringToBSTR(s2);

      unsafe {
        for (char* ptr1 = (char*)bstr1.ToPointer(), ptr2 = (char*)bstr2.ToPointer();
             *ptr1 != 0 && *ptr2 != 0;
             ++ptr1, ++ptr2)
          if (*ptr1 != *ptr2)
            return false;
      }

      return true;
    }
    finally {
      if (bstr1 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr1);

      if (bstr2 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr2);
    }
  }

  public static bool IsValidDateTime(this string date) {
    return DateTime.TryParse(date, out var _);
  }
}