using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IIntervalRepository : IRepository<Interval>
    {
        IList<Interval> GetAllInterval();
    }
}