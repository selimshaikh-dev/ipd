using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Web.Extensions;
using IPD.Web.Filters;
using IPD.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static IPD.Domain.Constants.Enumerators;

namespace IPD.Web.Controllers
{
    [ModuleAuthorized(UserAccessModule.Clients)]
    public class ClientsController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public ClientsController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string pin)
        {
            var country = new List<Country>();
            var inkhundla = new List<Tinkhundla>();
            var chiefdom = new List<Chiefdom>();

            using (var client = new HttpClient())
            {
                var response3 = await client.GetAsync($"{BaseUrl}/Countries/LoadCountryName");
                var response4 = await client.GetAsync($"{BaseUrl}/Tinkhundlas/LoadInkhundla");
                var response5 = await client.GetAsync($"{BaseUrl}/Chiefdoms/LoadChiefdom");

                string result3 = response3.Content.ReadAsStringAsync().Result;
                string result4 = response4.Content.ReadAsStringAsync().Result;
                string result5 = response5.Content.ReadAsStringAsync().Result;

                country = JsonConvert.DeserializeObject<List<Country>>(result3);
                inkhundla = JsonConvert.DeserializeObject<List<Tinkhundla>>(result4);
                chiefdom = JsonConvert.DeserializeObject<List<Chiefdom>>(result5);
            }
            ViewBag.country = country;
            ViewBag.inkhundla = inkhundla;
            ViewBag.chiefdom = chiefdom;
            var patient = new PatientDto();
            patient.baseUrl = BaseUrl;
            patient.NationalID = pin;
            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientDto clientRegistration)
        {
            if (clientRegistration.Email == null || clientRegistration.Email == "")
            {
                clientRegistration.Email = "";
            }
            if (clientRegistration.dateOfBirth != null)
            {
                clientRegistration.DOB = Convert.ToDateTime(clientRegistration.dateOfBirth);
            }

            clientRegistration.UHID = string.Empty;
            var clientRegistrationJson = JsonConvert.SerializeObject(clientRegistration);

            using (var client = new HttpClient())   //method used for provide data into API
            {
                HttpContent httpContent = new StringContent(clientRegistrationJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"{BaseUrl}/Patients/SaveOrUpdate", httpContent);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    //List Show
                    var list = JsonConvert.DeserializeObject<PatientDto>(result);
                    return RedirectToAction("ClientConfirmation", "Clients", list);
                }
                else
                {
                    var country = new List<Country>();
                    var inkhundla = new List<Tinkhundla>();
                    var chiefdom = new List<Chiefdom>();

                    using (var client2 = new HttpClient())  //method used for get data from API
                    {
                        var response5 = await client2.GetAsync($"{BaseUrl}/Countries/LoadCountryName");
                        var response6 = await client2.GetAsync($"{BaseUrl}/Tinkhundlas/LoadInkhundla");
                        var response7 = await client2.GetAsync($"{BaseUrl}/Chiefdoms/LoadChiefdom");

                        string result3 = response5.Content.ReadAsStringAsync().Result;
                        string result4 = response6.Content.ReadAsStringAsync().Result;
                        string result5 = response7.Content.ReadAsStringAsync().Result;

                        country = JsonConvert.DeserializeObject<List<Country>>(result3);
                        inkhundla = JsonConvert.DeserializeObject<List<Tinkhundla>>(result4);
                        chiefdom = JsonConvert.DeserializeObject<List<Chiefdom>>(result5);
                    }

                    ViewBag.country = country;
                    ViewBag.inkhundla = inkhundla;
                    ViewBag.chiefdom = chiefdom;

                    return View(clientRegistration);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string patientId)
        {
            try
            {
                var EditClient = new PatientDto();
                Guid PatientID = Guid.Parse(patientId);
                var country = new List<Country>();
                var inkhundla = new List<Tinkhundla>();
                var chiefdom = new List<Chiefdom>();

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{BaseUrl}/Patients/GetClientById?PatientId=" + PatientID);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;

                        EditClient = JsonConvert.DeserializeObject<PatientDto>(result);

                        var response6 = await client.GetAsync($"{BaseUrl}/Chiefdoms/GetChiefdomById?ChiefdomId=" + EditClient.ChiefdomID);
                        string result6 = response6.Content.ReadAsStringAsync().Result;
                        var cheifdomObj = JsonConvert.DeserializeObject<ChiefdomDto>(result6);
                        EditClient.TinkhundlaID = cheifdomObj.TinkhundlaID;

                        var response3 = await client.GetAsync($"{BaseUrl}/Countries/LoadCountryName");
                        var response4 = await client.GetAsync($"{BaseUrl}/Tinkhundlas/LoadInkhundla");
                        var response5 = await client.GetAsync($"{BaseUrl}/Chiefdoms/LoadChiefdom?InkhundlaID=" + EditClient.TinkhundlaID);

                        string result3 = response3.Content.ReadAsStringAsync().Result;
                        string result4 = response4.Content.ReadAsStringAsync().Result;
                        string result5 = response5.Content.ReadAsStringAsync().Result;

                        country = JsonConvert.DeserializeObject<List<Country>>(result3);
                        inkhundla = JsonConvert.DeserializeObject<List<Tinkhundla>>(result4);
                        chiefdom = JsonConvert.DeserializeObject<List<Chiefdom>>(result5);
                    }
                }
                ViewBag.country = country;
                ViewBag.inkhundla = inkhundla;
                ViewBag.chiefdom = chiefdom;
                EditClient.baseUrl = BaseUrl;
                return View(EditClient);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PatientDto editClient)
        {
            try
            {
                var country = new List<Country>();
                var inkhundla = new List<Tinkhundla>();
                var chiefdom = new List<Chiefdom>();

                using (var client = new HttpClient())
                {
                    if (!ModelState.IsValid)
                    {
                        var response6 = await client.GetAsync($"{BaseUrl}/Chiefdoms/GetChiefdomById?ChiefdomId=" + editClient.ChiefdomID);
                        if (response6.IsSuccessStatusCode)
                        {
                            string result6 = response6.Content.ReadAsStringAsync().Result;

                            var cheifdomObj = JsonConvert.DeserializeObject<ChiefdomDto>(result6);
                            editClient.TinkhundlaID = cheifdomObj.TinkhundlaID;

                            var response3 = await client.GetAsync($"{BaseUrl}/Countries/LoadCountryName");
                            var response4 = await client.GetAsync($"{BaseUrl}/Tinkhundlas/LoadInkhundla");
                            var response5 = await client.GetAsync($"{BaseUrl}/Chiefdoms/LoadChiefdom?InkhundlaID=" + editClient.TinkhundlaID);

                            string result3 = response3.Content.ReadAsStringAsync().Result;
                            string result4 = response4.Content.ReadAsStringAsync().Result;
                            string result5 = response5.Content.ReadAsStringAsync().Result;

                            country = JsonConvert.DeserializeObject<List<Country>>(result3);
                            inkhundla = JsonConvert.DeserializeObject<List<Tinkhundla>>(result4);
                            chiefdom = JsonConvert.DeserializeObject<List<Chiefdom>>(result5);
                        }

                        ViewBag.country = country;
                        ViewBag.inkhundla = inkhundla;
                        ViewBag.chiefdom = chiefdom;
                        editClient.baseUrl = BaseUrl;
                        return View(editClient);
                    }
                    else
                    {
                        var user = JsonConvert.SerializeObject(editClient);
                        HttpContent httpContent = new StringContent(user, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync($"{BaseUrl}/Patients/SaveOrUpdate", httpContent);

                        if (response.IsSuccessStatusCode)
                        {
                            string result = await response.Content.ReadAsStringAsync();
                            var clientDto = JsonConvert.DeserializeObject<PatientDto>(result);

                            return RedirectToAction("ClientUpdateConfirmation", "Clients",new { patientId =clientDto.PatientID});
                        }
                        else
                        {
                            var response6 = await client.GetAsync($"{BaseUrl}/Chiefdoms/GetChiefdomById?ChiefdomId=" + editClient.ChiefdomID);
                            string result6 = response6.Content.ReadAsStringAsync().Result;
                            var cheifdomObj = JsonConvert.DeserializeObject<ChiefdomDto>(result6);
                            editClient.TinkhundlaID = cheifdomObj.TinkhundlaID;

                            var response3 = await client.GetAsync($"{BaseUrl}/Countries/LoadCountryName");
                            var response4 = await client.GetAsync($"{BaseUrl}/Tinkhundlas/LoadInkhundla");
                            var response5 = await client.GetAsync($"{BaseUrl}/Chiefdoms/LoadChiefdom?TinkhundlaID=" + editClient.TinkhundlaID);

                            string result3 = response3.Content.ReadAsStringAsync().Result;
                            string result4 = response4.Content.ReadAsStringAsync().Result;
                            string result5 = response5.Content.ReadAsStringAsync().Result;

                            country = JsonConvert.DeserializeObject<List<Country>>(result3);
                            inkhundla = JsonConvert.DeserializeObject<List<Tinkhundla>>(result4);
                            chiefdom = JsonConvert.DeserializeObject<List<Chiefdom>>(result5);

                            ViewBag.country = country;
                            ViewBag.inkhundla = inkhundla;
                            ViewBag.chiefdom = chiefdom;
                            editClient.baseUrl = BaseUrl;

                            return View(editClient);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult ProfileSearch(string? message)
        {
            session?.SetCurrentClientId(string.Empty);
            session?.SetCurrentAdmissionId(string.Empty);
            PatientDto model = new PatientDto
            {
                message = message,
                baseUrl = BaseUrl
            };
            return View(model);
        }

        public IActionResult AdvancedSearch()
        {
            session?.SetCurrentClientId(string.Empty);
            session?.SetCurrentAdmissionId(string.Empty);
            PatientDto model = new PatientDto
            {
                baseUrl = BaseUrl
            };
            return View(model);
        }

        public async Task<IActionResult> ClientConfirmation(PatientDto clientRegistration)
        {
            var patientDetails = await GetClientDetailsById(clientRegistration.PatientID.ToString());
            return View(patientDetails);
        }

        public async Task<IActionResult> ClientUpdateConfirmation(string patientId)
        {
            var patientDetails = await GetClientDetailsById(patientId);
            return View(patientDetails);
        }

        public async Task<IActionResult> ManageClient(string patientId)
        {
            if (string.IsNullOrEmpty(patientId))
            {
                return View();
            }

            var clientViewModel = await GetClientById(patientId);
            if (clientViewModel == null)
            {
                return View();
            }

            session?.SetCurrentClientId(clientViewModel.PatientID.ToString());
            var admissions = await GetByPatientId(clientViewModel.PatientID.ToString());
            session?.SetCurrentAdmissionId((admissions.FirstOrDefault(x => x.IsDischarged == false)?.AdmissionID ?? Guid.Empty).ToString());

            return View(clientViewModel);
        }

        private async Task<List<AdmissionsDto>> GetByPatientId(string patientId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Admissions/LoadAdmission/{patientId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<AdmissionsDto>();
            }

            string result = await response.Content.ReadAsStringAsync();

            var admissions = JsonConvert.DeserializeObject<List<AdmissionsDto>>(result);
            return admissions ?? new List<AdmissionsDto>();
        }

        private async Task<PatientDto?> GetClientById(string patientId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Patients/GetClientById?PatientId=" + patientId);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientDto>(result);
        }


        private async Task<PatientGetDto?> GetClientDetailsById(string patientId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Patients/GetClientDetailsById?PatientId=" + patientId);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PatientGetDto>(result);
        }
    }
}