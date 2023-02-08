using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace EasMe.Authorization.Filters
{
    /// <summary>
    /// EndPointAuthorizationFilter to select permission to each endpoint for <see cref="HttpContext.User"/> must be authorized before this.
    /// If user authorization is not true it will not check
    /// </summary>
    public class EndPointAuthorizationFilterAttribute : ActionFilterAttribute
    {
        public EndPointAuthorizationFilterAttribute(object actionCode)
        {
            _actionCode = actionCode.ToString() ?? "";
            if (string.IsNullOrEmpty(_actionCode))
                throw new ArgumentNullException(nameof(actionCode));
        }
        
        private readonly string _actionCode;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity is { IsAuthenticated: true })
            {
                Trace.WriteLine("Not authorized");
                return;
            }
            var endPointPermissionString = context.HttpContext.User.FindFirst(EasMeClaimType.EndPointPermissions)?.Value ?? "";
            if (string.IsNullOrEmpty(endPointPermissionString))
            {
                context.Result = new ForbidResult();
                return;
            }
            Trace.WriteLine(endPointPermissionString);
            var permList = AuthorizationHelper.SplitPermissions(endPointPermissionString);
            Trace.WriteLine("Permission List" + JsonConvert.SerializeObject(permList));
            if (permList.Length == 0)
            {
                context.Result = new ForbidResult();
                return;
            }
            if (!permList.Contains(_actionCode))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
