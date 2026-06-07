using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class VolumesRepository : Repository<Volume>, IVolumesRepository
    {
        private readonly DataContext context;

        public VolumesRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Volume UpdateVolume(Volume volume)
        {
            try
            {
                var existingInDb = context.volumes
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(volume.PartographID) &&
                        i.VolumesTime.Equals(volume.VolumesTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new Volume()
                    {
                        PartographID = volume.PartographID,
                        VolumesDetails = volume.VolumesDetails,
                        VolumesTime = volume.VolumesTime
                    };
                    context.volumes.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.VolumesDetails != volume.VolumesDetails)
                    {
                        existingInDb.VolumesDetails = volume.VolumesDetails;
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