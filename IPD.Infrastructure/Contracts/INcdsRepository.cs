using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface INcdsRepository : IRepository<Ncd>
    {
        IList<Ncd> GetAllNcds();
    }
}