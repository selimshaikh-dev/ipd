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
    [ModuleAuthorized(UserAccessModule.Vitals)]
    public class VitalsController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public VitalsController(IHttpContextAccessor httpContextAccessor,
            IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            #region capture age start

            var clientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            using var clients = new HttpClient();
            var responses = await clients.GetAsync($"{BaseUrl}/Patients/GetClientById?PatientId={clientId}");

            if (responses.IsSuccessStatusCode)
            {
                string results = await responses.Content.ReadAsStringAsync();
                var clientInfo = JsonConvert.DeserializeObject<PatientDto>(results);

                if (clientInfo != null)
                {
                    ViewBag.age = Extensions.SessionExtensions.CalculateClientsAge(clientInfo.DOB);
                }
            }

            #endregion capture age start

            var vitals = await GetByCurrentAdmissionId();

            return View(vitals);
        }

        #endregion Index

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var existVital = await GetExistedVital();
            var vital = new VitalDto
            {
                DateCollected = DateTime.Now,
                TimeCollected = DateTime.Now,
                Height = existVital?.Height ?? 0,
            };
            return View(vital);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VitalDto vital)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var vitalAdded = await CreateVitals(vital);

            if (vitalAdded == null)
                return View(vital);

            return RedirectToAction("Details", new
            {
                vitalId = vitalAdded.VitalID.ToString()
            });
        }

        #endregion Create

        #region Edit
        public async Task<IActionResult> Edit(string vitalId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var vital = await GetById(vitalId);

            if (vital == null)
                return RedirectToAction("Index");

            return View(vital);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VitalDto vital)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var vitalUpdated = await UpdateVitals(vital);

            if (vitalUpdated == null)
                return View(vital);

            return RedirectToAction("Details", new
            {
                vitalId = vitalUpdated.VitalID.ToString()
            });
        }

        #endregion Edit

        #region Details
        public async Task<IActionResult> Details(string vitalId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var vital = await GetById(vitalId);

            if (vital == null)
                return RedirectToAction("Index");

            return View(vital);
        }

        #endregion Details

        #region Vital helper
        private async Task<VitalDto?> GetExistedVital()
        {
            var allVitals = await GetByCurrentAdmissionId();
            return allVitals.Count > 0 ? allVitals.FirstOrDefault() : null;
        }

        private async Task<List<VitalDto>> GetByCurrentAdmissionId()
        {
            string admissionId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();

            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Vitals/LoadVitals/{admissionId}");

            if (!response.IsSuccessStatusCode)
                return new List<VitalDto>();

            string result = await response.Content.ReadAsStringAsync();
            var vitals = JsonConvert.DeserializeObject<List<VitalDto>>(result);
            return vitals ?? new List<VitalDto>();
        }

        private async Task<VitalDto?> GetById(string vitalId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Vitals/FindVitalByKey/{vitalId}");

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            var vital = JsonConvert.DeserializeObject<VitalDto>(result);

            return vital;
        }

        private async Task<VitalDto?> CreateVitals(VitalDto vital)
        {
            RemapVitalsDto(vital);
            if (vital != null)
            {
                if (vital.Weight != null && vital.Height != null)
                {
                    decimal bmi = Extensions.SessionExtensions.CalculateBMI((decimal)vital.Weight, (decimal)vital.Height);
                    vital.BMI = bmi;
                }

                if (vital.BMI != null)
                {
                    var clientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
                    using var clients = new HttpClient();
                    var responses = await clients.GetAsync($"{BaseUrl}/Patients/GetClientById?PatientId={clientId}");

                    if (responses.IsSuccessStatusCode)
                    {
                        string results = await responses.Content.ReadAsStringAsync();
                        var clientInfo = JsonConvert.DeserializeObject<PatientDto>(results);

                        if (clientInfo != null)
                        {
                            var age = Extensions.SessionExtensions.CalculateClientsAge(clientInfo.DOB);
                            vital.NutritionalStatus = Extensions.SessionExtensions.DetermineAdultsNutritionalStatus((decimal)vital.BMI, age);
                        }
                    }
                }
            }

            vital.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString());
            var data = JsonConvert.SerializeObject(vital);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/Vitals/AddVitals", httpContent);

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            var outputVital = JsonConvert.DeserializeObject<VitalDto>(result);

            return outputVital;
        }

        private async Task<VitalDto?> UpdateVitals(VitalDto vital)
        {
            RemapVitalsDto(vital);

            if (vital != null)
            {
                if (vital.Weight != null && vital.Height != null)
                {
                    decimal bmi = Extensions.SessionExtensions.CalculateBMI((decimal)vital.Weight, (decimal)vital.Height);
                    vital.BMI = bmi;
                }
                if (vital.BMI != null)
                {
                    var clientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
                    using var clients = new HttpClient();
                    var responses = await clients.GetAsync($"{BaseUrl}/Patients/GetClientById?PatientId={clientId}");

                    if (responses.IsSuccessStatusCode)
                    {
                        string results = await responses.Content.ReadAsStringAsync();
                        var clientInfo = JsonConvert.DeserializeObject<PatientDto>(results);

                        if (clientInfo != null)
                        {
                            var age = Extensions.SessionExtensions.CalculateClientsAge(clientInfo.DOB);
                            vital.NutritionalStatus = Extensions.SessionExtensions.DetermineAdultsNutritionalStatus((decimal)vital.BMI, age);
                        }
                    }
                }
            }

            var data = JsonConvert.SerializeObject(vital);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/Vitals/EditVitals", httpContent);

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VitalDto>(result);
        }

        private void RemapVitalsDto(VitalDto vital)
        {
            var vitalDateTime = GetVitalDateTime(vital.DateCollected, vital.TimeCollected);
            vital.DateCollected = vitalDateTime;
            vital.TimeCollected = vitalDateTime;
        }

        private DateTime GetVitalDateTime(DateTime vitalDate, DateTime vitalTime)
        {
            var date = vitalDate.Date;
            var time = vitalTime.TimeOfDay;
            return date + time;
        }

        #endregion Vital helper
    }
}