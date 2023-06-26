using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace EasMe.Authorization;

/// <summary>
///     Is to authorize every user by <see cref="HttpMethod" /> permissions
/// </summary>
public class HttpMethodAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public HttpMethodAuthorizationMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext) {
        if (httpContext.User.Identity is not { IsAuthenticated: true }) {
            Trace.WriteLine("Not authenticated");
            await _next(httpContext);
            return;
        }

        var permissionString = httpContext.User.FindFirst(EasMeClaimType.HttpMethodPermissions)?.Value ?? "";
        var permList = InternalHelper.SplitPermissions(permissionString);
        if (permList.Length == 0) {
            httpContext.Response.StatusCode = 403;
            return;
        }

        var httpMethod = httpContext.Request.Method;
        if (!permList.Contains(httpMethod)) {
            httpContext.Response.StatusCode = 403;
            return;
        }

        await _next(httpContext);
    }
}