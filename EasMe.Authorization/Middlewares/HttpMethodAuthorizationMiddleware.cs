using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EasMe.Authorization.Middlewares
{
    /// <summary>
    /// Is to authorize every user by <see cref="HttpMethod"/> permissions
    /// </summary>
    public class HttpMethodAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpMethodAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity is not { IsAuthenticated: true })
            {
                await _next(context);
                return;
            }

            var permissionString = context.User.FindFirst(EasMeClaimType.HttpMethodPermissions)?.Value ?? "";
            var permList = AuthorizationHelper.SplitPermissions(permissionString);
            if (permList.Length == 0)
            {
                context.Response.StatusCode = 403;
                return;
            }
            var httpMethod = context.Request.Method;
            if(!permList.Contains(httpMethod))
            {
                context.Response.StatusCode = 403;
                return;
            }
            await _next(context);
        }
    }
}
