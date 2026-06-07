using IPD.Domain.Dto;
using IPD.Web.Extensions;
using IPD.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace IPD.Web.Controllers
{
    public class BirthDetailsController : Controller
    {
        private readonly string BaseUrl;
        private readonly IAppSettings appSettings;
        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public BirthDetailsController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.appSettings = appSettings;
            BaseUrl = this.appSettings.BaseUrl;
        }

        public async Task<IActionResult> Index(string partographId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var partograph = await GetById(partographId);

            if (partograph == null)
                return RedirectToAction("Index");

            return View(partograph);
        }

        #region Create

        public async Task<IActionResult> Create()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var birthDetail = new BirthDetailsDto
            {
                BirthDate = DateTime.Now,
                BirthTime = DateTime.Now,
            };
            return View(birthDetail);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartographDto model)
        {
            BirthDetailsDto birthDetails = new BirthDetailsDto
            {
                BirthDate = model.BirthDate,
                BirthTime = model.BirthTime,
                Gender = model.Gender,
                Remarks = model.Remarks,
                TypeOfDelivery = model.TypeOfDelivery,
                IsSuccessfulDelivery = model.IsSuccessfulDelivery,
                Weight = model.Weight,
                AdmissionID = model.AdmissionID,
                BirthDetailsID = model.BirthDetailsID
            };
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();

            var birthDetailsAdded = await CreateBirthDetail(birthDetails);

            var p = await GetPartographId();
            if (birthDetailsAdded == null)
            {
                return RedirectToAction("Index", "Partograph", new { partographId = p });
            }
            var partographId = await GetPartographId();
            return RedirectToAction("Details", "Partograph", new { partographId = partographId });
        }
        #endregion Create

        #region Edit
        public async Task<IActionResult> Edit(string birthDetailId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var birthDetails = await GetById(birthDetailId);

            if (birthDetails == null)
                return RedirectToAction("Index");

            return View(birthDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BirthDetailsDto birthDetails)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var birthDetailsUpdated = await UpdateBirthDetails(birthDetails);

            if (birthDetailsUpdated == null)
                return View(birthDetails);

            return RedirectToAction("Details", new
            {
                birthDetailsId = birthDetailsUpdated.BirthDetailsID.ToString()
            });
        }
        #endregion Edit

        #region CreateBirthDetail
        private async Task<BirthDetailsDto?> CreateBirthDetail(BirthDetailsDto birthDetails)
        {
            birthDetails.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? string.Empty);

            var data = JsonConvert.SerializeObject(birthDetails);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{BaseUrl}/BirthDetails/AddBirthDetails", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BirthDetailsDto>(result);
        }
        #endregion CreateBirthDetail

        #region UpdateBirthDetails

        private async Task<BirthDetailsDto?> UpdateBirthDetails(BirthDetailsDto birthDetails)
        {
            var data = JsonConvert.SerializeObject(birthDetails);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/BirthDetails/EditBirthDetails", httpContent);

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BirthDetailsDto>(result);
        }

        #endregion UpdateBirthDetails

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

        private async Task<BirthDetailsDto?> GetById(string birthDetailId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/BirthDetails/FindBirthDetailByKey/{birthDetailId}");

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            var birthDetail = JsonConvert.DeserializeObject<BirthDetailsDto>(result);

            return birthDetail;
        }

        private async Task<Guid> GetPartographId()
        {
            var admissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? string.Empty);

            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Partograph/LoadPartographByAdmissionId/{admissionID}");

            if (!response.IsSuccessStatusCode)
                return new Guid();

            string result = await response.Content.ReadAsStringAsync();
            var partographId = JsonConvert.DeserializeObject<Guid>(result);
            return partographId;
        }
    }
}