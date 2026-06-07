using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IBirthDetailsRepository : IRepository<BirthDetail>
    {
        Task<List<PartographIndexDto>> GetByPatientId(Guid patientId);

        Task<BirthDetail> GetByAdmissionID(Guid admissionID);
    }
}