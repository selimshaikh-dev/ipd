using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class Search : Repository<Patient>, ISearch
    {
        private readonly DataContext _context;

        public Search(DataContext context) : base(context)
        {
            this._context = context;
        }

        //public IQueryable<Patient> PatientSearchByCellPhone(string CellPhone)
        //{
        //    var PSCell = _context.Patients.Where(x => x.Cellphone == CellPhone);

        //    return PSCell;
        //}

        //public IQueryable<Patient> PatientSearchByPatientId(Guid PatientId)
        //{
        //    var PSId = _context.Patients.Where(x => x.PatientID == PatientId);

        //    return PSId;
        //}

        //public IQueryable<Patient> PatientSearchByPin(string Pin)
        //{
        //    var PSPin = _context.Patients.Where(x => x.NationalID == Pin);

        //    return PSPin;
        //}
    }
}