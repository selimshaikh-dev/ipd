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
    [ModuleAuthorized(UserAccessModule.InterReferrals)]
    public class InterReferralsController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public InterReferralsController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        public async Task<IActionResult> Index()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            if (!await AddPatientAndDepartmentInformation())
            {
                return RedirectToAction("Index", "Admissions");
            }

            var interReferrals = await GetByAdmissionId(admissionId);

            return View(interReferrals);
        }

        #region Create

        public async Task<IActionResult> Create()
        {
            if (!await AddPatientAndDepartmentInformation())
            {
                return RedirectToAction("Index", "Admissions");
            }

            var interReferral = new InterDepartmentReferralDto
            {
                Date = DateTime.Now,
                Time = DateTime.Now
            };
            return View(interReferral);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InterDepartmentReferralDto interReferralDto)
        {
            var interReferralAdded = await CreateInterDepartmentReferral(interReferralDto);
            if (interReferralAdded != null)
            {
                return RedirectToAction("Details", new
                {
                    interDepartmentReferralsId = interReferralAdded.InterDepartmentReferralsID.ToString()
                });
            }

            if (!await AddPatientAndDepartmentInformation())
            {
                return RedirectToAction("Index", "Admissions");
            }

            return View(interReferralDto);
        }

        #endregion Create

        #region Edit

        public async Task<IActionResult> Edit(string interDepartmentReferralsId)
        {
            if (!await AddPatientAndDepartmentInformation())
            {
                return RedirectToAction("Index", "Admissions");
            }

            var interReferral = await GetById(interDepartmentReferralsId);
            if (interReferral == null)
            {
                return RedirectToAction("Index");
            }

            return View(interReferral);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InterDepartmentReferralDto interReferralDto)
        {
            var interReferralAdded = await UpdateInterDepartmentReferral(interReferralDto);
            if (interReferralAdded != null)
            {
                return RedirectToAction("Details", new
                {
                    interDepartmentReferralsId = interReferralAdded.InterDepartmentReferralsID.ToString()
                });
            }

            if (!await AddPatientAndDepartmentInformation())
            {
                return RedirectToAction("Index", "Admissions");
            }

            return View(interReferralDto);
        }

        #endregion Edit

        public async Task<IActionResult> Details(string interDepartmentReferralsId)
        {
            if (!await AddPatientAndDepartmentInformation())
            {
                return RedirectToAction("Index", "Admissions");
            }

            var interReferral = await GetById(interDepartmentReferralsId);
            if (interReferral == null)
            {
                return RedirectToAction("Index");
            }

            return View(interReferral);
        }

        #region Helper

        private async Task<bool> AddPatientAndDepartmentInformation()
        {
            var patientId = session?.GetCurrentClientId() ?? string.Empty;
            var patient = await GetPatientById(patientId);

            if (patient == null)
            {
                return false;
            }

            ViewBag.ClientId = patientId;
            var clientName = patient.FirstName;
            if (!string.IsNullOrEmpty(patient.MiddleName))
            {
                clientName += " " + patient.MiddleName;
            }
            clientName += " " + patient.LastName;
            ViewBag.ClientName = clientName;
            ViewBag.Patient = patient;

            var departments = await GetAllDepartments();
            ViewBag.Departments = departments;
            return true;
        }

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

        private async Task<List<DepartmentsDto>> GetAllDepartments()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Departments/LoadDepartments/");
            if (!response.IsSuccessStatusCode)
            {
                return new List<DepartmentsDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<DepartmentsDto>>(result) ?? new List<DepartmentsDto>();
        }

        private async Task<List<InterDepartmentReferralDto>> GetByAdmissionId(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/InterReferrals/LoadReferrals/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<InterDepartmentReferralDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<InterDepartmentReferralDto>>(result) ?? new List<InterDepartmentReferralDto>();
        }

        private async Task<InterDepartmentReferralDto?> GetById(string interDepartmentReferralsId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/InterReferrals/FindReferralByKey/{interDepartmentReferralsId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<InterDepartmentReferralDto>(result);
        }

        private async Task<InterDepartmentReferralDto?> CreateInterDepartmentReferral(InterDepartmentReferralDto interReferralsDto)
        {
            AssignInterDepartmentReferralDtoDateTime(interReferralsDto);
            interReferralsDto.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? string.Empty);

            var data = JsonConvert.SerializeObject(interReferralsDto);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/InterReferrals/AddReferrals", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<InterDepartmentReferralDto>(result);
        }

        private async Task<InterDepartmentReferralDto?> UpdateInterDepartmentReferral(InterDepartmentReferralDto interReferralDto)
        {
            AssignInterDepartmentReferralDtoDateTime(interReferralDto);

            var data = JsonConvert.SerializeObject(interReferralDto);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/InterReferrals/EditReferrals", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<InterDepartmentReferralDto>(result);
        }

        private void AssignInterDepartmentReferralDtoDateTime(InterDepartmentReferralDto interReferralDto)
        {
            var interReferralDtoDateTime = GetInterDepartmentReferralDateTime(interReferralDto.Date, interReferralDto.Time);
            interReferralDto.Date = interReferralDtoDateTime;
            interReferralDto.Time = interReferralDtoDateTime;
        }

        private DateTime GetInterDepartmentReferralDateTime(DateTime dateOfReferral, DateTime timeOfReferral)
        {
            var date = dateOfReferral.Date;
            var time = timeOfReferral.TimeOfDay;
            return date + time;
        }

        #endregion Helper
    }
}