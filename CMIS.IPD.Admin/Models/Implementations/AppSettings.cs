using IPD.Admin.Models.Contracts;

namespace IPD.Admin.Models.Implementations
{
    public class AppSettings:IAppSettings
    {
        private readonly IConfiguration configuration;

        public AppSettings(IConfiguration configuration)
        {
            this.configuration = configuration;
            BaseUrl =  configuration.GetSection("BaseUrl").Value;
        }

        public string BaseUrl
        {
            get;private set;
        }
    }
}
