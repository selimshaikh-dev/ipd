using IPD.Domain.Dto;
using IPD.Web.Extensions;
using IPD.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IPD.Web.ViewComponents
{
    public class DailyObservationViewComponent : ViewComponent
    {
        private readonly string BaseUrl;
        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public DailyObservationViewComponent(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();
            var latestDoctorNoteTask = GetLatestDoctorNoteInformation(admissionId);
            var latestVitalTask = GetLatestVitalInformation(admissionId);
            var latestDiabetesMonitoringTask = GetLatestDiabetesMonitoringInformation(admissionId);
            var loadAdmissionInformation = loadAdmissionInformations(admissionId);

            await Task.WhenAll(latestDoctorNoteTask, latestVitalTask, latestDiabetesMonitoringTask);

            var latestDoctorNote = await latestDoctorNoteTask;
            var latestVital = await latestVitalTask;
            var latestDiabetesMonitoring = await latestDiabetesMonitoringTask;
            var admissionInformation = await loadAdmissionInformation;

            ViewBag.LatestDoctorNote = latestDoctorNote;
            ViewBag.LatestVital = latestVital;
            ViewBag.LatestDiabetesMonitoring = latestDiabetesMonitoring;
            ViewBag.Admission = admissionInformation;

            return View();
        }

        private async Task<List<DoctorNotesDto>> GetLatestDoctorNoteInformation(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DoctorsNotes/LoadDoctorsNotes/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var doctorsNotes = JsonConvert.DeserializeObject<List<DoctorNotesDto>>(result) ?? new List<DoctorNotesDto>();
            var latestDoctorNote = doctorsNotes.OrderByDescending(x => x.DateOfNote).ToList();
            return latestDoctorNote;
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

        private async Task<List<VitalDto?>> GetLatestVitalInformation(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Vitals/LoadVitals/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var vitals = JsonConvert.DeserializeObject<List<VitalDto>>(result) ?? new List<VitalDto>();
            var latestVital = vitals.OrderByDescending(x => x.DateCollected).ToList();
          
            return latestVital;
        }

        private async Task<List<DiabeticsProfileDto>> GetLatestDiabetesMonitoringInformation(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DiabeticProfiles/LoadDiabeticProfiles/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            var diabeticProfileDtos = JsonConvert.DeserializeObject<List<DiabeticsProfileDto>>(result) ?? new List<DiabeticsProfileDto>();
            var latestDiabetesMonitoringInformation = diabeticProfileDtos.OrderByDescending(x => x.DateCollected).ToList();
            return latestDiabetesMonitoringInformation;
        }
    }
}