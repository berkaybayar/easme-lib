using EasMe.Models;
using Microsoft.AspNetCore.Http;
using Windows.Devices.Geolocation;

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
        /// Gets header values by HttpRequest
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
            //NOT IMPLEMENTED
            var model = new GeoLocationModel();
            Geolocator geolocator = new Geolocator();
            //geolocator.DesiredAccuracy = Windows.Devices.Geolocation.PositionAccuracy.High;
            geolocator.DesiredAccuracyInMeters = accuracyInMeters;
            try
            {
                Geoposition geoposition = (Geoposition)geolocator.GetGeopositionAsync(TimeSpan.FromMilliseconds(500), TimeSpan.FromSeconds(1));
                model.Latitude = geoposition.Coordinate.Latitude;
                model.Longitude = geoposition.Coordinate.Longitude;
                model.Accuracy = geoposition.Coordinate.Accuracy;
            }
            catch { }
            return model;
        }
    }
}
