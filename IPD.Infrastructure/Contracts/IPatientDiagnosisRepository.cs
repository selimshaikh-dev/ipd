using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IPatientDiagnosisRepository : IRepository<PatientDiagnosis>
    {
        Task<List<PatientDiagnosisDto>> GetPatientDiagnosisByAdmissionIdAsync(Guid admissionId, string facilityCode);
    }
}