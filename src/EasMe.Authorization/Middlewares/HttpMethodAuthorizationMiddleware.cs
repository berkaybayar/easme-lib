using Microsoft.AspNetCore.Http;

namespace EasMe.Authorization.Middlewares;

/// <summary>
///     Is to authorize every user by <see cref="HttpMethod" /> permissions
/// </summary>
public class HttpMethodAuthorizationMiddleware {
    private readonly RequestDelegate _next;

    public HttpMethodAuthorizationMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext _dbContext) {
        if (_dbContext.User.Identity is not { IsAuthenticated: true }) {
            await _next(_dbContext);
            return;
        }

        var permissionString = _dbContext.User.FindFirst(EasMeClaimType.HttpMethodPermissions)?.Value ?? "";
        var permList = AuthorizationHelper.SplitPermissions(permissionString);
        if (permList.Length == 0) {
            _dbContext.Response.StatusCode = 403;
            return;
        }

        var httpMethod = _dbContext.Request.Method;
        if (!permList.Contains(httpMethod)) {
            _dbContext.Response.StatusCode = 403;
            return;
        }

        await _next(_dbContext);
    }
}