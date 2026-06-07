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
    [ModuleAuthorized(UserAccessModule.ChiefComplaints)]
    public class ChiefComplaintsController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public ChiefComplaintsController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var complaints = await GetByAdmissionId();
            await AssignAllergiesAndNcdToView();
            return View(complaints);
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

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var complaint = new ChiefComplaintsDto { };
            await AssignAllergiesAndNcdToView();
            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChiefComplaintsDto complaintsDTO)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var complaintAdded = await CreateComplaint(complaintsDTO);
            if (complaintAdded == null)
            {
                await AssignAllergiesAndNcdToView();
                return View(complaintsDTO);
            }

            return RedirectToAction("Details", new
            {
                chiefComplaintId = complaintAdded.ComplaintID.ToString()
            });
        }

        #endregion Create

        #region Edit

        public async Task<IActionResult> Edit(string complaintId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var complaint = await GetById(complaintId);

            if (complaint == null)
            {
                return RedirectToAction("Index");
            }

            await AssignAllergiesAndNcdToView();
            return View(complaint);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChiefComplaintsDto complaints)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var complaintUpdated = await UpdateComplaint(complaints);

            if (complaintUpdated == null)
            {
                await AssignAllergiesAndNcdToView();
                return View(complaints);
            }

            return RedirectToAction("Details", new
            {
                chiefComplaintId = complaintUpdated.ComplaintID.ToString()
            });
        }

        #endregion Edit

        #region Details
        public async Task<IActionResult> Details(string chiefComplaintId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var complaints = await GetById(chiefComplaintId);

            if (complaints == null)
            {
                return RedirectToAction("Index");
            }

            await AssignAllergiesAndNcdToView();
            return View(complaints);
        }

        #endregion Details
        private async Task AssignAllergiesAndNcdToView()
        {
            var allergies = await GetAllergiesAsync();
            var ncds = await GetNcdsAsync();

            ViewBag.Allergies = allergies;
            ViewBag.Ncds = ncds;
        }

        private void AssignAllergiesAndNcdToModel(ChiefComplaintsDto complaintsDTO)
        {
            if (complaintsDTO.PatientsNcdsIds != null && complaintsDTO.PatientsNcdsIds.Any())
            {
                var ncds = complaintsDTO.PatientsNcdsIds
                    .Select(ncdsId => new PatientsNcdDto
                    {
                        NcdsID = ncdsId
                    }).ToList();
                complaintsDTO.PatientsNcds = ncds;
            }

            if (complaintsDTO.PatientAllergyIds != null && complaintsDTO.PatientAllergyIds.Any())
            {
                var allergies = complaintsDTO.PatientAllergyIds
                    .Select(allergyId => new PatientAllergyDto
                    {
                        AllergiesID = allergyId
                    }).ToList();
                complaintsDTO.PatientAllergy = allergies;
            }
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

        private async Task<List<NcdDto>> GetNcdsAsync()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Ncds/LoadNcds");

            if (!response.IsSuccessStatusCode)
            {
                return new List<NcdDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            var ncds = JsonConvert.DeserializeObject<List<NcdDto>>(result);

            return ncds ?? new List<NcdDto>();
        }

        private async Task<List<ChiefComplaintsDto>> GetByAdmissionId()
        {
            string admissionId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();

            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Complaints/LoadComplaints/{admissionId}");

            if (!response.IsSuccessStatusCode)
            {
                return new List<ChiefComplaintsDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            var complaints = JsonConvert.DeserializeObject<List<ChiefComplaintsDto>>(result);

            return complaints ?? new List<ChiefComplaintsDto>();
        }

        private async Task<ChiefComplaintsDto?> GetById(string complaintId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Complaints/FindComplaintByKey/{complaintId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ChiefComplaintsDto>(result);
        }

        private async Task<ChiefComplaintsDto?> CreateComplaint(ChiefComplaintsDto complaintsDTO)
        {
            complaintsDTO.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? string.Empty);
            AssignAllergiesAndNcdToModel(complaintsDTO);
            var data = JsonConvert.SerializeObject(complaintsDTO);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/Complaints/AddComplaints", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ChiefComplaintsDto>(result);
        }

        private async Task<ChiefComplaintsDto?> UpdateComplaint(ChiefComplaintsDto complaintsDTO)
        {
            AssignAllergiesAndNcdToModel(complaintsDTO);
            var data = JsonConvert.SerializeObject(complaintsDTO);

            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/Complaints/EditComplaints", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ChiefComplaintsDto>(result);
        }
    }
}