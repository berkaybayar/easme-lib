namespace EasMe
{
    /*
        [PROGRAM.CS] => FOR WEB APPLICATIONS 
        builder.Services.AddHttpContextAccessor();
        
        [CONTROLLER]
        public ControllerClass(IHttpContextAccessor accessor)
        {
            EasHttpContext.Configure(accessor);
            OR
            IEasLog.ConfigureHttpContext(accessor);
    
        }
     */
    internal static class EasHttpContext
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor? m_httpContextAccessor;


        internal static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor? httpContextAccessor)
        {
            m_httpContextAccessor = httpContextAccessor;
        }


        internal static Microsoft.AspNetCore.Http.HttpContext? Current
        {
            get
            {
                if (m_httpContextAccessor == null) return null;
                return m_httpContextAccessor.HttpContext;
            }
        }

    }
}
