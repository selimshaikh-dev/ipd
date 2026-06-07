using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class FetalHeartRatesRepository : Repository<FetalHeartRate>, IFetalHeartRatesRepository
    {
        private readonly DataContext context;

        public FetalHeartRatesRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public FetalHeartRate UpdateFatalRate(FetalHeartRate fetalHeartRate)
        {
            try
            {
                var existingInDb = context.FetalHeartRates
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(fetalHeartRate.PartographID) &&
                        i.FetalRateTime.Equals(fetalHeartRate.FetalRateTime)
                    );
                if (existingInDb == null)
                {
                    existingInDb = new FetalHeartRate()
                    {
                        PartographID = fetalHeartRate.PartographID,
                        FetalRateTime = fetalHeartRate.FetalRateTime,
                        FetalRate = fetalHeartRate.FetalRate
                    };
                    context.FetalHeartRates.Add(existingInDb);
                }
                else
                {
                    existingInDb.FetalRate = fetalHeartRate.FetalRate;
                    context.Entry(existingInDb).State = EntityState.Modified;
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