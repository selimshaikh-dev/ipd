using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface ISearch : IRepository<Patient>
    {
        //IQueryable<Patient> PatientSearchByPin(string Pin);
        //IQueryable<Patient> PatientSearchByPatientId(Guid PatientId);
        //IQueryable<Patient> PatientSearchByCellPhone(string CellPhone);
    }
}