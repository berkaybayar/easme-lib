using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace EasMe
{
    public class EasValidate
    {

        public bool IsValidEmail(string email)
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
        public bool IsValidIPAddress( string ipAddress, out string version)
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
        public bool IsValidMACAddress(string macAddress)
        {
            var MACRegex = "^[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}$";
            if (!Regex.IsMatch(macAddress, MACRegex))
            {
                return false;
            }
            return true;
        }
        public bool IsValidPort(string port)
        {
            var PortRegex = "^([0-9]{1,4}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$";
            if (!Regex.IsMatch(port, PortRegex))
            {
                return false;
            }
            return true;
        }

        public bool HasSpecialChars(string yourString, string allowedChars)
        {
            foreach (char c in yourString)
            {
                if (char.IsLetterOrDigit(c))
                {
                    continue;
                }
                if (!allowedChars.Contains(c))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsStrongPassword(string password, string allowedChars, int minLength, int maxLength, bool allowSpace)
        {
            if (password.Length < minLength || password.Length > maxLength)
            {
                return false;
            }
            if (allowSpace && password.Contains(' '))
            {
                return false;
            }
            if (HasSpecialChars(password, allowedChars))
            {
                return false;
            }

            return true;
        }
    }
}
