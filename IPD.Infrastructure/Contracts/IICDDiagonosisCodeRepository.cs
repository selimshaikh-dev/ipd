using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IICDDiagonosisCodeRepository : IRepository<ICDDigonosisCode>
    {
        IList<ICDDigonosisCode> GetAllICDDigonosisCode();
    }
}