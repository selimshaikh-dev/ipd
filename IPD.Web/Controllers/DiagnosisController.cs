using IPD.Domain.Dto;
using IPD.Web.Extensions;
using IPD.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Web.Controllers
{
    [ModuleAuthorized(UserAccessModule.Diagnosis)]
    public class DiagnosisController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient _httpClient;
        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public DiagnosisController(
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            _httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var diagnosis = await LoadPatientDiagnosisAsync();
            var diagnosisCodes = await LoadDiagnosisCodesAsync();
            ViewBag.DiagnosisCode = diagnosisCodes;

            return View(diagnosis);
        }

        public Task<IActionResult> Create()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            return Task.FromResult<IActionResult>(View(new PatientDiagnosisDto
            {
                AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString()),
            }));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientDiagnosisDto patientDiagnosisDto)
        {
            var diagnosis = await AddPatientDiagnosisAsync(patientDiagnosisDto);
            if (diagnosis != null)
            {
                return RedirectToAction("Details", new { patientDiagnosisId = diagnosis.PatientDiagnosisID });
            }

            return View(patientDiagnosisDto);
        }

        public async Task<IActionResult> Edit(string patientDiagnosisId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var diagnosis = await FindPatientDiagnosisAsync(patientDiagnosisId);
            if (diagnosis == null)
            {
                return RedirectToAction("Index");
            }

            return View(diagnosis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientDiagnosisDto patientDiagnosisDto)
        {
            var diagnosis = await EditPatientDiagnosisAsync(patientDiagnosisDto);
            if (diagnosis != null)
            {
                return RedirectToAction("Details", new
                {
                    patientDiagnosisId = diagnosis.PatientDiagnosisID
                });
            }

            return View(patientDiagnosisDto);
        }

        public async Task<IActionResult> Details(string patientDiagnosisId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var diagnosis = await FindPatientDiagnosisAsync(patientDiagnosisId);
            if (diagnosis == null)
            {
                return RedirectToAction("Index");
            }

            var diagnosisCodes = await LoadDiagnosisCodesAsync();
            ViewBag.DiagnosisCode = diagnosisCodes;

            return View(diagnosis);
        }

        public IActionResult ClinicalAssessment()
        {
            return View();
        }

        public async Task<JsonResult> LoadDiagnosisCodesTree()
        {
            try
            {
                var tree = await LoadDiagnosisCodesWithTreeAsync();
                return Json(tree);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, StatusCode(500));
            }
        }

        #region Helper

        #region Diagnosis

        private async Task<PatientDiagnosisDto?> EditPatientDiagnosisAsync(PatientDiagnosisDto patientDiagnosis)
        {
            AssignDiagnosisDetail(patientDiagnosis);
            var data = JsonConvert.SerializeObject(patientDiagnosis);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync("PatientDiagnosis/EditPatientDiagnosis", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientDiagnosisDto>(result);
        }

        private async Task<PatientDiagnosisDto?> AddPatientDiagnosisAsync(PatientDiagnosisDto patientDiagnosis)
        {
            AssignDiagnosisDetail(patientDiagnosis);
            var data = JsonConvert.SerializeObject(patientDiagnosis);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("PatientDiagnosis/AddPatientDiagnosis", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientDiagnosisDto>(result);
        }

        private async Task<PatientDiagnosisDto?> FindPatientDiagnosisAsync(string patientDiagnosisId)
        {
            var response = await _httpClient.GetAsync($"PatientDiagnosis/FindPatientDiagnosisByKey/{patientDiagnosisId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientDiagnosisDto>(result);
        }

        private async Task<List<PatientDiagnosisDto>> LoadPatientDiagnosisAsync()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            var response = await _httpClient.GetAsync($"PatientDiagnosis/LoadPatientDiagnosis/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<PatientDiagnosisDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<PatientDiagnosisDto>>(result) ?? new List<PatientDiagnosisDto>();
        }

        private void AssignDiagnosisDetail(PatientDiagnosisDto patientDiagnosisDto)
        {
            if (patientDiagnosisDto.PatientsDiseaseIds != null && patientDiagnosisDto.PatientsDiseaseIds.Any())
            {
                patientDiagnosisDto.DiagonosisDetails = patientDiagnosisDto.PatientsDiseaseIds
                    .Select(id => new DiagonosisDetailsDto
                    {
                        DiseaseID = id
                    })
                    .ToList();
            }
        }

        #endregion Diagnosis

        #region DiagonosisCode

        private async Task<List<ICDsDigonosisCodeDto>> LoadDiagnosisCodesWithTreeAsync()
        {
            var icdDiagnosisCodes = await LoadDiagnosisCodesAsync();
            return ICDsDigonosisCodeDto.BuildTree(icdDiagnosisCodes);
        }

        private async Task<List<ICDDigonosisCodeDto>> LoadDiagnosisCodesAsync()
        {
            var response = await _httpClient.GetAsync("ICDDiagonosisCodes/LoadICDDiagonosisCodes");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ICDDigonosisCodeDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ICDDigonosisCodeDto>>(result) ?? new List<ICDDigonosisCodeDto>();
        }

        #endregion DiagonosisCode

        #endregion Helper
    }
}