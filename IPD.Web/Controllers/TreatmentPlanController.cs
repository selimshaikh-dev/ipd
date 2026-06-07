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
    [ModuleAuthorized(UserAccessModule.TreatmentPlan)]
    public class TreatmentPlanController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public TreatmentPlanController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var treatmentPlans = await GetByAdmissionId(admissionId);
            return View(treatmentPlans);
        }

        private async Task<List<TreatmentPlanDto>> GetByAdmissionId(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/TreatmentPlans/LoadTreatmentPlans/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<TreatmentPlanDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<TreatmentPlanDto>>(result) ?? new List<TreatmentPlanDto>();
        }

        #endregion Index

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var latestChiefComplainTask = GetLatestChiefComplaintAsync();
            var latestPatientDiagnosisTask = GetLatestPatientDiagnosisAsync();
            var latestTreatmentPlanTask = GetLatestTreatmentPlansAsync();
            var latestExaminationTask = GetLatestExaminationAsync();
            var latestMedicationtask = GetLatestMedicationPlan();
            await Task.WhenAll(latestChiefComplainTask, latestPatientDiagnosisTask,
                latestTreatmentPlanTask, latestExaminationTask, latestMedicationtask);
            var latestChiefComplain = await latestChiefComplainTask;
            var latestPatientDiagnosis = await latestPatientDiagnosisTask;
            var latestTreatmentPlan = await latestTreatmentPlanTask;
            var latestExamination = await latestExaminationTask;
            var latestMedication = await latestMedicationtask;
            ViewBag.ChiefComplain = latestChiefComplain;
            ViewBag.PatientDiagnosis = latestPatientDiagnosis;
            ViewBag.TreatmentPlan = latestTreatmentPlan;
            ViewBag.Examination = latestExamination;
            ViewBag.Medication = latestMedication;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TreatmentPlanDto treatmentPlansDto)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var treatementplanAdd = await CreateTreatementPlan(treatmentPlansDto);
            if (treatementplanAdd != null)
            {
                return RedirectToAction("Details", new
                {
                    TreatmentPlanID = treatementplanAdd.TreatmentPlanID.ToString()
                });
            }
            return View(treatementplanAdd);
        }

        private async Task<TreatmentPlanDto?> CreateTreatementPlan(TreatmentPlanDto treatmentPlansDto)
        {
            treatmentPlansDto.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? string.Empty);

            var data = JsonConvert.SerializeObject(treatmentPlansDto);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/TreatmentPlans/AddTreatmentPlans", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TreatmentPlanDto>(result);
        }

        #endregion Create

        #region Edit
        public async Task<IActionResult> Edit(string treatmentPlanID)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var treatementPlan = await GetById(treatmentPlanID);
            if (treatementPlan == null)
            {
                return RedirectToAction("Index");
            }
            return View(treatementPlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TreatmentPlanDto treatmentPlans)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var treatmentPlan = await updatetraetementPlan(treatmentPlans);
            if (treatmentPlan != null)
            {
                return RedirectToAction("Details", new
                {
                    TreatmentPlanID = treatmentPlan.TreatmentPlanID.ToString()
                });
            }

            return View(treatmentPlan);
        }

        private async Task<TreatmentPlanDto?> updatetraetementPlan(TreatmentPlanDto treatmentPlans)
        {
            var data = JsonConvert.SerializeObject(treatmentPlans);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/TreatmentPlans/EdittreatmentPlans", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TreatmentPlanDto>(result);
        }

        #endregion Edit

        #region Details
        public async Task<IActionResult> Details(string TreatmentPlanID)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var patientMedication = await GetById(TreatmentPlanID);

            if (patientMedication == null)
            {
                return RedirectToAction("Index");
            }
            return View(patientMedication);
        }

        private async Task<TreatmentPlanDto?> GetById(string TreatmentPlanID)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/TreatmentPlans/FindTreatmentPlanByKey/{TreatmentPlanID}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TreatmentPlanDto>(result);
        }

        #endregion Details

        private async Task<ChiefComplaintsDto?> GetLatestChiefComplaintAsync()
        {
            string admissionId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();

            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Complaints/LoadComplaints/{admissionId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var complaints = JsonConvert.DeserializeObject<List<ChiefComplaintsDto>>(result);
            if (complaints == null || complaints.Count == 0)
            {
                return null;
            }

            return complaints.OrderByDescending(x => x.DateCreated).FirstOrDefault();
        }

        private async Task<PatientDiagnosisDto?> GetLatestPatientDiagnosisAsync()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/PatientDiagnosis/LoadPatientDiagnosis/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var patientDiagnosis = JsonConvert.DeserializeObject<List<PatientDiagnosisDto>>(result);
            if (patientDiagnosis == null || patientDiagnosis.Count == 0)
            {
                return null;
            }

            return patientDiagnosis.OrderByDescending(x => x.DateCreated).FirstOrDefault();
        }

        private async Task<TreatmentPlanDto?> GetLatestTreatmentPlansAsync()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/TreatmentPlans/LoadTreatmentPlans/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var patientDiagnosis = JsonConvert.DeserializeObject<List<TreatmentPlanDto>>(result);
            if (patientDiagnosis == null || patientDiagnosis.Count == 0)
            {
                return null;
            }

            return patientDiagnosis.OrderByDescending(x => x.DateCreated).FirstOrDefault();
        }

        private async Task<PatientExaminationsDto?> GetLatestExaminationAsync()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/PatientExaminations/LoadPatientExaminations/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var patientDiagnosis = JsonConvert.DeserializeObject<List<PatientExaminationsDto>>(result);
            if (patientDiagnosis == null || patientDiagnosis.Count == 0)
            {
                return null;
            }

            return patientDiagnosis.OrderByDescending(x => x.DateCreated).FirstOrDefault();
        }

        private async Task<MedicationPlanDto?> GetLatestMedicationPlan()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/MedicationPlans/LoadMedicationPlans/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var patientDiagnosis = JsonConvert.DeserializeObject<List<MedicationPlanDto>>(result);
            if (patientDiagnosis == null || patientDiagnosis.Count == 0)
            {
                return null;
            }

            return patientDiagnosis.OrderByDescending(x => x.DateCreated).FirstOrDefault();
        }
    }
}