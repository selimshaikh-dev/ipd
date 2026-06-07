using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IComplaintsRepository : IRepository<Complaint>
    {
        Task<IEnumerable<ComplaintDto>> GetAllLoadCompient(Guid aid);
    }
}