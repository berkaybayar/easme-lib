using Microsoft.AspNetCore.Http;

namespace EasMe;

/*
    [PROGRAM.CS] => FOR WEB APPLICATIONS 
    builder.Services.AddHttpContextAccessor();
 */
public static class HttpContextHelper
{
    private static readonly HttpContextAccessor? Accessor = new();

    public static HttpContext? Current => Accessor?.HttpContext;

    //public static void test()
    //{
    //    Current.Session.SetString();
    //}
}