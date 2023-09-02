using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace EasMe.Extensions;

public static class ValidationExtensions
{
    /// <summary>
    ///   Returns true if given string is valid email address.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsValidEmail(this string str) {
    var trimmedEmail = str.Replace(" ", "");
    if (trimmedEmail.EndsWith("."))
      return false;
    try {
      var addr = new MailAddress(str);
      return addr.Address == trimmedEmail;
    }
    catch {
      return false;
    }
  }

    /// <summary>
    ///   returns true if given string is valid IP address.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public static bool IsValidIpAddress(this string value, out IPAddress? ipAddress) {
    ipAddress = null;
    if (IPAddress.TryParse(value, out var address)) {
      ipAddress = address;
      return true;
    }

    return false;
  }

    /// <summary>
    ///   Return true if given string is valid IP address.
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public static bool IsValidIpAddress(this string ipAddress) {
    return ipAddress.IsValidIpAddress(out _);
  }

  //It's not quite possible make %100 sure it is correct but this will do for most cases
  public static bool IsValidFilePath(this string path) {
    var isValid = path.IndexOfAny(Path.GetInvalidPathChars()) == -1;
    if (!isValid) return false;
    if (!path.Contains('\\'))
      return false;
    if (!path.Contains(':'))
      return false;
    return true;
  }

  /// <summary>
  ///   Returns true if given string is valid MAC Address.
  /// </summary>
  /// <param name="macAddress"></param>
  /// <returns></returns>
  public static bool IsValidMacAddress(this string macAddress) {
    const string macRegex = "^[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}$";
    if (!Regex.IsMatch(macAddress, macRegex))
      return false;
    return true;
  }

  /// <summary>
  ///   Returns true if given string is valid Port.
  /// </summary>
  /// <param name="port"></param>
  /// <returns></returns>
  public static bool IsValidPort(this int port) {
    const string portRegex = "^([0-9]{1,4}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$";
    if (!Regex.IsMatch(port.ToString(), portRegex))
      return false;
    return true;
  }

  /// <summary>
  ///   Checks if the string contains special chars. It will allow letters and digits by default.
  /// </summary>
  /// <param name="yourString"></param>
  /// <param name="allowedChars"></param>
  /// <returns></returns>
  public static bool ContainsSpecialChars(this string yourString, string allowedChars = "") {
    foreach (var c in yourString) {
      if (char.IsLetterOrDigit(c))
        continue;
      if (!allowedChars.Contains(c))
        return true;
    }

    return false;
  }

  /// <summary>
  ///   Checks if password matches the requirements. It will allow letters and digits by default.
  /// </summary>
  /// <param name="password"></param>
  /// <param name="allowedChars"></param>
  /// <param name="minLength"></param>
  /// <param name="maxLength"></param>
  /// <param name="minUpperCaseCount"></param>
  /// <param name="minLowerCaseCount"></param>
  /// <param name="minNumberCount"></param>
  /// <param name="minSpecialCharCount"></param>
  /// <returns></returns>
  public static bool IsStrongPassword(this string password, string allowedChars, byte minLength = 6,
                                      byte maxLength = 16, byte minUpperCaseCount = 1, byte minLowerCaseCount = 1, byte minNumberCount = 1,
                                      byte minSpecialCharCount = 1) {
    if (password.Length < minLength || password.Length > maxLength)
      return false;
    if (ContainsSpecialChars(password, allowedChars))
      return false;
    if (password.Count(char.IsUpper) < minUpperCaseCount)
      return false;
    if (password.Count(char.IsLower) < minLowerCaseCount)
      return false;
    if (password.Count(char.IsNumber) < minNumberCount)
      return false;
    if (password.Length - password.Count(char.IsLetterOrDigit) < minSpecialCharCount)
      return false;
    return true;
  }

  /// <summary>
  ///   Returns true if given URL is image.
  /// </summary>
  /// <param name="URL"></param>
  /// <returns></returns>
  public static bool IsUrlImage(this string URL) {
    try {
      if (!URL.IsValidUrl()) return false;
      var client = new HttpClient();
      var req = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, URL)).Result.Content.Headers.ContentType;
      if (req != null)
        return req.ToString().ToLower().StartsWith("image/");
      if (URL.Contains(".jpg") || URL.Contains(".png") || URL.Contains(".gif") || URL.Contains(".jpeg"))
        return true;
      return false;
    }
    catch (Exception ex) {
      throw new Exception("Failed to check if URL is image: " + URL, ex);
    }
  }

  /// <summary>
  ///   Returns true if given URL is Video.
  /// </summary>
  /// <param name="url"></param>
  /// <returns></returns>
  public static bool IsUrlVideo(this string url) {
    try {
      if (!url.IsValidUrl()) return false;
      var client = new HttpClient();
      var req = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, url)).Result.Content.Headers.ContentType;
      if (req != null)
        return req.ToString().ToLower().StartsWith("video/");

      if (url.Contains(".mp4") || url.Contains(".avi") || url.Contains(".mkv") || url.Contains(".wmv") ||
          url.Contains(".flv") || url.Contains(".mov") || url.Contains(".mpeg") || url.Contains(".mpg") ||
          url.Contains(".webm"))
        return true;
      return false;
    }
    catch (Exception ex) {
      throw new Exception("Failed to check if given URL is video: " + url, ex);
    }
  }

  /// <summary>
  ///   Returns true if given string is valid URL.
  /// </summary>
  /// <param name="url"></param>
  /// <returns></returns>
  public static bool IsValidUrl(this string url) {
    return Uri.IsWellFormedUriString(url, UriKind.Absolute);
  }

  /// <summary>
  ///   Returns true if given string is a valid database connection string.
  /// </summary>
  /// <param name="yourConn"></param>
  /// <returns></returns>
  public static bool IsValidConnectionString(this string yourConn) {
    try {
      DbConnectionStringBuilder csb = new();
      csb.ConnectionString = yourConn;
      return true;
    }
    catch {
      return false;
    }
  }

  public static bool IsValidCreditCard(string cardNo, string expiryDate, int cvv) {
    //Source: https://stackoverflow.com/questions/32959273/c-sharp-validating-user-input-like-a-credit-card-number
    var cardCheck = new Regex(@"^(1298|1267|4512|4567|8901|8933)([\-\s]?[0-9]{4}){3}$");
    var monthCheck = new Regex(@"^(0[1-9]|1[0-2])$");
    var yearCheck = new Regex(@"^20[0-9]{2}$");
    var cvvCheck = new Regex(@"^\d{3}$");

    if (!cardCheck.IsMatch(cardNo)) // <1>check card number is valid
      return false;
    if (!cvvCheck.IsMatch(cvv.ToString())) // <2>check cvv is valid as "999"
      return false;

    var dateParts = expiryDate.Split('/'); //expiry date in from MM/yyyy            
    if (!monthCheck.IsMatch(dateParts[0]) || !yearCheck.IsMatch(dateParts[1])) // <3 - 6>
      return false; // ^ check date format is valid as "MM/yyyy"

    var year = int.Parse(dateParts[1]);
    var month = int.Parse(dateParts[0]);
    var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month); //get actual expiry date
    var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);

    //check expiry greater than today & within next 6 years <7, 8>>
    return cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(6);
  }
}