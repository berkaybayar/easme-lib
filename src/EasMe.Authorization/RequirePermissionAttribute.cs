using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EasMe.Authorization;

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
public class RequirePermissionAttribute : ActionFilterAttribute
{
    private readonly string _actionCode;

    /// <summary>
    /// Constructor for <see cref="RequirePermissionAttribute"/>. Converts given object to string and use it as ActionCode.
    /// </summary>
    /// <param name="actionCode"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public RequirePermissionAttribute(object actionCode) {
        _actionCode = actionCode.ToString() ?? "";
        if (string.IsNullOrEmpty(_actionCode))
            throw new ArgumentNullException(nameof(_actionCode));
    }
    
    /// <summary>
    ///   Constructor for <see cref="RequirePermissionAttribute" />. Converts given enum to string and use it as ActionCode.
    /// </summary>
    /// <param name="enumActionCode"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public RequirePermissionAttribute(Enum enumActionCode) {
        _actionCode = enumActionCode.ToString() ?? "";
        if (string.IsNullOrEmpty(_actionCode))
            throw new ArgumentNullException(nameof(_actionCode));
    }
    
    /// <summary>
    ///  Constructor for <see cref="RequirePermissionAttribute" />. Use given string as ActionCode.
    /// </summary>
    /// <param name="actionCode"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public RequirePermissionAttribute(string actionCode) {
        _actionCode = actionCode;
        if (string.IsNullOrEmpty(_actionCode))
            throw new ArgumentNullException(nameof(_actionCode));
    }

    public override void OnActionExecuting(ActionExecutingContext actionExecutingContext) {
        if (actionExecutingContext.HttpContext.User.Identity is not { IsAuthenticated: true }) {
            Trace.WriteLine("Not authenticated");
            return;
        }

        var endPointPermissionString =
            actionExecutingContext.HttpContext.User.FindFirst(EasMeClaimType.EndPointPermissions)?.Value ?? "";
        if (string.IsNullOrEmpty(endPointPermissionString)) {
            actionExecutingContext.Result = new ForbidResult();
            return;
        }

        Trace.WriteLine(endPointPermissionString);
        var permList = InternalHelper.SplitPermissions(endPointPermissionString);
        Trace.WriteLine("Permission List: " + string.Join(",", permList));
        if (permList.Length == 0) {
            actionExecutingContext.Result = new ForbidResult();
            return;
        }

        if (!permList.Contains(_actionCode)) actionExecutingContext.Result = new ForbidResult();
    }
}