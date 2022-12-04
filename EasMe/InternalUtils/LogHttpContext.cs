using Microsoft.AspNetCore.Http;

namespace EasMe
{
    /*
        [PROGRAM.CS] => FOR WEB APPLICATIONS 
        builder.Services.AddHttpContextAccessor();
     */
    internal static class LogHttpContext
    {
        public static readonly HttpContext? Current = new HttpContextAccessor().HttpContext;
    }
}
