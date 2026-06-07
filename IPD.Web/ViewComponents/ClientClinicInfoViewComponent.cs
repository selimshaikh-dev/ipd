using IPD.Domain.Dto;
//using IPD.Domain.Dto.WebDto;
using IPD.Web.Extensions;
using IPD.Web.Models.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IPD.Web.ViewComponents
{
    public class ClientClinicInfoViewComponent : ViewComponent
    {
        private readonly string BaseUrl;
        private readonly IHttpContextAccessor httpContextAccessor;
        private ISession? session => httpContextAccessor.HttpContext?.Session;

        public ClientClinicInfoViewComponent(IHttpContextAccessor httpContextAccessor, IAppSettings appSettings)
        {
            this.httpContextAccessor = httpContextAccessor;
            BaseUrl = appSettings.BaseUrl;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = await GetCurrentClient();

            var admission = await GetAdmissionByKey();

            return View(new
            {
                Client = client ?? new ClientsInfoDto(),
                Admission = admission
            });
        }

        private async Task<ClientsInfoDto?> GetCurrentClient()
        {
            string patientId = session?.GetCurrentClientId() ?? Guid.Empty.ToString();
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/Patients/GetClientById?PatientId=" + patientId);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ClientsInfoDto>(result);
        }

        private async Task<AdmissionsDto?> GetAdmissionByKey()
        {
            using var client = new HttpClient();
            string patientId = session?.GetCurrentAdmissionId() ?? Guid.Empty.ToString();
            var response = await client.GetAsync($"{BaseUrl}/Admissions/FindAdmissionByKey/{patientId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AdmissionsDto>(result) ?? new AdmissionsDto();
        }
    }
}