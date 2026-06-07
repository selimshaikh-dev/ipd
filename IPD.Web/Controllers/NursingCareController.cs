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
    [ModuleAuthorized(UserAccessModule.NursingCare)]
    public class NursingCareController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public NursingCareController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var nursingCares = await GetByCurrentAdmissionId();

            return View(nursingCares);
        }

        public IActionResult Create()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var nursingCare = new NursingCaresDto
            {
                DateOfCare = DateTime.Now,
                TimeOfCare = DateTime.Now
            };
            return View(nursingCare);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NursingCaresDto nursingCare)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var nursingCareAdded = await CreateNursingCare(nursingCare);

            if (nursingCareAdded == null)
                return View(nursingCare);

            return RedirectToAction("Details", new
            {
                nursingCareId = nursingCareAdded.NursingCareID.ToString()
            });
        }

        public async Task<IActionResult> Edit(string nursingCareId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var nursingCare = await GetById(nursingCareId);

            if (nursingCare == null)
                return RedirectToAction("Index");

            return View(nursingCare);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NursingCaresDto nursingCare)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var nursingCareUpdated = await UpdateNursingCare(nursingCare);

            if (nursingCareUpdated == null)
                return View(nursingCare);

            return RedirectToAction("Details", new
            {
                nursingCareId = nursingCareUpdated.NursingCareID.ToString()
            });
        }

        public async Task<IActionResult> Details(string nursingCareId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var nursingCare = await GetById(nursingCareId);

            if (nursingCare == null)
                return RedirectToAction("Index");

            return View(nursingCare);
        }

        private async Task<List<NursingCaresDto>> GetByCurrentAdmissionId()
        {
            string admissionId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();

            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/NursingCares/LoadNursingCares/{admissionId}");

            if (!response.IsSuccessStatusCode)
                return new List<NursingCaresDto>();

            string result = await response.Content.ReadAsStringAsync();
            var nursingCares = JsonConvert.DeserializeObject<List<NursingCaresDto>>(result);

            return nursingCares ?? new List<NursingCaresDto>();
        }

        private async Task<NursingCaresDto?> GetById(string nursingCareId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/NursingCares/FindNursingCareByKey/{nursingCareId}");

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            var nursingCare = JsonConvert.DeserializeObject<NursingCaresDto>(result);

            return nursingCare;
        }

        private async Task<NursingCaresDto?> CreateNursingCare(NursingCaresDto nursingCare)
        {
            RemapNursingCareDto(nursingCare);

            nursingCare.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString());
            var data = JsonConvert.SerializeObject(nursingCare);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/NursingCares/AddNursingCare", httpContent);

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<NursingCaresDto>(result);
        }

        private async Task<NursingCaresDto?> UpdateNursingCare(NursingCaresDto nursingCare)
        {
            RemapNursingCareDto(nursingCare);

            var data = JsonConvert.SerializeObject(nursingCare);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/NursingCares/EditNursingCare", httpContent);

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<NursingCaresDto>(result);
        }

        private void RemapNursingCareDto(NursingCaresDto nursingCare)
        {
            var nursingCareDateTime = GetNursingCareDateTime(nursingCare.DateOfCare, nursingCare.TimeOfCare);
            nursingCare.DateOfCare = nursingCareDateTime;
            nursingCare.TimeOfCare = nursingCareDateTime;
        }

        private DateTime GetNursingCareDateTime(DateTime dateOfNote, DateTime timeOfNote)
        {
            var date = dateOfNote.Date;
            var time = timeOfNote.TimeOfDay;
            return date + time;
        }
    }
}