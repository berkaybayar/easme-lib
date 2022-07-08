namespace EasMe
{
    /*
        [PROGRAM.CS] => FOR WEB APPLICATIONS 
        builder.Services.AddHttpContextAccessor();
        
        [CONTROLLER]
        public ControllerClass(IHttpContextAccessor accessor)
        {
            EasHttpContext.Configure(accessor);
        }
     */
    public static class EasHttpContext
    {
        private static Microsoft.AspNetCore.Http.IHttpContextAccessor? m_httpContextAccessor;


        public static void Configure(Microsoft.AspNetCore.Http.IHttpContextAccessor? httpContextAccessor)
        {
            m_httpContextAccessor = httpContextAccessor;
        }


        public static Microsoft.AspNetCore.Http.HttpContext? Current
        {
            get
            {
                if (m_httpContextAccessor == null) return null;
                return m_httpContextAccessor.HttpContext;
            }
        }

    }
}
