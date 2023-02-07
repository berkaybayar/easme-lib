using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EasMe.Authorization.Filters
{
    /// <summary>
    /// EndPointAuthorizationFilter to select permission to each endpoint for <see cref="HttpContext.User"/> must be authorized before this.
    /// If user authorization is not true it will not check
    /// </summary>
    public class EndPointAuthorizationFilterAttribute : ActionFilterAttribute
    {
        public EndPointAuthorizationFilterAttribute(object uniqueActionCode)
        {
            _uniqueActionCode = uniqueActionCode.ToString();
            if (string.IsNullOrEmpty(_uniqueActionCode))
                throw new ArgumentNullException(nameof(uniqueActionCode) + " can not be null or empty");
        }
        
        private readonly string _uniqueActionCode;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity is { IsAuthenticated: true })
            {
                return;
            }
            var endPointPermissionString = context.HttpContext.User.FindFirst(EasMeClaimType.EndPointPermissions)?.Value ?? "";
            var permList = AuthorizationHelper.SplitPermissions(endPointPermissionString);
            if (permList.Length == 0 || !permList.Contains(_uniqueActionCode))
            {
                context.Result = new ForbidResult();
            }
          
        }
    }
}
