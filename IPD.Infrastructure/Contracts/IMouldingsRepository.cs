using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IMouldingsRepository : IRepository<Moulding>
    {
        Moulding UpdateMoulding(Moulding moulding);
    }
}