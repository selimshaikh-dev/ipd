using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class AcetonesRepository : Repository<Acetone>, IAcetonesRepository
    {
        private readonly DataContext context;

        public AcetonesRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Acetone UpdateAcetone(Acetone acetone)
        {
            try
            {
                var existingInDb = context.Acetones
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(acetone.PartographID) &&
                        i.AcetoneTime.Equals(acetone.AcetoneTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new Acetone()
                    {
                        PartographID = acetone.PartographID,
                        AcetoneTime = acetone.AcetoneTime,
                        AcetonesDetails = acetone.AcetonesDetails
                    };
                    context.Acetones.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.AcetonesDetails != acetone.AcetonesDetails)
                    {
                        existingInDb.AcetonesDetails = acetone.AcetonesDetails;
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