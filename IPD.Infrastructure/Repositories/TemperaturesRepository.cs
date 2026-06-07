using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class TemperaturesRepository : Repository<Temperature>, ITemperaturesRepository
    {
        private readonly DataContext context;

        public TemperaturesRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Temperature UpdateTemperature(Temperature temperature)
        {
            try
            {
                var existingInDb = context.Temperatures
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(temperature.PartographID) &&
                        i.TemperatureTime.Equals(temperature.TemperatureTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new Temperature()
                    {
                        PartographID = temperature.PartographID,
                        TemperaturesDetails = temperature.TemperaturesDetails,
                        TemperatureTime = temperature.TemperatureTime
                    };
                    context.Temperatures.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.TemperaturesDetails != temperature.TemperaturesDetails)
                    {
                        existingInDb.TemperaturesDetails = temperature.TemperaturesDetails;
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