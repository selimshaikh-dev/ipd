using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IOxytocinRepository : IRepository<Oxytocin>
    {
        Oxytocin UpdateOxytocin(Oxytocin oxytocin);
    }
}