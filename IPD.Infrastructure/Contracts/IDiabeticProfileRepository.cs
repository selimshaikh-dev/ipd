using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IDiabeticProfileRepository : IRepository<DiabeticProfile>
    {
        Task<IEnumerable<DiabeticsProfileDto>> GetAllDiabeticProfile(Guid aid);
    }
}