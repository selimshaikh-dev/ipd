using AspNetCore.Reporting;
using IPD.Domain.Dto;
using IPD.Web.Extensions;
using IPD.Web.Filters;
using IPD.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;
using System.Text;


namespace IPD.Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly string BaseUrl;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment webHostEnvirnoment;

        private ISession? session => httpContextAccessor.HttpContext?.Session;
        private readonly HttpClient httpClient;
        public ReportController(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings, IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvirnoment)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.webHostEnvirnoment = webHostEnvirnoment;
            BaseUrl = appSettings.BaseUrl;
            this.httpClient = httpClientFactory.CreateClient(HttpClientExtension.ClientName);
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PatientList()
        {
            return  View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>PatientList(DateTimeDto dateTimeDto)
        {
            var PatientsData = await PatientLists(dateTimeDto);           
            var facility = httpContextAccessor.HttpContext?.Session.GetCurrentFacility();
            
            var EditUser = new UserEditDto();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;
            Guid UserId = Guid.Empty;
            if (claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault() != null)
            {
                UserId = Guid.Parse(claim.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value?.ToString());
                EditUser.UserAccountID = UserId;
            }
          
            using (var client = new HttpClient())
            {
                var response3 = await client.GetAsync($"{BaseUrl}/Users/GetForEdit?UserAccountId=" + EditUser.UserAccountID);              
                string result2 = response3.Content.ReadAsStringAsync().Result;              
                EditUser = JsonConvert.DeserializeObject<UserEditDto>(result2);
                EditUser.Password = "mk3ljiuhytrfed==";
                
            }
            string username = EditUser.FirstName + " " + EditUser.LastName;
            string path = Path.Combine(webHostEnvirnoment.ContentRootPath, "reports", "Admissionreport.rdlc");
            string mimtype = "";
            int extension = 1;
          
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("fromdate", "("+dateTimeDto.FromDate.ToString("dd/MM/yyyy")+" - "+ dateTimeDto.ToDate.ToString("dd/MM/yyyy")+")");
           
            parameters.Add("facility", facility);
            parameters.Add("generateby", username);
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("AdmissionReportSet", PatientsData);
            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimtype);
            return File(result.MainStream, "application/pdf");
        }

     

        private async Task<List<ReportDto>> PatientLists(DateTimeDto dateTimeDto)
        {
            var data = JsonConvert.SerializeObject(dateTimeDto);
            using var client = new HttpClient();
            var httpContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"{BaseUrl}/Report/LoadReport", httpContent);
            if (!response.IsSuccessStatusCode)
            {
                return new List<ReportDto>();
            }
            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ReportDto>>(result) ?? new List<ReportDto>();
        }

       
    }
}
