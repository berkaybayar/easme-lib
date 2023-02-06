using Microsoft.AspNetCore.Http;

namespace EasMe.Helpers
{
    /*
        [PROGRAM.CS] => FOR WEB APPLICATIONS 
        builder.Services.AddHttpContextAccessor();
     */
    public static class HttpContextHelper
    {
        private static readonly HttpContextAccessor? _accessor = new();

        public static HttpContext? Current
        {
            get
            {
                if (_accessor == null) return null;
                return _accessor.HttpContext;
            }
        }

    }
}
