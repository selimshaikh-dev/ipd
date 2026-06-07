using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class BloodPressureRepository : Repository<BloodPressure>, IBloodPressureRepository
    {
        private readonly DataContext context;

        public BloodPressureRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public BloodPressure UpdateBloodPressure(BloodPressure bloodPressure)
        {
            try
            {
                var existingInDb = context.BloodPressures
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(bloodPressure.PartographID) &&
                        i.BloodPressureTime.Equals(bloodPressure.BloodPressureTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new BloodPressure()
                    {
                        PartographID = bloodPressure.PartographID,
                        SystolicPressure = bloodPressure.SystolicPressure,
                        DiastolicPressure = bloodPressure.DiastolicPressure,
                        BloodPressureTime = bloodPressure.BloodPressureTime
                    };
                    context.BloodPressures.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.SystolicPressure != bloodPressure.SystolicPressure || existingInDb.DiastolicPressure != bloodPressure.DiastolicPressure)
                    {

                        existingInDb.SystolicPressure = bloodPressure.SystolicPressure;
                        existingInDb.DiastolicPressure = bloodPressure.DiastolicPressure;
                        context.Entry(existingInDb).State = EntityState.Modified;
                    }
                }

                return existingInDb;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}