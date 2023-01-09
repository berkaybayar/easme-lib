using Azure.Core;
using EasMe.Models;
using log4net.Plugin;
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
        public static readonly string[] _realIpHeaderList = new string[5]
{
              "X-FORWARDED-FOR",
              "X-Real-IP",
              "X-ORIGINAL-IP",
              "PC-Real-IP",
              "CF-Connecting-IP",

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
		
		public static string GetXForwardedForOrRemoteIp(this HttpRequest req)
        {
			var ip = req.Headers["X-Forwarded-For"].FirstOrDefault();
			if (string.IsNullOrEmpty(ip))
			{
				ip = req.HttpContext.Connection.RemoteIpAddress.ToString();
			}
			return ip;
		}
        public static string[] GetIpAddressList(this HttpRequest req)
        {
			var ipList = new List<string>();
			foreach (var item in _realIpHeaderList)
			{
                try
                {
                    var ip = req.Headers[item].ToString();
                    if (ip != null && ip != "")
                    {
                        ipList.Add(ip);
                    }
                }
                catch { }
			}
			ipList.Add(req.HttpContext.Connection.RemoteIpAddress.ToString());
			return ipList.ToArray();
		}

        /// <summary>
        /// Gets IP's in http request headers by HttpRequest.
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        public static List<string> GetIpAddresses(this HttpRequest httpRequest)
        {
            var list = new List<string>();
            var headers = httpRequest.GetHeaderValues();
            var remoteIp = httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();
            list.Add(remoteIp);
            foreach (var item in headers)
            {
                if (item.Key == "X-FORWARDED-FOR" || item.Key == "PC-Real-IP" || item.Key == "X-Real-IP")
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
