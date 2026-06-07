using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IPatientExaminationsRepository : IRepository<PatientExamination>
    {
        Task<List<PatientExaminationsDto>> GetPatientExaminationsByAdmissionIdAsync(Guid admissionId, string facilityCode);
    }
}