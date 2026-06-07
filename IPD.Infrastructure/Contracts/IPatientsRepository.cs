using IPD.Domain.Dto;
using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IPatientsRepository : IRepository<Patient>
    {
        Task<IEnumerable<Patient>> GetPatientsByCellPhone(string cellPhone);

        Task<IEnumerable<Patient>> GetPatientsByPatientId(Guid pid);

        Task<IEnumerable<Patient>> GetPatientsByNID(string nid);

        Task<IEnumerable<Patient>> GetPatientsByUHID(string uhid);

        Task<IEnumerable<Patient>> GetPatientsByAdvanced(string FirstNmae, string MiddleName, string LastName, DateTime Dob, Byte Sex);

        Task<IEnumerable<Patient>> GetPatientsByAdvanced(string FirstNmae, string MiddleName, string LastName, Byte Sex);

        Task<IEnumerable<Patient>> GetPatientsByAdvanced(string FirstNmae, string LastName, DateTime Dob, Byte Sex);

        Task<IEnumerable<Patient>> GetPatientsByAdvanced(string FirstNmae, string LastName, Byte Sex);

        Task<string> GeneratePatientUHID(DateTime dateOfBirth);

        IQueryable<Patient> PatientSearchByPin(string Pin);

        IQueryable<Patient> PatientSearchByPatientId(Guid PatientId);

        IQueryable<Patient> PatientSearchByCellPhone(string CellPhone);

        Task<PatientGetDto> GetPatientDetailsById(Guid patientID);
    }
}