using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IPartographRepository : IRepository<Partograph>
    {
        Task<Guid> GetPartographIdByAdmissionId(Guid aid);
        Task<string> GetPartographByAdmissionId(Guid aid);
    }
}