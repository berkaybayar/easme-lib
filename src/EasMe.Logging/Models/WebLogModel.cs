using System.Diagnostics;
using EasMe.Extensions;
using EasMe.Logging.Internal;

namespace EasMe.Logging.Models
{
    public class WebLogModel
    {
    
        public WebLogModel()
        {
            if (HttpContextHelper.Current is null) return;
            try
            {
                Ip = HttpContextHelper.Current.Request.GetRemoteIpAddress();
                HttpMethod = HttpContextHelper.Current.Request.Method;
                RequestUrl = HttpContextHelper.Current.Request.GetRequestQuery();
                Headers = EasLogHelper.GetHeadersJson(HttpContextHelper.Current);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        public string? Ip { get; set; }
        public string? HttpMethod { get; set; }
        public string? RequestUrl { get; set; }
        public string? Headers { get; set; }


    }


}
