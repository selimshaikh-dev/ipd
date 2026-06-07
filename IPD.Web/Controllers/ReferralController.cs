using IPD.Web.Extensions;
using IPD.Web.Filters;
using IPD.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Web.Controllers
{
    [ModuleAuthorized(UserAccessModule.Referral)]
    public class ReferralController : Controller
    {
        private readonly string BaseUrl;
        private readonly IAppSettings appSettings;
        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public ReferralController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.appSettings = appSettings;
            BaseUrl = this.appSettings.BaseUrl;
        }

        public IActionResult ReferralOptions()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            return View();
        }
    }
}