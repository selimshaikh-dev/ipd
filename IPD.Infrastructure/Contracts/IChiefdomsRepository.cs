using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IChiefdomsRepository : IRepository<Chiefdom>
    {
        IList<ChiefdomDto> GetChiefdomList();

        Task<IList<ChiefdomDto>> GetChiefdomListAsync(int inkundlaId);
    }
}