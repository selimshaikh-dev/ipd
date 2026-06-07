using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IDirectionRepository : IRepository<Direction>
    {
        IList<Direction> GetAllDirection();
    }
}