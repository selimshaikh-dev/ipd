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
    [ModuleAuthorized(UserAccessModule.DeathCertificates)]
    public class DeathCertificatesController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;
        private readonly ILogger<DeathCertificatesController> logger;

        public DeathCertificatesController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory, ILogger<DeathCertificatesController> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.logger = logger;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;

                if (!await AssignAdmissionAndPatientInformationToView())
                {
                    return RedirectToAction("Index", "Admissions");
                }

                var deathCertificateDto = new DeathCertificateDto
                {
                    DateOfDeath = DateTime.Now,
                    TimeOfDeath = DateTime.Now,
                    AdmissionID = Guid.Parse(admissionId)
                };

                return View(deathCertificateDto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeathCertificateDto deathCertificate)
        {
            try
            {
                var deathCertificateAdded = await CreateDeathCertificate(deathCertificate);
                if (deathCertificateAdded != null)
                {
                    await UpdateAdmissionDischarge(deathCertificate.AdmissionID);

                    return RedirectToAction("Details", new
                    {
                        deathCertificateId = deathCertificateAdded.DeathCertificateID.ToString()
                    });
                }

                if (!await AssignAdmissionAndPatientInformationToView())
                {
                    return RedirectToAction("Index", "Admissions");
                }

                return View(deathCertificate);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        public async Task<IActionResult> Edit(string deathCertificateId)
        {
            try
            {
                if (!await AssignAdmissionAndPatientInformationToView())
                {
                    return RedirectToAction("Index", "Admissions");
                }

                var deathCertificate = await GetById(deathCertificateId);
                if (deathCertificate == null)
                {
                    return RedirectToAction("Create");
                }
                return View(deathCertificate);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DeathCertificateDto deathCertificates)
        {
            try
            {
                var patientId = session?.GetCurrentClientId() ?? string.Empty;
                var deathCertificateAdded = await UpdateDeathCertificate(deathCertificates);

                if (deathCertificateAdded != null)
                {
                    return RedirectToAction("Details", new
                    {
                        deathCertificateId = deathCertificateAdded.DeathCertificateID.ToString()
                    });
                }

                if (!await AssignAdmissionAndPatientInformationToView())
                {
                    return RedirectToAction("Index", "Admissions");
                }

                return View(deathCertificates);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        public async Task<IActionResult> Details(string deathCertificateId)
        {
            try
            {
                if (!await AssignAdmissionAndPatientInformationToView())
                {
                    return RedirectToAction("Index", "Admissions");
                }

                var deathCertificate = await GetById(deathCertificateId);
                if (deathCertificate == null)
                {
                    return RedirectToAction("Create");
                }

                return View(deathCertificate);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        #region Helper

        #region Admission

        private async Task UpdateAdmissionDischarge(Guid admissionID)
        {
            var admission = await GetAdmissionById(admissionID.ToString());
            if (admission == null)
            {
                return;
            }

            admission.IsDischarged = true;
            await UpdateAdmission(admission);
        }

        private async Task<AdmissionsDto?> GetAdmissionById(string admissionId)
        {
            if (string.IsNullOrEmpty(admissionId))
            {
                return null;
            }

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

        private async Task UpdateAdmission(AdmissionsDto admission)
        {
            var data = JsonConvert.SerializeObject(admission);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            await client.PutAsync($"{BaseUrl}/Admissions/EditAdmission", httpContent);
        }

        #endregion Admission

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

        #region Death certificate

        private async Task<bool> AssignAdmissionAndPatientInformationToView()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            var patientId = session?.GetCurrentClientId() ?? string.Empty;

            var admission = await GetAdmissionById(admissionId);
            var patient = await GetPatientById(patientId);

            if (admission == null || patient == null)
            {
                return false;
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
            ViewBag.Admission = admission;
            ViewBag.Patient = patient;

            return true;
        }

        private async Task<DeathCertificateDto?> GetById(string deathCertificateId)
        {
            if (string.IsNullOrEmpty(deathCertificateId))
            {
                return null;
            }

            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DeathCertificates/FindDeathCertificateByKey/{deathCertificateId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            var deathCertificates = JsonConvert.DeserializeObject<DeathCertificateDto>(result);
            return deathCertificates;
        }

        private async Task<DeathCertificateDto?> CreateDeathCertificate(DeathCertificateDto deathCertificates)
        {
            AssignDeathCertificateDateTime(deathCertificates);

            var data = JsonConvert.SerializeObject(deathCertificates);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/DeathCertificates/AddDeathCertificates", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DeathCertificateDto>(result);
        }

        private async Task<DeathCertificateDto?> UpdateDeathCertificate(DeathCertificateDto deathCertificates)
        {
            AssignDeathCertificateDateTime(deathCertificates);

            var data = JsonConvert.SerializeObject(deathCertificates);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{BaseUrl}/DeathCertificates/EditDeathCertificates", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DeathCertificateDto>(result);
        }

        private void AssignDeathCertificateDateTime(DeathCertificateDto deathCertificates)
        {
            var deathCertificatesDateTime = GetDeathCertificateDateTime(deathCertificates.DateOfDeath, deathCertificates.TimeOfDeath);
            deathCertificates.DateOfDeath = deathCertificatesDateTime;
            deathCertificates.TimeOfDeath = deathCertificatesDateTime;
        }

        private DateTime GetDeathCertificateDateTime(DateTime dateOfNote, DateTime timeOfNote)
        {
            var date = dateOfNote.Date;
            var time = timeOfNote.TimeOfDay;
            return date + time;
        }

        #endregion Death certificate

        #endregion Helper
    }
}