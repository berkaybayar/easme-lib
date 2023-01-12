using EasMe.Extensions;
using EasMe.Models;
using EasMe.Models.SystemModels;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace EasMe
{
    /// <summary>
    /// Web helper
    /// </summary>
    public static class EasProxy
    {


        public static NetworkInfoModel GetNetworkInfo_Client()
        {
			var resp = EasAPI.SendGetRequest("https://cloudflare.com/cdn-cgi/trace", null, 3);
			if (!resp.IsSuccessStatusCode) throw new Exception("Failed to get network info");
			var bodyText = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
			var bodyLines = bodyText.Split("\n");
			var ip = bodyLines[2].Split("=")[1];
			var loc = bodyLines[9].Split("=")[1];
			var warp = bodyLines[12].Split("=")[1].StringConversion<bool>();
			var gateway = bodyLines[13].Split("=")[1].StringConversion<bool>();
			return new NetworkInfoModel
			{
				IpAddress = ip,
				IsGatewayOn = gateway,
				IsWarpOn = warp,
				Location = loc,
			};
		}
        

        public static string GetStatusCodeShortMessage(uint httpStatusCode)
        {
            var statusCode = (HttpStatusCode)httpStatusCode;
            var str = statusCode.ToString();
			var num = int.TryParse(str, out var _);
            if(num) return "Internal Error";
            return str;
        }

        public static string GetHostIpAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            return ipAddress.ToString();
        }
        public static string GetStatusCodeLongMessage(uint httpStatusCode)
        {
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
                226 => "Used",
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
