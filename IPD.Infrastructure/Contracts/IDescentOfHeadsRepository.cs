using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IDescentOfHeadsRepository : IRepository<DescentOfHead>
    {
        DescentOfHead UpdateDescentOfHead(DescentOfHead descentOfHead);
    }
}