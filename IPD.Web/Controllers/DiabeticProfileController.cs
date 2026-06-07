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
    [ModuleAuthorized(UserAccessModule.DiabeticProfile)]
    public class DiabeticProfileController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public DiabeticProfileController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var vital = await GetByCurrentAdmissionId();
            return View(vital);
        }

        public IActionResult Create()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var diabeticProfile = new DiabeticsProfileDto
            {
                DateCollected = DateTime.Now,
                TimeCollected = DateTime.Now
            };

            return View(diabeticProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DiabeticsProfileDto diabeticProfile)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var DiabeticProfileAdded = await CreateDiabeticProfile(diabeticProfile);

            if (DiabeticProfileAdded == null)
                return View(diabeticProfile);

            return RedirectToAction("Details", new
            {
                DiabeticProfileID = DiabeticProfileAdded.DiabeticProfileID.ToString()
            });
        }

        public async Task<IActionResult> Edit(string diabeticProfileId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var diabeticProfile = await GetById(diabeticProfileId);

            if (diabeticProfile == null)
                return RedirectToAction("Index");

            return View(diabeticProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DiabeticsProfileDto diabeticProfile)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var ProfileUpdated = await UpdateDiabeticProfile(diabeticProfile);

            if (ProfileUpdated == null)
                return View(diabeticProfile);

            return RedirectToAction("Details", new
            {
                diabeticProfileId = ProfileUpdated.DiabeticProfileID.ToString()
            });
        }

        public async Task<IActionResult> Details(string diabeticProfileId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var diabeticProfile = await GetById(diabeticProfileId);

            if (diabeticProfile == null)
                return RedirectToAction("Index");

            return View(diabeticProfile);
        }

        private async Task<List<DiabeticsProfileDto>> GetByCurrentAdmissionId()
        {
            string admissionId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();

            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DiabeticProfiles/LoadDiabeticProfiles/{admissionId}");

            if (!response.IsSuccessStatusCode)
                return new List<DiabeticsProfileDto>();

            string result = await response.Content.ReadAsStringAsync();
            var diabeticProfiles = JsonConvert.DeserializeObject<List<DiabeticsProfileDto>>(result);

            return diabeticProfiles ?? new List<DiabeticsProfileDto>();
        }

        private async Task<DiabeticsProfileDto?> GetById(string diabeticProfile)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DiabeticProfiles/FindDiabeticProfileByKey/{diabeticProfile}");

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            var profile = JsonConvert.DeserializeObject<DiabeticsProfileDto>(result);

            return profile;
        }

        private async Task<DiabeticsProfileDto?> CreateDiabeticProfile(DiabeticsProfileDto diabeticProfile)
        {
            RemapDiabeticProfileDto(diabeticProfile);

            diabeticProfile.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString());
            var data = JsonConvert.SerializeObject(diabeticProfile);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/DiabeticProfiles/AddDiabeticProfile", httpContent);

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DiabeticsProfileDto>(result);
        }

        private async Task<DiabeticsProfileDto?> UpdateDiabeticProfile(DiabeticsProfileDto diabeticProfile)
        {
            RemapDiabeticProfileDto(diabeticProfile);

            var data = JsonConvert.SerializeObject(diabeticProfile);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/DiabeticProfiles/EditDiabeticProfile", httpContent);

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DiabeticsProfileDto>(result);
        }

        private void RemapDiabeticProfileDto(DiabeticsProfileDto diabeticProfile)
        {
            var diabeticProfileDateTime = GetDiabeticProfileDateTime(diabeticProfile.DateCollected, diabeticProfile.TimeCollected);
            diabeticProfile.DateCollected = diabeticProfileDateTime;
            diabeticProfile.TimeCollected = diabeticProfileDateTime;
        }

        private DateTime GetDiabeticProfileDateTime(DateTime diabeticProfileDate, DateTime diabeticProfileTime)
        {
            var date = diabeticProfileDate.Date;
            var time = diabeticProfileTime.TimeOfDay;
            return date + time;
        }
    }
}