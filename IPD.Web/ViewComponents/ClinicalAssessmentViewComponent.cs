using IPD.Web.Extensions;
using IPD.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using IPD.Domain.Dto;

namespace IPD.Web.ViewComponents
{
    public class ClinicalAssessmentViewComponent : ViewComponent
    {
        private readonly string BaseUrl;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient httpClient;

        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public ClinicalAssessmentViewComponent(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
            BaseUrl = appSettings.BaseUrl;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();
            var loadAdmissionInformation = loadAdmissionInformations(admissionId);
            var chiefComplainTask = GetLatestChiefComplaintInformation(admissionId);
            var ExaminationTask = FindPatientExaminationByAdmissionID(admissionId);
            var patientDiaDiagnosisTask = GetLatestDiagnosisInformation(admissionId);
            var treatmentPlansTask = GetLatestTreatmentPlanInformation(admissionId);
            var medicationPlanTask = GetLatestMedicationPlanInformation(admissionId);
            var loadDiagnosisCodeInformationTask = LoadDiagnosisCodeInformation();
            await Task.WhenAll(chiefComplainTask, patientDiaDiagnosisTask, ExaminationTask,
                treatmentPlansTask, medicationPlanTask);


            var admissionInformation = await loadAdmissionInformation;
            var chiefComplain = await chiefComplainTask;
            var Examination = await ExaminationTask;
            var patientDiaDiagnosis = await patientDiaDiagnosisTask;
            var treatmentPlans = await treatmentPlansTask;
            var medicationPlan = await medicationPlanTask;
            var diagnosisCodeInformation = await loadDiagnosisCodeInformationTask;



            ViewBag.Admission = admissionInformation;
            ViewBag.LatestChiefComplain = chiefComplain;
            ViewBag.LatestExamination = Examination;
            //var patientExamination = await GetAllExamination();
            //ViewBag.DiagonosisExamimations = patientExamination;
            ViewBag.LatestPatientDiaDiagnosis = patientDiaDiagnosis;
            ViewBag.LatestTreatmentPlans = treatmentPlans;
            ViewBag.LatestMedicationPlan = medicationPlan;
            ViewBag.DiagnosisCode = diagnosisCodeInformation;

            ViewBag.AllDiagnosis = diagnosisCodeInformation;


            return View();
        }

        private async Task<AdmissionsDto?> loadAdmissionInformations(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Admissions/FindAdmissionByKey/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AdmissionsDto>(result);
        }

        private async Task<List<ChiefComplaintsDto>> GetLatestChiefComplaintInformation(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Complaints/LoadComplaints/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var complaintsDtos = JsonConvert.DeserializeObject<List<ChiefComplaintsDto>>(result) ?? new List<ChiefComplaintsDto>();
            var latestComplain = complaintsDtos.OrderByDescending(x => x.DateCreated).ToList();
            return latestComplain;
        }
        private async Task<List<PatientExaminationsDto?>> GetLatestDiagonosisExaminationInformation(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/PatientExaminations/LoadPatientExaminations/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<PatientExaminationsDto?>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<PatientExaminationsDto>>(result) ?? new List<PatientExaminationsDto>();

        }


        private async Task<List<PatientDiagnosisDto>> GetLatestDiagnosisInformation(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/PatientDiagnosis/LoadPatientDiagnosis/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var diagnosisDtos = JsonConvert.DeserializeObject<List<PatientDiagnosisDto>>(result) ?? new List<PatientDiagnosisDto>();
            var latestDiagnosisInformation = diagnosisDtos.OrderByDescending(x => x.DateCreated).ToList();
            return latestDiagnosisInformation;
        }

        private async Task<List<ICDDigonosisCodeDto>> LoadDiagnosisCodeInformation()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/ICDDiagonosisCodes/LoadICDDiagonosisCodes");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ICDDigonosisCodeDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ICDDigonosisCodeDto>>(result) ?? new List<ICDDigonosisCodeDto>();
        }

        private async Task<List<TreatmentPlanDto?>> GetLatestTreatmentPlanInformation(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/TreatmentPlans/LoadTreatmentPlans/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            var treatmentPlans = JsonConvert.DeserializeObject<List<TreatmentPlanDto>>(result) ?? new List<TreatmentPlanDto>();
            var latestTreatmentPlans = treatmentPlans.OrderByDescending(x => x.DateCreated).ToList();
            return latestTreatmentPlans;
        }
        private async Task<List<PrescriptionGetDto?>> GetLatestMedicationPlanInformation(string admissionId)
        {
            var response = await httpClient.GetAsync($"{BaseUrl}/MedicationPlans/GetLatestMedicationPlan/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            var medicationPlan = JsonConvert.DeserializeObject<List<PrescriptionGetDto>>(result) ?? new List<PrescriptionGetDto>();
            return medicationPlan;
        }

        private async Task<List<IPD.Domain.Dto.PatientExaminationsDto>> FindPatientExaminationByAdmissionID(string admissionId)
        {
            var response = await httpClient.GetAsync($"{BaseUrl}/PatientExaminations/FindPatientExaminationByAdmissionID/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var medicationPlanDtos = JsonConvert.DeserializeObject<List<IPD.Domain.Dto.PatientExaminationsDto>>(result) ?? new List<IPD.Domain.Dto.PatientExaminationsDto>();
            var latestMedicationPlanInformation = medicationPlanDtos.OrderByDescending(x => x.DateCreated).ToList();
            return latestMedicationPlanInformation;
        }

        private async Task<List<ChiefComplaintsDto>> GetAllChiefComplaints()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Complaints/LoadComplaints/");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ChiefComplaintsDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<ChiefComplaintsDto>>(result) ?? new List<ChiefComplaintsDto>();
        }

        private async Task<List<AllergiesDto>> GetAllergiesAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Allergies/LoadAllergies");

            if (!response.IsSuccessStatusCode)
            {
                return new List<AllergiesDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            var ncds = JsonConvert.DeserializeObject<List<AllergiesDto>>(result);
            return ncds ?? new List<AllergiesDto>();
        }
    }
}