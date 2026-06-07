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
    [ModuleAuthorized(UserAccessModule.MedicationPlan)]
    public class MedicationPlanController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;
        private readonly ILogger<MedicationPlanController> logger;

        public MedicationPlanController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory, ILogger<MedicationPlanController> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
            BaseUrl = appSettings.BaseUrl;
            httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var medicationPlans = await GetByAdmissionId(admissionId);

            return View(medicationPlans);
        }

        private async Task<List<PrescriptionDetailsDto>> GetByAdmissionId(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Prescriptions/LoadPrescriptionsByAddmissionId/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<PrescriptionDetailsDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<PrescriptionDetailsDto>>(result) ?? new List<PrescriptionDetailsDto>();
        }

        #endregion Index

        #region Create

        public async Task<IActionResult> Create()
        {
            try
            {
                var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
                if (!await GetAdmissionById(admissionId))
                {
                    return RedirectToAction("Index");
                }

                var medicalList = await GetAllMedication();
                var intervalList = await GetAllInterval();
                var directionList = await GetAllDirection();
                ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

                var latestChiefComplainTask = GetLatestChiefComplaintAsync();
                var latestPatientDiagnosisTask = GetLatestPatientDiagnosisAsync();
                var latestTreatmentPlanTask = GetLatestTreatmentPlansAsync();
                var latestExaminationTask = GetLatestExaminationAsync();

                await Task.WhenAll(latestChiefComplainTask, latestPatientDiagnosisTask, latestTreatmentPlanTask, latestExaminationTask);
                var latestChiefComplain = await latestChiefComplainTask;
                var latestPatientDiagnosis = await latestPatientDiagnosisTask;
                var latestTreatmentPlan = await latestTreatmentPlanTask;
                var latestExamination = await latestExaminationTask;

                ViewBag.ChiefComplain = latestChiefComplain;
                ViewBag.PatientDiagnosis = latestPatientDiagnosis;
                ViewBag.TreatmentPlan = latestTreatmentPlan;
                ViewBag.Examination = latestExamination;

                MedicationsPlanDto model = new MedicationsPlanDto
                {
                    intervalDTOs = intervalList,
                    medicationDtos = medicalList,
                    directionDTOs = directionList,
                };

                return View(model);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        private async Task<List<MedicationDto>> GetAllMedication()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Medications/LoadMedications/");
            if (!response.IsSuccessStatusCode)
            {
                return new List<MedicationDto>();
            }
            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<MedicationDto>>(result) ?? new List<MedicationDto>();
        }

        private async Task<List<IntervalDto>> GetAllInterval()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Interval/LoadIntervals/");
            if (!response.IsSuccessStatusCode)
            {
                return new List<IntervalDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<IntervalDto>>(result) ?? new List<IntervalDto>();
        }

        private async Task<List<DirectionDto>> GetAllDirection()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Directions/LoadDirections/");
            if (!response.IsSuccessStatusCode)
            {
                return new List<DirectionDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<DirectionDto>>(result) ?? new List<DirectionDto>();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicationsPlanDto patientsMedication)
        {
            try
            {
                ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
                var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
                if (!await GetAdmissionById(admissionId))
                {
                    return RedirectToAction("Index");
                }

                patientsMedication.AdmissionID = Guid.Parse(admissionId);

                var medicationAdd = await CreatePatientMedication(patientsMedication);
                if (medicationAdd != null)
                {
                    return RedirectToAction("Details", new
                    {
                        prescriptionId = medicationAdd.PrescriptionsID.ToString()
                    });
                }
                return View(patientsMedication);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

           
        }

        private async Task<PrescriptionCreateDTO?> CreatePatientMedication(MedicationsPlanDto medicationPlanDTO)
        {
            medicationPlanDTO.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? string.Empty);
            var medicatonList = new List<PrescriptionDTO>();

            if (medicationPlanDTO.MedicationsID != null)
            {
                for (int i = 0; i < medicationPlanDTO.MedicationsID.Length; i++)
                {
                    var prescription = new PrescriptionDTO()
                    {
                        Dose = medicationPlanDTO.Dose[i],
                        Durations = medicationPlanDTO.Durations[i],
                        MedicationsID = medicationPlanDTO.MedicationsID[i],
                        IntervalsID = medicationPlanDTO.IntervalsID[i],
                        DirectionsID = medicationPlanDTO.DirectionsID[i],
                        PrescriptionsID = medicationPlanDTO.PrescriptionsID,
                        AdmissionID = medicationPlanDTO.AdmissionID,
                        DateCreated = medicationPlanDTO.DateCreated,
                        Medications = medicationPlanDTO.Medications,
                        Directions = medicationPlanDTO.Directions,
                        Intervals = medicationPlanDTO.Intervals
                    };
                    medicatonList.Add(prescription);
                }
            }
            var prescriptionData = new PrescriptionCreateDTO()
            {
                PrescriptionsID = medicationPlanDTO.PrescriptionsID,
                PrescriptionsDate = medicationPlanDTO.DateCreated ?? DateTime.Now,
                DoctorName = medicationPlanDTO.DoctorName,
                AdmissionID = medicationPlanDTO.AdmissionID,
                MedicationPlans = medicatonList
            };

            var data = JsonConvert.SerializeObject(prescriptionData);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/Prescriptions/AddPrescriptions", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PrescriptionCreateDTO>(result);
        }

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

        #endregion Create

        #region Edit

        public async Task<IActionResult> Edit(string prescriptionId)
        {
            try
            {
                ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
                var medicalList = await GetAllMedication();
                var intervalList = await GetAllInterval();
                var directionList = await GetAllDirection();
                ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

                MedicationsPlanDto medicationPlan = await GetById(prescriptionId) ?? new MedicationsPlanDto();

                medicationPlan.intervalDTOs = intervalList;
                medicationPlan.medicationDtos = medicalList;
                medicationPlan.directionDTOs = directionList;

                if (medicationPlan == null)
                {
                    return RedirectToAction("Index");
                }

                return View(medicationPlan);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MedicationsPlanDto medicationPlan)
        {
            try
            {
                ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

                var medicationPlans = await updatemedication(medicationPlan);
                if (medicationPlans != null)
                {
                    return RedirectToAction("Details", new
                    {
                        prescriptionId = medicationPlans.PrescriptionsID.ToString()
                    });
                }

                return View(medicationPlan);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

           
        }

        private async Task<PrescriptionCreateDTO?> updatemedication(MedicationsPlanDto medicationPlanDTO)
        {
            var medicatonList = new List<PrescriptionDTO>();

            if (medicationPlanDTO.MedicationsID != null)
            {
                for (int i = 0; i < medicationPlanDTO.MedicationsID.Length; i++)
                {
                    var prescription = new PrescriptionDTO()
                    {
                        Dose = medicationPlanDTO.Dose[i],
                        Durations = medicationPlanDTO.Durations[i],
                        MedicationsID = medicationPlanDTO.MedicationsID[i],
                        IntervalsID = medicationPlanDTO.IntervalsID[i],
                        DirectionsID = medicationPlanDTO.DirectionsID[i],
                        MedicationPlanID = medicationPlanDTO.MedicationPlanID[i],
                        PrescriptionsID = medicationPlanDTO.PrescriptionsID,
                        AdmissionID = medicationPlanDTO.AdmissionID,
                        DateCreated = medicationPlanDTO.DateCreated,
                        Medications = medicationPlanDTO.Medications,
                        Directions = medicationPlanDTO.Directions,
                        Intervals = medicationPlanDTO.Intervals
                    };
                    medicatonList.Add(prescription);
                }
            }
            var prescriptionData = new PrescriptionCreateDTO()
            {
                PrescriptionsID = medicationPlanDTO.PrescriptionsID,
                PrescriptionsDate = medicationPlanDTO.DateCreated ?? DateTime.Now,
                DoctorName = medicationPlanDTO.DoctorName,
                AdmissionID = medicationPlanDTO.AdmissionID,
                MedicationPlans = medicatonList
            };

            var data = JsonConvert.SerializeObject(prescriptionData);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/Prescriptions/EditPrescriptions", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PrescriptionCreateDTO>(result);
        }

        #endregion Edit

        #region Admission

        private async Task<bool> GetAdmissionById(string admissionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Admissions/FindAdmissionByKey/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            string result = await response.Content.ReadAsStringAsync();

            var admission = JsonConvert.DeserializeObject<AdmissionsDto>(result);
            ViewBag.Admission = admission;
            return true;
        }

        #endregion Admission

        #region Details
        public async Task<IActionResult> Details(string prescriptionId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var patientMedication = await GetPrescriptionById(prescriptionId);

            if (patientMedication == null)
            {
                return RedirectToAction("Index");
            }
            return View(patientMedication);
        }

        private async Task<MedicationsPlanDto?> GetById(string prescriptionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Prescriptions/LoadPrescriptions/{prescriptionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            var prescriptionDetails = JsonConvert.DeserializeObject<PrescriptionDetailsDto>(result) ?? new PrescriptionDetailsDto();
            var intervalList = new List<Guid>();
            var medicationPlanIdList = new List<Guid>();
            var medicationList = new List<Guid>();
            var directionList = new List<Guid>();
            var doseList = new List<string>();
            var durationList = new List<string>();

            foreach (var item in prescriptionDetails.MedicationPlans)
            {
                intervalList.Add(item.IntervalsID);
                medicationList.Add(item.MedicationsID);
                directionList.Add(item.DirectionsID);
                durationList.Add(item.Durations);
                doseList.Add(item.Dose);
                medicationPlanIdList.Add(item.MedicationPlanID);
            }

            var medicationDto = new MedicationsPlanDto()
            {
                PrescriptionsID = prescriptionDetails.PrescriptionsID,
                DoctorName = prescriptionDetails.DoctorName,
                AdmissionID = prescriptionDetails.AdmissionID,
                DateCreated = prescriptionDetails.PrescriptionsDate,
                IntervalsID = intervalList.ToArray(),
                MedicationsID = medicationList.ToArray(),
                DirectionsID = directionList.ToArray(),
                Dose = doseList.ToArray(),
                Durations = durationList.ToArray(),
                MedicationPlanID = medicationPlanIdList.ToArray(),
            };

            return medicationDto;
        }

        private async Task<PrescriptionDetailsDto?> GetPrescriptionById(string prescriptionId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Prescriptions/LoadPrescriptions/{prescriptionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<PrescriptionDetailsDto>(result);
        }
        #endregion Details
    }
}