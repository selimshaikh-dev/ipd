using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IPartographDetailsRepository : IRepository<PartographDetail>
    {
        Task<PartographDetailReadDto> GetPartographDetailsAsync(Guid partographId);
    }
}