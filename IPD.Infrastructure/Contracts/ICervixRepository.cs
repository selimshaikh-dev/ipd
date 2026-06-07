using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface ICervixRepository : IRepository<Cervix>
    {
        Cervix UpdateCervix(Cervix cervix);
    }
}