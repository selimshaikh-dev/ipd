using IPD.Domain.Dto;
using IPD.Web.Extensions;
using IPD.Web.Filters;
using IPD.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static IPD.Domain.Constants.Enumerators;
using static IPD.Web.Extensions.DateTimeExtensions;


namespace IPD.Web.Controllers
{
    [ModuleAuthorized(UserAccessModule.Partograph)]
    public class PartographController : Controller
    {
        private readonly string BaseUrl;
        private readonly IAppSettings appSettings;
        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public PartographController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.appSettings = appSettings;
            BaseUrl = this.appSettings.BaseUrl;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            ViewBag.facilityCode = session?.GetCurrentFacilityCode() ?? string.Empty;
            var partographId = await GetParotGraphId();
            ViewBag.partographId = partographId != Guid.Empty ? partographId.ToString(): "";
            await AddPatientInformation();
            await AssignAdmissionAndPatientInformationToView();
            var isCreatedBirthDetails = "false";
            var birthDetails = await BirthDetailsInfo();
            if (birthDetails != null)
            {
                isCreatedBirthDetails = "true";
            }
            ViewBag.isCreatedBirthDetails = isCreatedBirthDetails;
            ViewBag.BirthDetailList = await BirthDetailsInfoByPatientId();
            ViewBag.PartographDetaild = await PartographDetails();

            return View();
        }
        public async Task<IActionResult> Details(string partographId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            await AddPatientInformation();
            await AssignAdmissionAndPatientInformationToView();
            var birthDetailsInfo =await BirthDetailsInfo();
            ViewBag.facilityCode = session?.GetCurrentFacilityCode() ?? string.Empty;

            var partograph = await GetById(partographId);

            if (partograph == null)
                return RedirectToAction("Details");

            ViewBag.BirthdetailsInfo = birthDetailsInfo;
            return View(partograph);
        }

        #region Create
        public async Task<IActionResult> Create(string partographId = "")
        {
            if (string.IsNullOrEmpty(partographId))
            {
                var partographindb = await GetParotGraphId();
                partographId = partographindb.ToString();
            }
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            ViewBag.facilityCode = session?.GetCurrentFacilityCode() ?? string.Empty;
            
            var birthDetails = await BirthDetailsInfo();
            if (birthDetails != null)
            {
                return RedirectToAction("Details", "Partograph", new { partographId = partographId });
            }
            await AddPatientInformation();
            await AssignAdmissionAndPatientInformationToView();

            var partograph = await GetById(partographId);
            if (partograph == null)
            {
                var guidPartographId = Guid.Parse(partographId);
                partograph = new PartographDto
                {
                    PartographID = guidPartographId,
                    MembranesRuptured = DateTime.Now,
                    RegularContractions = DateTime.Now,
                    InitiateDate = DateTime.Now
                };
            }
  
            return View(partograph);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PartographDto partograph)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var partographAdded = await CreatePartograph(partograph);
            await AddPatientInformation();
            await AssignAdmissionAndPatientInformationToView();
            if (partographAdded == null)
            {
                return View(partograph);
            }

            ViewBag.Baseurl = BaseUrl;
            return RedirectToAction("Create");
        }

        #endregion Create
        private async Task<PartographDto?> CreatePartograph(PartographDto partograph)
        {
            partograph.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? string.Empty);

            var data = JsonConvert.SerializeObject(partograph);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{BaseUrl}/Partograph/SaveOrUpdatePartograph", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PartographDto>(result);
        }

        private async Task<bool> AddPatientInformation()
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

            return true;
        }

        private async Task<ClientsInfoDto?> GetPatientById(string patientId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Patients/GetClientById?patientId={patientId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();

            var patients = JsonConvert.DeserializeObject<ClientsInfoDto>(result);
            
            return patients;
        }

        private async Task<bool> AssignAdmissionAndPatientInformationToView()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            var admission = await GetAdmissionById(admissionId);
            if (admission == null)
            {
                return false;
            }
            ViewBag.Admission = admission;
            ViewBag.Baseurl = BaseUrl;
            return true;
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

        private async Task<PartographDto?> GetById(string partographId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Partograph/FindPartographByKey/{partographId}");

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            var partograph = JsonConvert.DeserializeObject<PartographDto>(result);

            return partograph;
        }

        //private void RemapDateDto(PartographDto partograph)
        //{
        //    var date = GetDateTime(partograph.RegularContractions, partograph.MembranesRuptured);
        //    partograph.RegularContractions = date;
        //    partograph.MembranesRuptured = date;
        //}

        //private DateTime GetDateTime(DateTime regularContractions, DateTime MembranesRepture)
        //{
        //    var date = regularContractions.Date;
        //    var time = MembranesRepture.TimeOfDay;
        //    return date + time;
        //}

        private async Task<Guid> GetParotGraphId()
        {
            var admissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? string.Empty);

            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Partograph/LoadPartographByAdmissionId/{admissionID}");

            if (!response.IsSuccessStatusCode)
                return Guid.Empty;

            string result = await response.Content.ReadAsStringAsync();
            var partographId = JsonConvert.DeserializeObject<Guid>(result);         
            return partographId;
        }

        private async Task<BirthDetailsDto> BirthDetailsInfo()
        {
           var admissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? string.Empty); 
           using  var client = new HttpClient();
           var response = await client.GetAsync($"{BaseUrl}/BirthDetails/LoadBirthDetailsByadmissionID/{admissionID}");

        if (!response.IsSuccessStatusCode)
            return null;

        string result = await response.Content.ReadAsStringAsync();
        var birthDetails = JsonConvert.DeserializeObject<BirthDetailsDto>(result);
        return birthDetails;
        }
        private async Task<List<PartographIndexDto>> BirthDetailsInfoByPatientId()
        {
            var patientId = Guid.Parse(session?.GetCurrentClientId() ?? string.Empty);
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/BirthDetails/LoadBirthDetailsByPatientId/{patientId}");

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            var birthDetails = JsonConvert.DeserializeObject<List<PartographIndexDto>>(result);
            return birthDetails;
        }

        private async Task<string> PartographDetails()
        {
            ViewBag.facility = session?.GetCurrentFacility() ?? string.Empty;
            var admissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? string.Empty);

            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Partograph/LoadPartographDetailsAdmissionId/{admissionID}");

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
           // var partographDetails = JsonConvert.DeserializeObject<string>(result);
            return result;
        }
    }
}