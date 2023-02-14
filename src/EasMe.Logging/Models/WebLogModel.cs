using EasMe.Extensions;
using EasMe.Helpers;
using EasMe.Logging.Internal;

namespace EasMe.Logging.Models
{
    public class WebLogModel
    {
    
        public WebLogModel()
        {
            if (HttpContextHelper.Current is null) return;
            var log = new WebLogModel();
            log.Ip = HttpContextHelper.Current.Request.GetRemoteIpAddress();
            log.HttpMethod = HttpContextHelper.Current.Request.Method;
            log.RequestUrl = HttpContextHelper.Current.Request.GetRequestQuery();
            log.Headers = EasLogHelper.GetHeadersJson(HttpContextHelper.Current);
        }
        public string? Ip { get; set; }
        public string? HttpMethod { get; set; }
        public string? RequestUrl { get; set; }
        public string? Headers { get; set; }


    }


}
