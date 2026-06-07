using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IRegionRepository : IRepository<Region>
    {
        IList<Region> GetAllRegion();
    }
}