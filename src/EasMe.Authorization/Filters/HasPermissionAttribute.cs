using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace EasMe.Authorization.Filters;

/// <summary>
///     EndPointAuthorizationFilter to select permission to each endpoint for <see cref="HttpContext.User" /> must be
///     authorized before this.
///     If user authorization is not true it will not check
///     <br />
///     <example>
///         <br />
///         Example: Add ActionCode to <see cref="HttpContext.User" /> claims with
///         <see cref="EasMeClaimType.EndPointPermissions" /> claim type and
///         separate multiple ActionCode with ","
///         <br />
///         <br />
///         It is recommended to use Enum types for ActionCodes and add the intended permissions to
///         <see cref="HttpContext.User" /> claims.
///     </example>
/// </summary>
public class HasPermissionAttribute : ActionFilterAttribute
{
    private readonly string _actionCode;

    public HasPermissionAttribute(object actionCode)
    {
        _actionCode = actionCode.ToString() ?? "";
        if (string.IsNullOrEmpty(_actionCode))
            throw new ArgumentNullException(nameof(actionCode));
    }

    public override void OnActionExecuting(ActionExecutingContext _dbContext)
    {
        if (_dbContext.HttpContext.User.Identity is not { IsAuthenticated: true })
        {
            Trace.WriteLine("Not authorized");
            return;
        }

        var endPointPermissionString =
            _dbContext.HttpContext.User.FindFirst(EasMeClaimType.EndPointPermissions)?.Value ?? "";
        if (string.IsNullOrEmpty(endPointPermissionString))
        {
            _dbContext.Result = new ForbidResult();
            return;
        }

        Trace.WriteLine(endPointPermissionString);
        var permList = AuthorizationHelper.SplitPermissions(endPointPermissionString);
        Trace.WriteLine("Permission List" + JsonConvert.SerializeObject(permList));
        if (permList.Length == 0)
        {
            _dbContext.Result = new ForbidResult();
            return;
        }

        if (!permList.Contains(_actionCode))
        {
            _dbContext.Result = new ForbidResult();
        }
    }
}