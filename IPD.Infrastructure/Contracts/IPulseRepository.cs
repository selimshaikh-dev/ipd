using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IPulseRepository : IRepository<Pulse>
    {
        Pulse UpdatePulse(Pulse pulse);
    }
}