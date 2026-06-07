using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IVolumesRepository : IRepository<Volume>
    {
        Volume UpdateVolume(Volume volume);
    }
}