using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class DescentOfHeadsRepository : Repository<DescentOfHead>, IDescentOfHeadsRepository
    {
        private readonly DataContext context;

        public DescentOfHeadsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public DescentOfHead UpdateDescentOfHead(DescentOfHead descentOfHead)
        {
            try
            {
                var existingInDb = context.DescentOfHeads
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(descentOfHead.PartographID) &&
                        i.DescentOfHeadTime.Equals(descentOfHead.DescentOfHeadTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new DescentOfHead()
                    {
                        PartographID = descentOfHead.PartographID,
                        DescentOfHeadTime = descentOfHead.DescentOfHeadTime,
                        DescentOfHeadDetails = descentOfHead.DescentOfHeadDetails
                    };
                    context.DescentOfHeads.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.DescentOfHeadDetails != descentOfHead.DescentOfHeadDetails)
                    {

                        existingInDb.DescentOfHeadDetails = descentOfHead.DescentOfHeadDetails;
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