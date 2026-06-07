using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class PulseRepository : Repository<Pulse>, IPulseRepository
    {
        private readonly DataContext context;

        public PulseRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Pulse UpdatePulse(Pulse pulse)
        {
            try
            {
                var existingInDb = context.Pulses
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(pulse.PartographID) &&
                        i.PulseTime.Equals(pulse.PulseTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new Pulse()
                    {
                        PartographID = pulse.PartographID,
                        PulseDetails = pulse.PulseDetails,
                        PulseTime = pulse.PulseTime,
                    };
                    context.Pulses.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.PulseDetails != pulse.PulseDetails)
                    {
                        existingInDb.PulseDetails = pulse.PulseDetails;
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