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
    [ModuleAuthorized(UserAccessModule.Admissions)]
    public class AdmissionsController : Controller
    {
        private readonly string BaseUrl;
        private readonly IHttpContextAccessor httpContextAccessor;

        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public AdmissionsController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        #region Index
        public async Task<IActionResult> Index(string patientId)
        {
            session?.SetCurrentClientId(patientId);
            ViewBag.PatientId = patientId;
            var admissions = await GetByPatientId(patientId);
            admissions = admissions
                .OrderBy(x => x.IsDischarged)
                .ThenByDescending(x => x.AdmissionDate)
                .ToList();

            ViewBag.HasDeaDeathCertificates = await HasDeaDeathCertificates(patientId);

            return View(admissions);
        }

        #endregion Index

        #region Create
        public async Task<IActionResult> Create(string patientId)
        {
            var admissions = await GetByPatientId(patientId);
            if (admissions.Any(x => x.IsDischarged == false))
            {
                return RedirectToAction("Index", new { patientId });
            }

            ViewBag.PatientId = patientId;
            var admissionDto = new AdmissionsDto
            {
                AdmissionDate = DateTime.Now,
                AdmissionTime = DateTime.Now
            };
            return View(admissionDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdmissionsDto admission)
        {
            var admissionAdded = await CreateAdmission(admission);
            if (admissionAdded == null)
            {
                ViewBag.PatientId = admission.PatientID;
                return View(admission);
            }

            return RedirectToAction("Details", new
            {
                admissionId = admissionAdded.AdmissionID.ToString(),
                patientId = admissionAdded.PatientID
            });
        }

        // redirect to discharge create page
        public IActionResult AdmissionDischargeCreate(string admissionId)
        {
            session?.SetCurrentAdmissionId(admissionId);
            return RedirectToAction("Create", "Discharge");
        }

        #endregion Create

        #region Edit
        public async Task<IActionResult> Edit(string admissionId, string patientId)
        {
            ViewBag.PatientId = patientId;
            var admission = await GetById(admissionId);
            if (admission == null)
            {
                return RedirectToAction("Index", new { patientId });
            }

            return View(admission);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdmissionsDto admission)
        {
            var admissionUpdated = await UpdateAdmission(admission);
            if (admissionUpdated == null)
            {
                ViewBag.PatientId = admission.PatientID;
                return View(admission);
            }

            return RedirectToAction("Index", new { patientId = admissionUpdated.PatientID });
        }

        #endregion Edit

        #region Details
        public async Task<IActionResult> Details(string admissionId, string patientId)
        {
            ViewBag.PatientId = patientId;
            var admission = await GetById(admissionId);
            if (admission == null)
            {
                return RedirectToAction("Index", new { patientId });
            }

            return View(admission);
        }

        public async Task<IActionResult> AdmissionDischargeDetail(string admissionId, string patientId)
        {
            ViewBag.PatientId = patientId;
            var admission = await GetById(admissionId);
            if (admission == null)
            {
                return RedirectToAction("Index", new { patientId });
            }

            var discharges = await GetDischargesByAdmission(admissionId);
            admission.Discharges = discharges;
            var dischargeStatuses = await GetDischargeStatuesAsync();
            ViewBag.DischargeStatuses = dischargeStatuses;

            if (discharges.Any())
            {
                return View(admission);
            }

            var deathCertificates = await GetDeathCertificatesByAdmission(admissionId);
            admission.DeathCertificates = deathCertificates;

            var patient = await GetPatientById(patientId);
            if (patient == null)
            {
                return View(admission);
            }

            var chiefDom = await GetChiefdomById(patient.ChiefdomID);
            ViewBag.ClientId = patientId;
            ViewBag.ChiefdomName = chiefDom?.Name ?? string.Empty;
            var clientName = patient.FirstName;
            if (!string.IsNullOrEmpty(patient.MiddleName))
            {
                clientName += " " + patient.MiddleName;
            }
            clientName += " " + patient.LastName;
            ViewBag.ClientName = clientName;
            ViewBag.Patient = patient;
            ViewBag.Admission = admission;

            return View(admission);
        }

        #endregion Details

        #region Helper

        #region Patient
        private async Task<PatientDto?> GetPatientById(string patientId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Patients/GetClientById?patientId={patientId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientDto>(result);
        }
        #endregion Patient

        #region Chiefdom
        private async Task<ChiefdomDto?> GetChiefdomById(int chiefdomId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Chiefdoms/GetChiefdomById?ChiefdomId={chiefdomId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ChiefdomDto>(result);
        }
        #endregion Chiefdom

        private async Task<bool> HasDeaDeathCertificates(string patientId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DeathCertificates/HasDeathCertificates/{patientId}");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<bool>(result);
        }

        private async Task<List<DeathCertificateDto>?> GetDeathCertificatesByAdmission(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DeathCertificates/LoadDeathCertificates/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<DeathCertificateDto>();
            }
            string result = await response.Content.ReadAsStringAsync();
            var discharges = JsonConvert.DeserializeObject<List<DeathCertificateDto>>(result);

            return discharges ?? new List<DeathCertificateDto>();
        }

        private async Task<List<DischargeStatusDto>> GetDischargeStatuesAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DischargeStatus/LoadDischargeStatuses");
            if (!response.IsSuccessStatusCode)
            {
                return new List<DischargeStatusDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            var dischargeStatues = JsonConvert.DeserializeObject<List<DischargeStatusDto>>(result);
            return dischargeStatues ?? new List<DischargeStatusDto>();
        }

        private async Task<List<DischargesDto>> GetDischargesByAdmission(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Discharge/LoadDischarges/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<DischargesDto>();
            }
            string result = await response.Content.ReadAsStringAsync();
            var discharges = JsonConvert.DeserializeObject<List<DischargesDto>>(result);

            return discharges ?? new List<DischargesDto>();
        }

        private async Task<List<AdmissionsDto>> GetByPatientId(string patientId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Admissions/LoadAdmission/{patientId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<AdmissionsDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            var admissions = JsonConvert.DeserializeObject<List<AdmissionsDto>>(result);
            return admissions ?? new List<AdmissionsDto>();
        }

        private async Task<AdmissionsDto?> GetById(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Admissions/FindAdmissionByKey/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            var admission = JsonConvert.DeserializeObject<AdmissionsDto>(result);
            return admission;
        }

        private async Task<AdmissionsDto?> CreateAdmission(AdmissionsDto admission)
        {
            AdmissionDtoDateTime(admission);

            var data = JsonConvert.SerializeObject(admission);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/Admissions/AddAdmission", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AdmissionsDto>(result);
        }

        private async Task<AdmissionsDto?> UpdateAdmission(AdmissionsDto admission)
        {
            AdmissionDtoDateTime(admission);

            var data = JsonConvert.SerializeObject(admission);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/Admissions/EditAdmission", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AdmissionsDto>(result);
        }

        private void AdmissionDtoDateTime(AdmissionsDto admission)
        {
            var admissionDateTime = GetAdmissionDateTime(admission.AdmissionDate, admission.AdmissionTime);
            admission.AdmissionDate = admissionDateTime;
            admission.AdmissionTime = admissionDateTime;
        }

        private DateTime GetAdmissionDateTime(DateTime dateOfAdmission, DateTime timeOfAdmission)
        {
            var date = dateOfAdmission.Date;
            var time = timeOfAdmission.TimeOfDay;
            return date + time;
        }

        #endregion Helper
    }
}