
using IPD.Admin.Models.Contracts;

namespace IPD.Admin.Extensions
{
    public static class HttpClientExtension
    {
        public const string ClientName = "IPD.Admin.HttpClient";

        public static void AddClient(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            services.AddHttpClient(ClientName, (sp, client) =>
            {
                AddBaseUrl(sp, client);
                AddFacilityCode(sp, client);
            });
        }

        private static void AddBaseUrl(IServiceProvider serviceProvider, HttpClient client)
        {
            var appSettings = serviceProvider.GetRequiredService<IAppSettings>();
            var baseUrl = $"{appSettings.BaseUrl.TrimEnd('/')}/";
            client.BaseAddress = new Uri(baseUrl);
        }

        private static void AddFacilityCode(IServiceProvider serviceProvider, HttpClient client)
        {
            var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            var session = httpContextAccessor.HttpContext?.Session;
            if (session == null)
            {
                return;
            }

            var facilityCode = session.GetCurrentFacilityCode();
            if (string.IsNullOrEmpty(facilityCode))
            {
                return;
            }

            client.DefaultRequestHeaders.Add("x-facility-code", facilityCode);
        }
    }
}
