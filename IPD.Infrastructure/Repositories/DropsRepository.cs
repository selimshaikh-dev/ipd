using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class DropsRepository : Repository<Drop>, IDropsRepository
    {
        private readonly DataContext context;

        public DropsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Drop UpdateDrop(Drop drop)
        {
            try
            {
                var existingInDb = context.Drops
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(drop.PartographID) &&
                        i.DropsTime.Equals(drop.DropsTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new Drop()
                    {
                        PartographID = drop.PartographID,
                        DropsTime = drop.DropsTime,
                        DropsDetails = drop.DropsDetails
                    };
                    context.Drops.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.DropsDetails != drop.DropsDetails)
                    {
                        existingInDb.DropsDetails = drop.DropsDetails;
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