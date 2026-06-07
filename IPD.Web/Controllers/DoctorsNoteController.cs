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
    [ModuleAuthorized(UserAccessModule.DoctorsNote)]
    public class DoctorsNoteController : Controller
    {
        private readonly string BaseUrl;

        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;

        public DoctorsNoteController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var doctorsNotes = await GetByCurrentAdmissionId();

            return View(doctorsNotes);
        }

        public IActionResult Create()
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var doctorsNote = new DoctorNotesDto
            {
                DateOfNote = DateTime.Now,
                TimeOfNote = DateTime.Now
            };

            return View(doctorsNote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorNotesDto doctorsNote)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var doctorsNoteAdded = await CreateDoctorsNote(doctorsNote);

            if (doctorsNoteAdded == null)
                return View(doctorsNote);

            return RedirectToAction("Details", new
            {
                doctorsNoteId = doctorsNoteAdded.DoctorsNoteID.ToString()
            });
        }

        public async Task<IActionResult> Edit(string doctorsNoteId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var doctorsNote = await GetById(doctorsNoteId);

            if (doctorsNote == null)
                return RedirectToAction("Index");

            return View(doctorsNote);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorNotesDto doctorsNote)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var doctorsNoteUpdated = await UpdateDoctorsNote(doctorsNote);

            if (doctorsNoteUpdated == null)
                return View(doctorsNote);

            return RedirectToAction("Details", new
            {
                doctorsNoteId = doctorsNoteUpdated.DoctorsNoteID.ToString()
            });
        }

        public async Task<IActionResult> Details(string doctorsNoteId)
        {
            ViewBag.ClientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            var doctorsNote = await GetById(doctorsNoteId);

            if (doctorsNote == null)
                return RedirectToAction("Index");

            return View(doctorsNote);
        }

        private async Task<List<DoctorNotesDto>> GetByCurrentAdmissionId()
        {
            string admissionId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DoctorsNotes/LoadDoctorsNotes/{admissionId}");

            if (!response.IsSuccessStatusCode)
                return new List<DoctorNotesDto>();

            string result = await response.Content.ReadAsStringAsync();
            var doctorsNotes = JsonConvert.DeserializeObject<List<DoctorNotesDto>>(result);

            return doctorsNotes ?? new List<DoctorNotesDto>();
        }

        private async Task<DoctorNotesDto?> GetById(string doctorsNoteId)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/DoctorsNotes/FindDoctorsNoteByKey/{doctorsNoteId}");

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            var doctorsNote = JsonConvert.DeserializeObject<DoctorNotesDto>(result);

            return doctorsNote;
        }

        private async Task<DoctorNotesDto?> CreateDoctorsNote(DoctorNotesDto doctorsNote)
        {
            RemapDoctorsNoteDto(doctorsNote);

            doctorsNote.AdmissionID = Guid.Parse(session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString());
            var data = JsonConvert.SerializeObject(doctorsNote);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/DoctorsNotes/AddDoctorsNote", httpContent);

            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DoctorNotesDto>(result);
        }

        private async Task<DoctorNotesDto?> UpdateDoctorsNote(DoctorNotesDto doctorsNote)
        {
            RemapDoctorsNoteDto(doctorsNote);

            var data = JsonConvert.SerializeObject(doctorsNote);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{BaseUrl}/DoctorsNotes/EditDoctorsNote", httpContent);
            if (!response.IsSuccessStatusCode)
                return null;

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DoctorNotesDto>(result);
        }

        private void RemapDoctorsNoteDto(DoctorNotesDto doctorsNote)
        {
            var doctorsNoteDateTime = GetDoctorsNoteDateTime(doctorsNote.DateOfNote, doctorsNote.TimeOfNote);
            doctorsNote.DateOfNote = doctorsNoteDateTime;
            doctorsNote.TimeOfNote = doctorsNoteDateTime;
        }

        private DateTime GetDoctorsNoteDateTime(DateTime dateOfNote, DateTime timeOfNote)
        {
            var date = dateOfNote.Date;
            var time = timeOfNote.TimeOfDay;
            return date + time;
        }
    }
}