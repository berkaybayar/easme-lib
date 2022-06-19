using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace EasMe
{
    public static class EasValidate
    {
        public static bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();
            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidIPAddress(string ipAddress, out string version)
        {
            IPAddress address;
            version = "";
            if (IPAddress.TryParse(ipAddress, out address))
            {
                switch (address.AddressFamily)
                {
                    case System.Net.Sockets.AddressFamily.InterNetwork:
                        version = "IPv4";
                        return true;
                    case System.Net.Sockets.AddressFamily.InterNetworkV6:
                        version = "IPv6";
                        return true;
                    default:
                        return false;
                }
            }
            return false;
        }
        //It's not quite possible make %100 sure it is correct but this will do for most cases
        public static bool isValidFilePath(string path)
        {
            bool isValid = path.IndexOfAny(Path.GetInvalidPathChars()) == -1;
            if (!isValid)
            {
                return false;
            }
            isValid = path.Contains(@"\");
            if (!isValid)
            {
                return false;
            }
            isValid = path.Contains(":");
            if (!isValid)
            {
                return false;
            }
            return true;
        }
        public static bool IsValidMACAddress(string macAddress)
        {
            var MACRegex = "^[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}$";
            if (!Regex.IsMatch(macAddress, MACRegex))
            {
                return false;
            }
            return true;
        }
        public static bool IsValidPort(string port)
        {
            var PortRegex = "^([0-9]{1,4}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$";
            if (!Regex.IsMatch(port, PortRegex))
            {
                return false;
            }
            return true;
        }

        public static bool HasSpecialChars(string yourString, string allowedChars)
        {
            foreach (char c in yourString)
            {
                if (char.IsLetterOrDigit(c))
                {
                    continue;
                }
                if (!allowedChars.Contains(c.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsStrongPassword(string password, string allowedChars, int minLength = 6, int maxLength = 16, int minUpperCaseCount = 1, int minLowerCaseCount = 1, int minNumberCount = 1, int minSpecialCharCount = 1)
        {
            if (password.Length < minLength || password.Length > maxLength)
            {
                return false;
            }
            if (HasSpecialChars(password, allowedChars))
            {
                return false;
            }
            if (password.Count(char.IsUpper) < minUpperCaseCount)
            {
                return false;
            }
            if (password.Count(char.IsLower) < minLowerCaseCount)
            {
                return false;
            }
            if (password.Count(char.IsNumber) < minNumberCount)
            {
                return false;
            }
            if ((password.Length - password.Count(char.IsLetterOrDigit)) < minSpecialCharCount)
            {
                return false;
            }

            return true;
        }

        public static bool IsUrlImage(string URL)
        {

            var client = new HttpClient();
            var req = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, URL)).Result.Content.Headers.ContentType;
            if (req != null)
                return req.ToString().ToLower().StartsWith("image/");
            if (URL.Contains(".jpg") || URL.Contains(".png") || URL.Contains(".gif") || URL.Contains(".jpeg"))
            {
                return true;
            }
            return false;
        }

        public static bool IsUrlVideo(string URL)
        {
            var client = new HttpClient();
            var req = client.SendAsync(new HttpRequestMessage(HttpMethod.Head, URL)).Result.Content.Headers.ContentType;
            if (req != null)
                return req.ToString().ToLower().StartsWith("video/");

            if (URL.Contains(".mp4") || URL.Contains(".avi") || URL.Contains(".mkv") || URL.Contains(".wmv") || URL.Contains(".flv") || URL.Contains(".mov") || URL.Contains(".mpeg") || URL.Contains(".mpg") || URL.Contains(".webm"))
            {
                return true;
            }
            return false;
        }

        public static bool IsURL(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}
