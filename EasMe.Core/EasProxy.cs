using Microsoft.AspNetCore.Http;

namespace EasMe.Core
{
    internal class EasProxy
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
        public Dictionary<string, string> GetHeaderValues(HttpContext ctx)
        {
            Dictionary<string, string> headerValues = new Dictionary<string, string>();
            foreach (string header in _HeaderList)
            {
                headerValues.Add(header, ctx.Request.Headers[header]);
            }
            return headerValues;
        }
        public List<string> GetIPbyHttpContext(HttpContext ctx)
        {
            var list = new List<string>();
            var headers = GetHeaderValues(ctx);
            foreach (var item in headers)
            {
                if (item.Key == "X-FORWARDED-FOR" || item.Key == "X-ORIGINAL-HOST" || item.Key == "PC-Real-IP" || item.Key == "X-Real-IP" || item.Key == "CF-Connecting-IP")
                {
                    list.Add(item.Value);
                }
            }
            return list;
        }
        //This will only work if current working app is downloadable client example: winform
        //If you use it on your website you will get your hosting IP Address
        public string GetUserIPbySendingRequest()
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://api.ipify.org/").Result;
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
