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
    [ModuleAuthorized(UserAccessModule.Surgeries)]
    public class SurgeriesController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public SurgeriesController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            string admissionId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();
            var surgeries = new List<SurgeriesDto>();

            using var client = new HttpClient();

            var response = await client.GetAsync($"{BaseUrl}/Surgeries/LoadSurgeries/{admissionId}");
            if (!response.IsSuccessStatusCode)
            {
                return View(surgeries);
            }

            string result = await response.Content.ReadAsStringAsync();
            surgeries = JsonConvert.DeserializeObject<List<SurgeriesDto>>(result) ?? new List<SurgeriesDto>();

            var surgeryTypes = new List<SurgeryTypeDto>();
            var surgeryTypeResponse = await client.GetAsync($"{BaseUrl}/SurgeryTypes/LoadSurgeryTypes");
            if (surgeryTypeResponse.IsSuccessStatusCode)
            {
                string surgeryTypeResult = await surgeryTypeResponse.Content.ReadAsStringAsync();
                surgeryTypes = JsonConvert.DeserializeObject<List<SurgeryTypeDto>>(surgeryTypeResult) ?? new List<SurgeryTypeDto>();
            }
            ViewBag.SurgeryTypes = surgeryTypes;

            var surgicalProcedures = new List<SurgicalProceduresDto>();
            var surgicalProcedureResponse = await client.GetAsync($"{BaseUrl}/SurgicalProcedures/LoadSurgicalProcedures");
            if (surgicalProcedureResponse.IsSuccessStatusCode)
            {
                string surgicalProcedureResult = await surgicalProcedureResponse.Content.ReadAsStringAsync();
                surgicalProcedures = JsonConvert.DeserializeObject<List<SurgicalProceduresDto>>(surgicalProcedureResult) ?? new List<SurgicalProceduresDto>();
            }

            ViewBag.SurgicalProcedures = surgicalProcedures;

            return View(surgeries);
        }

        #endregion Index

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var surgery = new SurgeriesDto
            {
                SurgeryDate = DateTime.Now,
                SurgeryTime = DateTime.Now
            };

            using var client = new HttpClient();

            var surgeryTypes = new List<SurgeryTypeDto>();
            var surgeryTypeResponse = await client.GetAsync($"{BaseUrl}/SurgeryTypes/LoadSurgeryTypes");
            if (surgeryTypeResponse.IsSuccessStatusCode)
            {
                string surgeryTypeResult = await surgeryTypeResponse.Content.ReadAsStringAsync();
                surgeryTypes = JsonConvert.DeserializeObject<List<SurgeryTypeDto>>(surgeryTypeResult) ?? new List<SurgeryTypeDto>();
            }
            ViewBag.SurgeryTypes = surgeryTypes;

            var surgicalProcedures = new List<SurgicalProceduresDto>();
            var surgicalProcedureResponse = await client.GetAsync($"{BaseUrl}/SurgicalProcedures/LoadSurgicalProcedures");
            if (surgicalProcedureResponse.IsSuccessStatusCode)
            {
                string surgicalProcedureResult = await surgicalProcedureResponse.Content.ReadAsStringAsync();
                surgicalProcedures = JsonConvert.DeserializeObject<List<SurgicalProceduresDto>>(surgicalProcedureResult) ?? new List<SurgicalProceduresDto>();
            }

            ViewBag.SurgicalProcedures = surgicalProcedures;

            return View(surgery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SurgeriesDto surgery)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            using var client = new HttpClient();

            var surgeryTypes = new List<SurgeryTypeDto>();
            var surgeryTypeResponse = await client.GetAsync($"{BaseUrl}/SurgeryTypes/LoadSurgeryTypes");
            if (surgeryTypeResponse.IsSuccessStatusCode)
            {
                string surgeryTypeResult = await surgeryTypeResponse.Content.ReadAsStringAsync();
                surgeryTypes = JsonConvert.DeserializeObject<List<SurgeryTypeDto>>(surgeryTypeResult) ?? new List<SurgeryTypeDto>();
            }
            ViewBag.SurgeryTypes = surgeryTypes;

            var surgicalProcedures = new List<SurgicalProceduresDto>();
            var surgicalProcedureResponse = await client.GetAsync($"{BaseUrl}/SurgicalProcedures/LoadSurgicalProcedures");
            if (surgicalProcedureResponse.IsSuccessStatusCode)
            {
                string surgicalProcedureResult = await surgicalProcedureResponse.Content.ReadAsStringAsync();
                surgicalProcedures = JsonConvert.DeserializeObject<List<SurgicalProceduresDto>>(surgicalProcedureResult) ?? new List<SurgicalProceduresDto>();
            }

            ViewBag.SurgicalProcedures = surgicalProcedures;

            surgery.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString());
            var data = JsonConvert.SerializeObject(surgery);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/Surgeries/AddSurgeries", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                return View(surgery);
            }

            string result = await response.Content.ReadAsStringAsync();
            var surgeryAdded = JsonConvert.DeserializeObject<SurgeriesDto>(result);
            if (surgeryAdded == null)
            {
                return View(surgery);
            }

            return RedirectToAction("Details", new
            {
                surgeryId = surgeryAdded.SurgeryID.ToString()
            });
        }

        #endregion Create

        #region Edit
        public async Task<IActionResult> Edit(string surgeryId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            using var client = new HttpClient();

            var response = await client.GetAsync($"{BaseUrl}/Surgeries/FindSurgeryByKey/{surgeryId}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            string result = await response.Content.ReadAsStringAsync();
            var surgery = JsonConvert.DeserializeObject<SurgeriesDto>(result);
            if (surgery == null)
            {
                return RedirectToAction("Index");
            }

            var surgeryTypes = new List<SurgeryTypeDto>();
            var surgeryTypeResponse = await client.GetAsync($"{BaseUrl}/SurgeryTypes/LoadSurgeryTypes");
            if (surgeryTypeResponse.IsSuccessStatusCode)
            {
                string surgeryTypeResult = await surgeryTypeResponse.Content.ReadAsStringAsync();
                surgeryTypes = JsonConvert.DeserializeObject<List<SurgeryTypeDto>>(surgeryTypeResult) ?? new List<SurgeryTypeDto>();
            }
            ViewBag.SurgeryTypes = surgeryTypes;

            var surgicalProcedures = new List<SurgicalProceduresDto>();
            var surgicalProcedureResponse = await client.GetAsync($"{BaseUrl}/SurgicalProcedures/LoadSurgicalProcedures");
            if (surgicalProcedureResponse.IsSuccessStatusCode)
            {
                string surgicalProcedureResult = await surgicalProcedureResponse.Content.ReadAsStringAsync();
                surgicalProcedures = JsonConvert.DeserializeObject<List<SurgicalProceduresDto>>(surgicalProcedureResult) ?? new List<SurgicalProceduresDto>();
            }

            ViewBag.SurgicalProcedures = surgicalProcedures;

            return View(surgery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SurgeriesDto surgery)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            using var client = new HttpClient();

            var surgeryTypes = new List<SurgeryTypeDto>();
            var surgeryTypeResponse = await client.GetAsync($"{BaseUrl}/SurgeryTypes/LoadSurgeryTypes");
            if (surgeryTypeResponse.IsSuccessStatusCode)
            {
                string surgeryTypeResult = await surgeryTypeResponse.Content.ReadAsStringAsync();
                surgeryTypes = JsonConvert.DeserializeObject<List<SurgeryTypeDto>>(surgeryTypeResult) ?? new List<SurgeryTypeDto>();
            }
            ViewBag.SurgeryTypes = surgeryTypes;

            var surgicalProcedures = new List<SurgicalProceduresDto>();
            var surgicalProcedureResponse = await client.GetAsync($"{BaseUrl}/SurgicalProcedures/LoadSurgicalProcedures");
            if (surgicalProcedureResponse.IsSuccessStatusCode)
            {
                string surgicalProcedureResult = await surgicalProcedureResponse.Content.ReadAsStringAsync();
                surgicalProcedures = JsonConvert.DeserializeObject<List<SurgicalProceduresDto>>(surgicalProcedureResult) ?? new List<SurgicalProceduresDto>();
            }

            ViewBag.SurgicalProcedures = surgicalProcedures;

            var data = JsonConvert.SerializeObject(surgery);
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/Surgeries/EditSurgeries", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                return View(surgery);
            }

            string result = await response.Content.ReadAsStringAsync();
            var surgeryAdded = JsonConvert.DeserializeObject<SurgeriesDto>(result);
            if (surgeryAdded == null)
            {
                return View(surgery);
            }

            return RedirectToAction("Details", new
            {
                surgeryId = surgeryAdded.SurgeryID.ToString()
            });
        }

        #endregion Edit

        #region Details
        public async Task<IActionResult> Details(string surgeryId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            using var client = new HttpClient();

            var response = await client.GetAsync($"{BaseUrl}/Surgeries/FindSurgeryByKey/{surgeryId}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            string result = await response.Content.ReadAsStringAsync();
            var surgery = JsonConvert.DeserializeObject<SurgeriesDto>(result);
            if (surgery == null)
            {
                return RedirectToAction("Index");
            }

            var surgeryTypes = new List<SurgeryTypeDto>();
            var surgeryTypeResponse = await client.GetAsync($"{BaseUrl}/SurgeryTypes/LoadSurgeryTypes");
            if (surgeryTypeResponse.IsSuccessStatusCode)
            {
                string surgeryTypeResult = await surgeryTypeResponse.Content.ReadAsStringAsync();
                surgeryTypes = JsonConvert.DeserializeObject<List<SurgeryTypeDto>>(surgeryTypeResult) ?? new List<SurgeryTypeDto>();
            }
            ViewBag.SurgeryTypes = surgeryTypes;

            var surgicalProcedures = new List<SurgicalProceduresDto>();
            var surgicalProcedureResponse = await client.GetAsync($"{BaseUrl}/SurgicalProcedures/LoadSurgicalProcedures");
            if (surgicalProcedureResponse.IsSuccessStatusCode)
            {
                string surgicalProcedureResult = await surgicalProcedureResponse.Content.ReadAsStringAsync();
                surgicalProcedures = JsonConvert.DeserializeObject<List<SurgicalProceduresDto>>(surgicalProcedureResult) ?? new List<SurgicalProceduresDto>();
            }

            ViewBag.SurgicalProcedures = surgicalProcedures;

            return View(surgery);
        }

        public async Task<IActionResult> SurgeryDetails(string surgeryId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            using var client = new HttpClient();

            var response = await client.GetAsync($"{BaseUrl}/Surgeries/FindSurgeryByKey/{surgeryId}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            string result = await response.Content.ReadAsStringAsync();
            var surgery = JsonConvert.DeserializeObject<SurgeriesDto>(result);
            if (surgery == null)
            {
                return RedirectToAction("Index");
            }

            var surgeryTypes = new List<SurgeryTypeDto>();
            var surgeryTypeResponse = await client.GetAsync($"{BaseUrl}/SurgeryTypes/LoadSurgeryTypes");
            if (surgeryTypeResponse.IsSuccessStatusCode)
            {
                string surgeryTypeResult = await surgeryTypeResponse.Content.ReadAsStringAsync();
                surgeryTypes = JsonConvert.DeserializeObject<List<SurgeryTypeDto>>(surgeryTypeResult) ?? new List<SurgeryTypeDto>();
            }
            ViewBag.SurgeryTypes = surgeryTypes;

            var surgicalProcedures = new List<SurgicalProceduresDto>();
            var surgicalProcedureResponse = await client.GetAsync($"{BaseUrl}/SurgicalProcedures/LoadSurgicalProcedures");
            if (surgicalProcedureResponse.IsSuccessStatusCode)
            {
                string surgicalProcedureResult = await surgicalProcedureResponse.Content.ReadAsStringAsync();
                surgicalProcedures = JsonConvert.DeserializeObject<List<SurgicalProceduresDto>>(surgicalProcedureResult) ?? new List<SurgicalProceduresDto>();
            }

            ViewBag.SurgicalProcedures = surgicalProcedures;

            var postSurgeryResponse = await client.GetAsync($"{BaseUrl}/PostSurgeries/LoadPostSurgeries/{surgeryId}");
            if (postSurgeryResponse.IsSuccessStatusCode)
            {
                string postSurgeryResult = await postSurgeryResponse.Content.ReadAsStringAsync();
                var postSurgeries = JsonConvert.DeserializeObject<List<PostSurgeriesDto>>(postSurgeryResult);
                surgery.PostSurgeries = postSurgeries;
            }

            return View(surgery);
        }

        #endregion Details

        /// <summary>
        /// Use for Linking, Remove later
        /// </summary>
        /// <returns></returns>
        public IActionResult SurgicalProcedure()
        {
            return View();
        }
    }
}