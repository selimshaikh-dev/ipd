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
    [ModuleAuthorized(UserAccessModule.PatientExaminations)]
    public class PatientExaminationsController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient _httpClient;
        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public PatientExaminationsController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            _httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var Examination = await LoadPatientDiagnosisAsync();
            var diagnosisExamination = await LoadDiagnosisCodesAsync();
            ViewBag.DiagonosisExamimations = diagnosisExamination;
            return View(Examination);
        }

        private async Task<List<DiagonosisExaminationDto>> LoadDiagnosisCodesAsync()
        {
            var response = await _httpClient.GetAsync("DiagonosisExamimations/LoadDiagonosisExaminations");
            if (!response.IsSuccessStatusCode)
            {
                return new List<DiagonosisExaminationDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DiagonosisExaminationDto>>(result) ?? new List<DiagonosisExaminationDto>();
        }

        private async Task<List<PatientExaminationsDto>> LoadPatientDiagnosisAsync()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            var response = await _httpClient.GetAsync($"PatientExaminations/LoadPatientExaminations/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<PatientExaminationsDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<PatientExaminationsDto>>(result) ?? new List<PatientExaminationsDto>();
        }
        #endregion Index

        #region Details
        public async Task<IActionResult> Details(string examinationsId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            if (!await AndDiagonosisExaminationsInformation())
            {
                return RedirectToAction("Index", "Admissions");
            }

            var patientExamination = await GetById(examinationsId);
            var diagonosisExaminations = await LoadDiagnosisExaminationsAsync();
            ViewBag.DiagonosisExamimations = diagonosisExaminations;
            if (patientExamination == null)
            {
                return RedirectToAction("Index");
            }

            return View(patientExamination);
        }

        #endregion Details

        #region Create

        public async Task<IActionResult> Create()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            if (!await AndDiagonosisExaminationsInformation())
            {
                return RedirectToAction("Index", "Admissions");
            }

            var examinations = new PatientExaminationsDto { };
            return View(examinations);
        }

        private async Task<bool> AndDiagonosisExaminationsInformation()
        {
            var patientExamination = await GetAllExamination();
            ViewBag.DiagonosisExamimations = patientExamination;

            return true;
        }

        private async Task<List<DiagonosisExaminationDto>> GetAllExamination()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DiagonosisExamimations/LoadDiagonosisExaminations/");
            if (!response.IsSuccessStatusCode)
            {
                return new List<DiagonosisExaminationDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<DiagonosisExaminationDto>>(result) ?? new List<DiagonosisExaminationDto>();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientExaminationsDto patientExamination)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var examinationAdded = await CreatePatientExamination(patientExamination);
            if (examinationAdded != null)
            {
                return RedirectToAction("Details", new
                {
                    examinationsId = examinationAdded.PatientExaminationID.ToString()
                });
            }

            if (!await AndDiagonosisExaminationsInformation())
            {
                return RedirectToAction("Index", "Admissions");
            }

            return View(patientExamination);
        }

        private async Task<PatientExaminationsDto?> CreatePatientExamination(PatientExaminationsDto patientExamination)
        {
            patientExamination.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? string.Empty);
            AssignExaminationDetail(patientExamination);
            var data = JsonConvert.SerializeObject(patientExamination);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{BaseUrl}/PatientExaminations/AddPatientExaminations", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientExaminationsDto>(result);
        }

        private void AssignExaminationDetail(PatientExaminationsDto patientExaminationDto)
        {
            if (patientExaminationDto.DigonosisExaminationIDs != null && patientExaminationDto.DigonosisExaminationIDs.Any())
            {
                patientExaminationDto.ExaminationDetails = patientExaminationDto.DigonosisExaminationIDs
                    .Select(id => new ExaminationDetailsDto
                    {
                        DigonosisExaminationID = id
                    })
                    .ToList();
            }
        }

        #endregion Create

        #region Edit

        public async Task<IActionResult> Edit(string PatientExaminationID)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            if (!await AndDiagonosisExaminationsInformation())
            {
                return RedirectToAction("Index", "Admissions");
            }

            var patientExamination = await FindPatientExaminationAsync(PatientExaminationID);
            if (patientExamination == null)
            {
                return RedirectToAction("Index");
            }
            await AssignExaminationToView();
            return View(patientExamination);
        }

        private async Task AssignExaminationToView()
        {
            var Examination = await GetAllExaminationAsync();
            ViewBag.DiagonosisExamimations = Examination;
        }

        private async Task<List<DiagonosisExaminationDto>> GetAllExaminationAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DiagonosisExamimations/LoadDiagonosisExaminations");

            if (!response.IsSuccessStatusCode)
            {
                return new List<DiagonosisExaminationDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            var examination = JsonConvert.DeserializeObject<List<DiagonosisExaminationDto>>(result);

            return examination ?? new List<DiagonosisExaminationDto>();
        }

        private async Task<PatientExaminationsDto?> FindPatientExaminationAsync(string patientExaminationId)
        {
            var response = await _httpClient.GetAsync($"PatientExaminations/FindPatientExaminationByKey/{patientExaminationId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientExaminationsDto>(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientExaminationsDto examinationDto)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var patientExamination = await UpdatePatientExamination(examinationDto);
            if (patientExamination != null)
            {
                return RedirectToAction("Details", new
                {
                    examinationsId = patientExamination.PatientExaminationID.ToString()
                });
            }

            if (!await AndDiagonosisExaminationsInformation())
            {
                return RedirectToAction("Index", "Admissions");
            }

            return View(examinationDto);
        }

        #endregion Edit

        #region Patient

        private async Task<PatientEditDto?> GetPatientById(string patientId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Patients/GetClientById?patientId={patientId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PatientEditDto>(result);
        }

        #endregion Patient

        #region Helper

        private async Task<List<PatientExaminationsDto>> GetByAdmissionId(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/PatientExaminations/LoadPatientExaminations/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<PatientExaminationsDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<PatientExaminationsDto>>(result) ?? new List<PatientExaminationsDto>();
        }

        private async Task<PatientExaminationsDto?> GetById(string examinationId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/PatientExaminations/FindPatientExaminationByKey/{examinationId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PatientExaminationsDto>(result);
        }

        private async Task<PatientExaminationsDto?> UpdatePatientExamination(PatientExaminationsDto patientExaminationDTO)
        {
            AssignExaminationDetail(patientExaminationDTO);
            var data = JsonConvert.SerializeObject(patientExaminationDTO);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/PatientExaminations/EditPatientExaminations", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientExaminationsDto>(result);
        }

        #endregion Helper

        private async Task<List<DiagonosisExaminationDto>> LoadDiagnosisExaminationsAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DiagonosisExamimations/LoadDiagonosisExaminations");
            if (!response.IsSuccessStatusCode)
            {
                return new List<DiagonosisExaminationDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DiagonosisExaminationDto>>(result) ?? new List<DiagonosisExaminationDto>();
        }
    }
}