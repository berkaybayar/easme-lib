using Microsoft.AspNetCore.Http;

namespace EasMe
{
    /// <summary>
    /// Web helper
    /// </summary>
    public static class EasProxy
    {
        public static string[] _HeaderList = new string[23]
        {
              "HOST",
              "ORIGIN",
              "X-FORWARDED-FOR",
              "ACCEPT",
              "ACCESSTOKEN",
              "CONTENT-LENGTH",
              "CONTENT-TYPE",
              "COOKIE",
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
        /// Converts all headers to string
        /// </summary>
        /// <param name="Headers"></param>
        /// <returns>Format: "HEADER:VALUE|HEADER2:VALUE2"</returns>       
        public static string ConvertHeadersToString(Dictionary<string, string>? Headers)
        {
            var val = "";
            if (Headers == null) return "";
            foreach (var item in Headers)
            {
                val += $"{item.Key}:{item.Value}|";
            }
            return val.Substring(0, val.Length - 1);

        }
        
        /// <summary>
        /// Gets header values by httpContext
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetHeaderValues(HttpContext httpContext)
        {
            return GetHeaderValues(httpContext.Request);
        }

        /// <summary>
        /// Gets header values by HttpRequest
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetHeaderValues(HttpRequest httpRequest)
        {
            Dictionary<string, string> headerValues = new Dictionary<string, string>();
            foreach (string header in _HeaderList)
            {
                headerValues.Add(header, httpRequest.Headers[header]);
            }
            return headerValues;
        }

        /// <summary>
        /// Gets IP's in http request headers by httpContext.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static List<string> GetIP(HttpContext httpContext)
        {
            return GetIP(httpContext.Request);
        }

        /// <summary>
        /// Gets IP's in http request headers by HttpRequest.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static List<string> GetIP(HttpRequest httpRequest)
        {
            var list = new List<string>();
            var headers = GetHeaderValues(httpRequest);
            foreach (var item in headers)
            {
                if (item.Key == "X-FORWARDED-FOR" || item.Key == "X-ORIGINAL-HOST" || item.Key == "PC-Real-IP" || item.Key == "X-Real-IP" || item.Key == "CF-Connecting-IP")
                {
                    list.Add(item.Value);
                }
            }
            list.Add(httpRequest.HttpContext.Connection.RemoteIpAddress.ToString());
            return list;
        }
        /// <summary>
        /// Get Remote IP Address by HttpContext.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetRemoteIpAddress(HttpContext httpContext)
        {
            return httpContext.Connection.RemoteIpAddress.ToString();
        }
        /// <summary>
        /// Get Remote IP Address by HttpRequest.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string GetRemoteIpAddress(HttpRequest httpRequest)
        {
            return httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        /// <summary>
        ///     Gets request URL by HttpContext.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static string GetRequestQuery(HttpContext httpContext)
        {
            return httpContext.Request.Path.ToUriComponent();
        }

        /// <summary>
        ///     Gets request URL by HttpRequest.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static string GetRequestQuery(HttpRequest httpRequest)
        {
            return httpRequest.Path.ToUriComponent();
        }
        public static string GetContentType(HttpContext httpContext)
        {            
            return GetContentType(httpContext.Request);
        }

        public static string GetContentType(HttpRequest httpRequest)
        {
            var contentType = httpRequest.ContentType;
            if (string.IsNullOrEmpty(contentType))
            {
                return "Unkown";
            }
            return contentType;
        }
    }
}
