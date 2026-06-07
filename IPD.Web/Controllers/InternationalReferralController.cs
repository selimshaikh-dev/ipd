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
    [ModuleAuthorized(UserAccessModule.InternationalReferral)]
    public class InternationalReferralController : Controller
    {
        private readonly string BaseUrl;
        private readonly IAppSettings appSettings;
        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public InternationalReferralController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.appSettings = appSettings;
            BaseUrl = this.appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        public IActionResult ReferralOptions()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            return View();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var requestForReferral = await LoadAllRequestReferral();
            return View(requestForReferral);
        }

        #region

        public async Task<IActionResult> Create()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            if (!await GetAdmissionById(admissionId))
            {
                return RedirectToAction("Index");
            }

            if (!await AddPatientInformation())
            {
                return RedirectToAction("Index");
            }

            var requestForReferral = await FindRequestReferral(string.Empty);
            requestForReferral.InternationalReferral ??= new InternationalReferralsDto
            {
                Date = DateTime.Now,
                Time = DateTime.Now,
                AdmissionID = Guid.Parse(admissionId)
            };

            ViewBag.Procedures = await LoadProcedures();
            ViewBag.Regions = await LoadRegions();
            ViewBag.Countries = await LoadCountries();

            return View(requestForReferral);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RequestForReferralsDto requestForReferralDto)
        {
            var result = await ProcessRequestForReferral(requestForReferralDto);
            if (result != null)
            {
                if (result.InternationalReferral != null)
                {
                    return RedirectToAction("Details", new { internationalReferralId = result.InternationalReferral.InternationalReferralID });
                }

                if (result.InternationalReferral != null)
                {
                    requestForReferralDto.InternationalReferral = result.InternationalReferral;
                }

                if (!await AddPatientInformation())
                {
                    return RedirectToAction("Index");
                }
            }

            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;

            if (!await GetAdmissionById(admissionId))
            {
                return RedirectToAction("Index");
            }
            ViewBag.Procedures = await LoadProcedures();
            ViewBag.Regions = await LoadRegions();
            ViewBag.Countries = await LoadCountries();
            return View(requestForReferralDto);
        }

        #region
        public async Task<IActionResult> Edit(string internationalReferralId)
        {
            if (!await AddPatientInformation())
            {
                return RedirectToAction("Index");
            }

            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            if (!await GetAdmissionById(admissionId))
            {
                return RedirectToAction("Index");
            }

            var requestForReferral = await FindRequestReferral(internationalReferralId);
            requestForReferral.InternationalReferral ??= new InternationalReferralsDto
            {
                Date = DateTime.Now,
                Time = DateTime.Now,
                AdmissionID = Guid.Parse(admissionId)
            };

            ViewBag.Procedures = await LoadProcedures();
            ViewBag.Regions = await LoadRegions();
            ViewBag.Countries = await LoadCountries();

            return View(requestForReferral);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RequestForReferralsDto requestForReferralDto)
        {
            var result = await ProcessRequestForReferral(requestForReferralDto);
            if (result != null)
            {
                if (result.InternationalReferral != null)
                {
                    //return RedirectToAction("Index");
                    return RedirectToAction("Details", new { internationalReferralId = result.InternationalReferral.InternationalReferralID });
                }

                if (result.InternationalReferral != null)
                {
                    requestForReferralDto.InternationalReferral = result.InternationalReferral;
                }

                if (!await AddPatientInformation())
                {
                    return RedirectToAction("Index");
                }
            }

            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;

            if (!await GetAdmissionById(admissionId))
            {
                return RedirectToAction("Index");
            }

            ViewBag.Procedures = await LoadProcedures();
            ViewBag.Regions = await LoadRegions();
            ViewBag.Countries = await LoadCountries();
            return View(requestForReferralDto);
        }
        #endregion

        public async Task<IActionResult> Details(string internationalReferralId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var requestForReferral = await FindRequestReferral(internationalReferralId);
            return View(requestForReferral);
        }

        #region Helper

        #region Country

        public async Task<List<CountriesDto>> LoadCountries()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Countries/LoadCountryName");
            if (!response.IsSuccessStatusCode)
            {
                return new List<CountriesDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            var patientDetails = JsonConvert.DeserializeObject<List<CountriesDto>>(result);
            return patientDetails ?? new List<CountriesDto>();
        }
        #endregion

        #region Region
        public async Task<List<RegionsDto>> LoadRegions()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Regions/LoadRegions");
            if (!response.IsSuccessStatusCode)
            {
                return new List<RegionsDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            var patientDetails = JsonConvert.DeserializeObject<List<RegionsDto>>(result);
            return patientDetails ?? new List<RegionsDto>();
        }
        #endregion

        #region Procedure
        public async Task<List<ProceduresDto>> LoadProcedures()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Procedures/LoadProcedures");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ProceduresDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            var patientDetails = JsonConvert.DeserializeObject<List<ProceduresDto>>(result);
            return patientDetails ?? new List<ProceduresDto>();
        }
        #endregion

        #region Request Referral
        private async Task<InternationalReferralsDto?> ProcessAsync(InternationalReferralsDto internationalReferralDto)
        {
            if (internationalReferralDto.InternationalReferralID == Guid.Empty)
            {
                return await AddInternationalReferralAsync(internationalReferralDto);
            }

            return await EditInternationalReferralAsync(internationalReferralDto);
        }

        private async Task<RequestForReferralsDto?> ProcessRequestForReferral(RequestForReferralsDto requestForReferralDto)
        {
            InternationalReferralsDto internationalReferral = new InternationalReferralsDto();
            internationalReferral = requestForReferralDto.InternationalReferral;

            if (internationalReferral == null)
            {
                return null;
            }

            var internationalReferralResult = await ProcessAsync(internationalReferral);

            return new RequestForReferralsDto
            {
                InternationalReferral = internationalReferralResult,
            };
        }

        private async Task<RequestForReferralsDto> FindRequestReferral(string internationalReferralId)
        {
            var internationalReferral = await FindInternationalReferral(internationalReferralId);

            return new RequestForReferralsDto
            {
                InternationalReferral = internationalReferral,
            };
        }

        private async Task<List<RequestForReferralsDto>> LoadAllRequestReferral()
        {
            var internationalReferrals = await LoadAllInternationalReferral();
            if (!internationalReferrals.Any())
            {
                return new List<RequestForReferralsDto>();
            }

            return internationalReferrals.Select(internationalReferral => new RequestForReferralsDto
            {
                InternationalReferral = internationalReferral,
            }).ToList();
        }

        #endregion

        #region International Referral
        private async Task<InternationalReferralsDto?> EditInternationalReferralAsync(InternationalReferralsDto internationalReferral)
        {
            AssignDateTime(internationalReferral);
            var data = JsonConvert.SerializeObject(internationalReferral);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/InternationalReferrals/EditInternationalReferrals", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<InternationalReferralsDto>(result);
        }

        private async Task<InternationalReferralsDto?> AddInternationalReferralAsync(InternationalReferralsDto internationalReferral)
        {
            AssignDateTime(internationalReferral);
            var data = JsonConvert.SerializeObject(internationalReferral);
            using var client = new HttpClient();
            try
            {
                HttpContent httpContent = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{BaseUrl}/InternationalReferrals/AddInternationalReferrals", httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                string result = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<InternationalReferralsDto>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AssignDateTime(InternationalReferralsDto internationalReferral)
        {
            var dateTime = GetDateTime(internationalReferral.Date, internationalReferral.Time);
            internationalReferral.Date = dateTime;
            internationalReferral.Time = dateTime;
        }

        private async Task<InternationalReferralsDto?> FindInternationalReferral(string internationalReferralId)
        {
            if (string.IsNullOrEmpty(internationalReferralId))
            {
                return null;
            }

            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/InternationalReferrals/FindInternationalReferralByKey/{internationalReferralId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<InternationalReferralsDto>(result);
        }

        private async Task<List<InternationalReferralsDto>> LoadAllInternationalReferral()
        {
            var admissionId = session?.GetCurrentAdmissionId() ?? string.Empty;
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/InternationalReferrals/LoadInternationalReferrals/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<InternationalReferralsDto>();
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<InternationalReferralsDto>>(result) ?? new List<InternationalReferralsDto>();
        }
        #endregion

        #region Patient

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

            var procedures = await LoadProcedures();
            ViewBag.Procedures = procedures;
            return true;
        }

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

        #endregion

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

        private DateTime GetDateTime(DateTime date, DateTime time)
        {
            var dateOnly = date.Date;
            var timeOnly = time.TimeOfDay;
            return dateOnly + timeOnly;
        }

        #endregion
        #endregion
    }
}