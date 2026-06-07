using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IProteinsRepository : IRepository<Protein>
    {
        Protein UpdateProtein(Protein protein);
    }
}