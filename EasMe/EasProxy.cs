using EasMe.Models;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace EasMe
{
    /// <summary>
    /// Web helper
    /// </summary>
    public static class EasProxy
    {
        public static string[] _HeaderList = new string[22]
        {
              "HOST",
              "ORIGIN",
              "X-FORWARDED-FOR",
              "ACCEPT",
              "ACCESSTOKEN",
              "CONTENT-LENGTH",
              "CONTENT-TYPE",
              //"COOKIE",
              "REFERER",
              "SEC-CH-UA",
              "SEC-CH-UA-MOBILE",
              "SEC-FETCH-SITE",
              "SERVERREGION",
              "USER-AGENT",
              "X-ORIGINAL-HOST",
              "X-ORIGINAL-URL",
              "X-ORIGINAL-IP",
              "X-FORWARDED-PORT",
              "X-FORWARDED-PROTO",
              "X-PA-IP",
              "PC-Real-IP",
              "CF-Connecting-IP",
              "X-Real-IP",
        };



        /// <summary>
        /// Gets header values by HttpRequest.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetHeaderValues(this HttpRequest httpRequest)
        {
            Dictionary<string, string> headerValues = new Dictionary<string, string>();
            foreach (string header in _HeaderList)
            {
                var value = httpRequest.Headers[header];
                if (!string.IsNullOrEmpty(value))
                {
                    headerValues.Add(header, value);
                }
            }
            return headerValues;
        }



        /// <summary>
        /// Gets IP's in http request headers by HttpRequest.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static List<string> GetHeaderRealIPs(this HttpRequest httpRequest)
        {
            var list = new List<string>();
            var headers = httpRequest.GetHeaderValues();
            list.Add(httpRequest.HttpContext.Connection.RemoteIpAddress.ToString());
            foreach (var item in headers)
            {
                if (item.Key == "X-FORWARDED-FOR" || item.Key == "X-ORIGINAL-HOST" || item.Key == "PC-Real-IP" || item.Key == "X-Real-IP" || item.Key == "CF-Connecting-IP")
                {
                    list.Add(item.Value);
                }
            }
            return list;
        }


        /// <summary>
        /// Get Remote IP Address by HttpRequest.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string GetRemoteIpAddress(this HttpRequest httpRequest)
        {
            return httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        /// <summary>
        ///     Gets request URL by HttpRequest.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string GetRequestQuery(this HttpRequest httpRequest)
        {
            return httpRequest.Path.ToUriComponent();
        }


        /// <summary>
        /// Gets request GeoLocation by HttpRequest.
        /// </summary>
        /// <param name="accuracyInMeters"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static GeoLocationModel GetGeolocation(this HttpRequest httpRequest, uint accuracyInMeters = 50)
        {
            throw new NotImplementedException();

        }

        public static string GetStatusCodeShortMessage(int httpStatusCode)
        {
            var statusCode = (HttpStatusCode)httpStatusCode;
            return statusCode.ToString();
        }

        public static string GetHostIpAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            return ipAddress.ToString();
        }
        public static string GetStatusCodeLongMessage(int httpStatusCode)
        {
            throw new NotImplementedException();
            return httpStatusCode switch
            {
                100 => "Continue",
                101 => "Switching Protocols",
                102 => "Processing",
                103 => "Early Hints",
                200 => "OK",
                201 => "Created",
                202 => "Accepted",
                203 => "Non-Authoritative Information",
                204 => "No Content",
                205 => "Reset Content",
                206 => "Partial Content",
                207 => "Multi-Status",
                208 => "Already Reported",
                226 => "IM Used",
                300 => "Multiple Choices",
                301 => "Moved Permanently",
                302 => "Found",
                303 => "See Other",
                304 => "Not Modified",
                305 => "Use Proxy",
                306 => "Switch Proxy",
                307 => "Temporary Redirect",
                308 => "Permanent Redirect",
                400 => "Bad Request",
                401 => "Unauthorized",
                402 => "Payment Required",
                403 => "Forbidden",
                404 => "Not Found",
                405 => "Method Not Allowed",
                406 => "Not Acceptable",
                407 => "Proxy Authentication Required",
                408 => "Request Timeout",
                409 => "Conflict",
                410 => "Gone",
                411 => "Length Required",
                412 => "Precondition Failed",
                413 => "Payload Too Large",
                414 => "URI Too Long",
                415 => "Unsupported Media Type",
                416 => "Range Not Satisfiable",
                417 => "Expectation Failed",
                418 => "I'm a teapot",
                421 => "Misdirected Request",
                422 => "Unprocessable Entity",
                423 => "Locked",
                424 => "Failed Dependency",
                425 => "Too Early",
                426 => "Upgrade Required",
                428 => "Precondition Required",
                429 => "Too Many Requests",
                431 => "Request Header Fields Too Large",
                451 => "Unavailable For Legal Reasons",
                500 => "Internal Server Error",
                501 => "Not Implemented",
                502 => "Bad Gateway",
                503 => "Service Unavailable",
                504 => "Gateway Timeout",
                505 => "HTTP Version Not Supported",
                506 => "Variant Also Negot",
                _ => ""
            };
        }
    }
}
