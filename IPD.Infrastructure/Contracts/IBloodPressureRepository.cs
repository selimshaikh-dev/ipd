using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IBloodPressureRepository : IRepository<BloodPressure>
    {
        BloodPressure UpdateBloodPressure(BloodPressure bloodPressure);
    }
}