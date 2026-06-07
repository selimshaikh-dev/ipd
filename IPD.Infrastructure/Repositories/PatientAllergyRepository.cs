using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class PatientAllergyRepository : Repository<PatientAllergy>, IPatientAllergyRepository
    {
        public PatientAllergyRepository(DataContext context) : base(context)
        {
        }
    }
}