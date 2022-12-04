using EasMe.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Extensions
{
    public static class HttpContextExtensions
    {
        public static string[] _HeaderList = new string[21]
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
              "PC-Real-IP",
              "CF-Connecting-IP",
              "X-Real-IP",
};
        public static readonly string[] _realIpHeaderList = new string[6]
{
                "CF-Connecting-IP",
              "X-FORWARDED-FOR",
              "X-ORIGINAL-IP",
              "X-FORWARDED-FOR",
              "PC-Real-IP",

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
            try
            {
                foreach (var item in httpRequest.Headers)
                {
                    if (_realIpHeaderList.Any(x => x == item.Key))
                    {
                        if (item.Value.ToString() != null || item.Value != "")
                        {
                            return item.Value.ToString() ?? "";
                        }
                    }
                }
                return httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();

            }
            catch { return "N/A"; }
        }

        public static string GetUserAgent(this HttpRequest httpRequest)
        {
            {
                try
                {
                    return httpRequest.Headers["User-Agent"].ToString();
                }
                catch { return "N/A"; }
            }
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
    }
}
