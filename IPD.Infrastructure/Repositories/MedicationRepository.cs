using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class MedicationRepository : Repository<Medication>, IMedicationRepository
    {
        private readonly DataContext context;

        public MedicationRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<Medication> GetAllMedication()
        {
            var list = (from s in context.Medications

                        where s.IsRowDeleted.Equals(false)
                        select new Medication
                        {
                            MedicationName = s.MedicationName,
                        }).ToList();
            return list;
        }
    }
}