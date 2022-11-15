using EasMe.Exceptions;
using System.Data.Common;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
namespace EasMe
{

    public static class EasValidate
    {
        /// <summary>
        /// Returns true if given string is valid email address.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string str)
        {
            var trimmedEmail = str.Trim();
            if (trimmedEmail.EndsWith("."))
                return false;
            try
            {
                var addr = new MailAddress(str);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// returns true if given string is valid IP address.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="IpAddress"></param>
        /// <returns></returns>
        public static bool IsValidIPAddress(this string value, out IPAddress? IpAddress)
        {
            IpAddress = null;
            if (IPAddress.TryParse(value, out IPAddress? address))
            {
                IpAddress = address;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Return true if given string is valid IP address.
        /// </summary>
        /// <param name="IpAddress"></param>
        /// <returns></returns>
        public static bool IsValidIPAddress(this string IpAddress)
        {
            return IpAddress.IsValidIPAddress(out IPAddress? IpVersion);
        }
        //It's not quite possible make %100 sure it is correct but this will do for most cases
        public static bool IsValidFilePath(this string path)
        {
            bool isValid = path.IndexOfAny(Path.GetInvalidPathChars()) == -1;
            if (!isValid) return false;
            if (!path.Contains('\\'))
                return false;
            if (!path.Contains(':'))
                return false;
            return true;
        }

        /// <summary>
        /// Returns true if given string is valid MAC Address.
        /// </summary>
        /// <param name="macAddress"></param>
        /// <returns></returns>
        public static bool IsValidMACAddress(this string macAddress)
        {
            var MACRegex = "^[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}$";
            if (!Regex.IsMatch(macAddress, MACRegex))
                return false;
            return true;
        }
        /// <summary>
        /// Returns true if given string is valid Port.
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool IsValidPort(this int port)
        {
            var PortRegex = "^([0-9]{1,4}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$";
            if (!Regex.IsMatch(port.ToString(), PortRegex))
                return false;
            return true;
        }
        /// <summary>
        /// Checks if the string contains special chars. It will allow letters and digits by default.
        /// </summary>
        /// <param name="yourString"></param>
        /// <param name="allowedChars"></param>
        /// <returns></returns>
        public static bool HasSpecialChars(this string yourString, string allowedChars = "")
        {
            foreach (char c in yourString)
            {
                if (char.IsLetterOrDigit(c))
                    continue;
                if (!allowedChars.Contains(c))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Checks if password matches the requirements. It will allow letters and digits by default.
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
        public static bool IsStrongPassword(this string password, string allowedChars, byte minLength = 6, byte maxLength = 16, byte minUpperCaseCount = 1, byte minLowerCaseCount = 1, byte minNumberCount = 1, byte minSpecialCharCount = 1)
        {
            if (password.Length < minLength || password.Length > maxLength)
                return false;
            if (HasSpecialChars(password, allowedChars))
                return false;
            if (password.Count(char.IsUpper) < minUpperCaseCount)
                return false;
            if (password.Count(char.IsLower) < minLowerCaseCount)
                return false;
            if (password.Count(char.IsNumber) < minNumberCount)
                return false;
            if ((password.Length - password.Count(char.IsLetterOrDigit)) < minSpecialCharCount)
                return false;
            return true;
        }

        /// <summary>
        /// Returns true if given URL is image.
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static bool IsUrlImage(this string URL)
        {
            try
            {
                if (!URL.IsValidURL()) return false;
                var client = new HttpClient();
                var req = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, URL)).Result.Content.Headers.ContentType;
                if (req != null)
                    return req.ToString().ToLower().StartsWith("image/");
                if (URL.Contains(".jpg") || URL.Contains(".png") || URL.Contains(".gif") || URL.Contains(".jpeg"))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new FailedToCheck("Failed to check if URL is image: " + URL, ex);
            }
        }
        /// <summary>
        /// Returns true if given URL is Video.
        /// </summary>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static bool IsUrlVideo(this string URL)
        {
            try
            {
                if (!URL.IsValidURL()) return false;
                var client = new HttpClient();
                var req = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, URL)).Result.Content.Headers.ContentType;
                if (req != null)
                    return req.ToString().ToLower().StartsWith("video/");

                if (URL.Contains(".mp4") || URL.Contains(".avi") || URL.Contains(".mkv") || URL.Contains(".wmv") || URL.Contains(".flv") || URL.Contains(".mov") || URL.Contains(".mpeg") || URL.Contains(".mpg") || URL.Contains(".webm"))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new FailedToCheck("Failed to check if given URL is video: " + URL, ex);
            }
        }
        /// <summary>
        /// Returns true if given string is valid URL.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidURL(this string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
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


    }
}
