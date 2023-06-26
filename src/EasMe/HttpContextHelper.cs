using Microsoft.AspNetCore.Http;

namespace EasMe;

/*
    [PROGRAM.CS] => FOR WEB APPLICATIONS 
    builder.Services.AddHttpContextAccessor();
 */
/// <summary>
///     Access to <see cref="HttpContext" /> in anywhere. Must register <see cref="HttpContextAccessor" /> in
///     <see cref="Program.cs" />
/// </summary>
public static class HttpContextHelper
{
    private static readonly HttpContextAccessor? Accessor = new();

    public static HttpContext? Current => Accessor?.HttpContext;
}