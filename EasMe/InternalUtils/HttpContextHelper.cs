using Microsoft.AspNetCore.Http;

namespace EasMe
{
    /*
        [PROGRAM.CS] => FOR WEB APPLICATIONS 
        builder.Services.AddHttpContextAccessor();
     */
    internal static class HttpContextHelper
    {
        private static readonly HttpContextAccessor? _accessor = new();

        internal static HttpContext? Current
        {
            get
            {
                if (_accessor == null) return null;
                return _accessor.HttpContext;
            }
        }

    }
}
