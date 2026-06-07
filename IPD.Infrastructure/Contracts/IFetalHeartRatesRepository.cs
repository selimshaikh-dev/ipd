using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IFetalHeartRatesRepository : IRepository<FetalHeartRate>
    {
        FetalHeartRate UpdateFatalRate(FetalHeartRate fetalHeartRate);
    }
}