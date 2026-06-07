using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class MouldingsRepository : Repository<Moulding>, IMouldingsRepository
    {
        private readonly DataContext context;

        public MouldingsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Moulding UpdateMoulding(Moulding moulding)
        {
            try
            {
                var existingInDb = context.Mouldings
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(moulding.PartographID) &&
                        i.MouldingTime.Equals(moulding.MouldingTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new Moulding()
                    {
                        PartographID = moulding.PartographID,
                        MouldingTime = moulding.MouldingTime,
                        MouldingDetails = moulding.MouldingDetails
                    };
                    context.Mouldings.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.MouldingDetails != moulding.MouldingDetails)
                    {
                        existingInDb.MouldingDetails = moulding.MouldingDetails;
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