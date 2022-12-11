namespace EasMe
{
    /*
        [PROGRAM.CS] => FOR WEB APPLICATIONS 
        builder.Services.AddHttpContextAccessor();
     */
    internal static class HttpContextAccessor
    {
        private static Microsoft.AspNetCore.Http.HttpContextAccessor? _accessor = new Microsoft.AspNetCore.Http.HttpContextAccessor();

        internal static Microsoft.AspNetCore.Http.HttpContext? Current
        {
            get
            {
                if (_accessor == null) return null;
                return _accessor.HttpContext;
            }
        }

    }
}
