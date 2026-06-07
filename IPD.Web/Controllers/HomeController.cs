using IPD.Domain.Dto;
using IPD.Web.Models;
using IPD.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace IPD.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly string BaseUrl;
        private readonly ILogger<HomeController> logger;
        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IAppSettings appSettings)
        {
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
        }

        public IActionResult Console()
        {
            return View();
        }

        public async Task<IActionResult> ForgotPassword()
        {
            var user = new LoginRecoveryRequestsDto();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(LoginRecoveryRequestsDto loginRecoveryRequest)
        {
            var userRegistrationJson = JsonConvert.SerializeObject(loginRecoveryRequest);
            using (var client = new HttpClient())
            {
                HttpContent httpContent = new StringContent(userRegistrationJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{BaseUrl}/Users/SaveRecoveryRequest", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return RedirectToAction("RecoveryConfirmation", "Home");
                }
            }
            return View(loginRecoveryRequest);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Error { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RecoveryConfirmation()
        {
            return View();
        }
    }
}