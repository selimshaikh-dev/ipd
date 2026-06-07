using IPD.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Web.Filters
{
    /// <summary>
    /// Authorize user base on UserAccess
    /// Only apply on controller/action you want to authorize
    /// </summary>
    public class ModuleAuthorizedAttribute : ActionFilterAttribute
    {
        public UserAccessModule[] Modules { get; set; } = new UserAccessModule[] { };

        public ModuleAuthorizedAttribute(params UserAccessModule[] modules)
        {
            Modules = modules;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var loginResult = new RedirectResult("/Users/Index");
            var session = context.HttpContext.Session;
            if(session == null)
            {
                context.Result = loginResult;
                return;
            }

            var currentUser = session.GetCurrentUsers();
            if (currentUser == null)
            {
                context.Result = loginResult;
                return;
            }

            var userAccesses = currentUser.UserAccess;
            if (userAccesses == null || !userAccesses.Any())
            {
                context.Result = loginResult;
                return;
            }

            var isModuleEnabled = userAccesses.Any(x => Modules.Any(y => (byte)y == x.Module));
            if (!isModuleEnabled)
            {
                var values = new RouteValueDictionary(new
                {
                    action = "NotAllow",
                    controller = "Users",
                    message = "NotAllow"
                });
                context.Result = new RedirectToRouteResult(values);
               // var message = "NotAllow";
                //context.Result = new RedirectResult("/Clients/ProfileSearch/message?="+ message);
                return;
            } 

            await base.OnActionExecutionAsync(context, next);
        }
    }
}
