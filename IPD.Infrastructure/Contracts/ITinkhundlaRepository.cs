using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface ITinkhundlaRepository : IRepository<Tinkhundla>
    {
        IList<TinkhundlaDto> GetTinkhundlaList();
    }
}