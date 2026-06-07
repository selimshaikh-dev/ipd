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
    [ModuleAuthorized(UserAccessModule.Discharge)]
    public class DischargeController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public DischargeController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        public IActionResult Index()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            return View();
        }

        #region DischargeNoteList

        public async Task<IActionResult> DischargeNoteList()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var discharges = await GetDischargesByCurrentAdmission();
            var dischargeStatuses = await GetDischargeStatuesAsync();

            var viewModel = new Domain.Dto.DischargeNoteListViewModels
            {
                DischargeNotes = discharges,
                DischargeStatuses = dischargeStatuses
            };

            return View(viewModel);
        }

        #endregion DischargeNoteList

        #region Create

        public async Task<IActionResult> Create()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var dischargeStatuses = await GetDischargeStatuesAsync();
            var viewModel = new Domain.Dto.DischargeNoteViewModels
            {
                DischargeStatuses = dischargeStatuses,
                Create = new DischargesDto
                {
                    DischargeDate = DateTime.Now,
                    DischargeTime = DateTime.Now,
                }
            };

            var vital = await GetLatestVitalInformation();
            var doctorsNote = await GetLatestDoctorNoteInformation();
            ViewBag.LatestVital = vital;
            ViewBag.LatestDoctorNote = doctorsNote;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Domain.Dto.DischargeNoteViewModels dischargeNoteViewModel)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var dischargeNote = dischargeNoteViewModel.Create;
            var discharge = await AddDischargeNote(dischargeNote);
            if (discharge == null)
            {
                var dischargeStatuses = await GetDischargeStatuesAsync();
                var viewModel = new Domain.Dto.DischargeNoteViewModels
                {
                    Create = dischargeNoteViewModel.Create,
                    DischargeStatuses = dischargeStatuses
                };

                return View(viewModel);
            }

            await UpdateAdmissionDischarge(discharge.AdmissionID);

            return RedirectToAction("Details", new
            {
                dischargeId = discharge.DischargeID
            });
        }

        #endregion Create

        #region EditDisharge

        public async Task<IActionResult> Edit(string dischargeId)
        {
            if (string.IsNullOrEmpty(dischargeId))
            {
                return RedirectToAction("Create");
            }

            var discharge = await GetDischargeById(dischargeId);
            if (discharge == null)
            {
                return RedirectToAction("Create");
            }

            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var dischargeStatuses = await GetDischargeStatuesAsync();
            var viewModel = new Domain.Dto.DischargeNoteViewModels
            {
                DischargeStatuses = dischargeStatuses,
                Create = discharge
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Domain.Dto.DischargeNoteViewModels dischargeNoteViewModel)
        {
            var patientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            ViewBag.ClientId = patientId;
            var dischargeNote = dischargeNoteViewModel.Create;
            var discharge = await EditDischargeNote(dischargeNote);
            if (discharge == null)
            {
                var dischargeStatuses = await GetDischargeStatuesAsync();

                var viewModel = new Domain.Dto.DischargeNoteViewModels
                {
                    Create = dischargeNoteViewModel.Create,
                    DischargeStatuses = dischargeStatuses
                };

                return View(viewModel);
            }

            return RedirectToAction("Index", "Admissions", new { patientId });
        }

        #endregion EditDisharge

        #region DischargeDetail

        public async Task<IActionResult> Details(string dischargeId)
        {
            if (string.IsNullOrEmpty(dischargeId))
            {
                return RedirectToAction("Create");
            }

            var discharge = await GetDischargeById(dischargeId);
            if (discharge == null)
            {
                return RedirectToAction("Create");
            }

            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var dischargeStatuses = await GetDischargeStatuesAsync();
            var viewModel = new Domain.Dto.DischargeNoteViewModels
            {
                DischargeStatuses = dischargeStatuses,
                Create = discharge
            };

            return View(viewModel);
        }

        #endregion DischargeDetail

        #region Discharge Helper

        private async Task<DoctorNotesDto?> GetLatestDoctorNoteInformation()
        {
            string admissionId = Guid.Empty.ToString();
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DoctorsNotes/LoadDoctorsNotes?admissionId={admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var doctorsNotes = JsonConvert.DeserializeObject<List<DoctorNotesDto>>(result) ?? new List<DoctorNotesDto>();
            var latestDoctorNote = doctorsNotes.OrderByDescending(x => x.DateOfNote).FirstOrDefault();
            return latestDoctorNote;
        }

        private async Task<VitalDto?> GetLatestVitalInformation()
        {
            string admissionId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Vitals/LoadVitals/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            var vitals = JsonConvert.DeserializeObject<List<VitalDto>>(result) ?? new List<VitalDto>();
            var latestVital = vitals.OrderByDescending(x => x.DateCollected).FirstOrDefault();
            return latestVital;
        }

        private async Task<List<DischargesDto>> GetDischargesByCurrentAdmission()
        {
            string admissionId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();

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

        private async Task<DischargesDto?> GetDischargeById(string dischargeId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Discharge/FindDischargeByKey/{dischargeId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            var discharge = JsonConvert.DeserializeObject<DischargesDto>(result);
            return discharge;
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

        private async Task<DischargesDto?> AddDischargeNote(DischargesDto dischargeNote)
        {
            AssignDischargeDateTime(dischargeNote);
            dischargeNote.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString());

            var data = JsonConvert.SerializeObject(dischargeNote);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/Discharge/AddDischarges", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var discharge = JsonConvert.DeserializeObject<DischargesDto>(result);
            return discharge;
        }

        private async Task<DischargesDto?> EditDischargeNote(DischargesDto dischargeNote)
        {
            AssignDischargeDateTime(dischargeNote);

            var data = JsonConvert.SerializeObject(dischargeNote);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/Discharge/EditDischarges", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            var discharge = JsonConvert.DeserializeObject<DischargesDto>(result);
            return discharge;
        }

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

        private async Task<AdmissionsDto?> UpdateAdmission(AdmissionsDto admission)
        {
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

        private void AssignDischargeDateTime(DischargesDto dischargeNote)
        {
            var dischargeDateTime = GetDischargeDateTime(dischargeNote.DischargeDate, dischargeNote.DischargeTime);
            dischargeNote.DischargeTime = dischargeDateTime;
            dischargeNote.DischargeDate = dischargeDateTime;
        }

        private DateTime GetDischargeDateTime(DateTime dischargeDate, DateTime disDateTime)
        {
            var date = dischargeDate.Date;
            var time = disDateTime.TimeOfDay;
            return date + time;
        }

        #endregion Discharge Helper

        public IActionResult Deathentry()
        {
            return View();
        }
    }
}