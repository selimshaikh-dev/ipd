using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IPinSearchRepository : IDapperRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetByPIN(long PIN);
    }
}