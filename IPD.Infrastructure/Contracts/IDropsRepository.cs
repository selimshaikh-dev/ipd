using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IDropsRepository : IRepository<Drop>
    {
        Drop UpdateDrop(Drop drop);
    }
}