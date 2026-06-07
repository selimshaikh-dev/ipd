using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IVitalRepository : IRepository<Vital>
    {
        Task<IEnumerable<VitalDto>> GetAllLoadVital(Guid aid);
    }
}
