using IPD.Domain.Dto;
using IPD.Web.Extensions;
using IPD.Web.Filters;
using IPD.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Web.Controllers
{
    [ModuleAuthorized(UserAccessModule.PostSurgeries)]
    public class PostSurgeriesController : Controller
    {
        private readonly string BaseUrl;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient httpClient;
        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public PostSurgeriesController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        public IActionResult Index(string surgeryId)
        {
            return RedirectToAction("Details", new { surgeryId });
        }

        public IActionResult Create(string surgeryId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var surgeryDto = new PostSurgeriesDto
            {
                SurgeryID = Guid.Parse(surgeryId)
            };

            return View(surgeryDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostSurgeriesDto postSurgery)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            using var client = new HttpClient();
            var data = JsonConvert.SerializeObject(postSurgery);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/PostSurgeries/AddPostSurgeries", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return View(postSurgery);
            }

            string result = await response.Content.ReadAsStringAsync();
            var postSurgeryAdded = JsonConvert.DeserializeObject<SurgeriesDto>(result);
            if (postSurgeryAdded == null)
            {
                return View(postSurgery);
            }

            return RedirectToAction("Details", new
            {
                surgeryId = postSurgeryAdded.SurgeryID.ToString()
            });
        }

        public async Task<IActionResult> Edit(string postSurgeryId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            using var client = new HttpClient();

            var response = await client.GetAsync($"{BaseUrl}/PostSurgeries/FindPostSurgeryByKey/{postSurgeryId}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Surgeries");
            }

            string result = await response.Content.ReadAsStringAsync();
            var postSurgery = JsonConvert.DeserializeObject<PostSurgeriesDto>(result);
            if (postSurgery == null)
            {
                return RedirectToAction("Index", "Surgeries");
            }

            return View(postSurgery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostSurgeriesDto postSurgery)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            using var client = new HttpClient();
            var data = JsonConvert.SerializeObject(postSurgery);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/PostSurgeries/EditPostSurgeries", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return View(postSurgery);
            }

            string result = await response.Content.ReadAsStringAsync();
            var postSurgeryUpdated = JsonConvert.DeserializeObject<SurgeriesDto>(result);
            if (postSurgeryUpdated == null)
            {
                return View(postSurgery);
            }

            return RedirectToAction("Details", new
            {
                surgeryId = postSurgeryUpdated.SurgeryID.ToString()
            });
        }

        public async Task<IActionResult> Details(string surgeryId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            ViewBag.SurgeryId = surgeryId;
            using var client = new HttpClient();

            var response = await client.GetAsync($"{BaseUrl}/PostSurgeries/LoadPostSurgeries/{surgeryId}");
            if (!response.IsSuccessStatusCode)
            {
                return View();
            }

            string result = await response.Content.ReadAsStringAsync();
            var postSurgeries = JsonConvert.DeserializeObject<List<PostSurgeriesDto>>(result);
            if (postSurgeries == null || !postSurgeries.Any())
            {
                return View();
            }

            var postSurgery = postSurgeries.FirstOrDefault();
            if (postSurgery == null)
            {
                return View();
            }

            return View(postSurgery);
        }
    }
}