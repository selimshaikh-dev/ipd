using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface ILiquorsRepository : IRepository<Liquor>
    {
        Liquor UpdateLiquor(Liquor liquor);
    }
}