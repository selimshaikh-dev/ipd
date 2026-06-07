using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class ProteinsRepository : Repository<Protein>, IProteinsRepository
    {
        private readonly DataContext context;

        public ProteinsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Protein UpdateProtein(Protein protein)
        {
            try
            {
                var existingInDb = context.Proteins
                    .FirstOrDefault(i =>
                        i.PartographID.Equals(protein.PartographID) &&
                        i.ProteinsTime.Equals(protein.ProteinsTime)
                    );

                if (existingInDb == null)
                {
                    existingInDb = new Protein()
                    {
                        PartographID = protein.PartographID,
                        ProteinsDetails = protein.ProteinsDetails,
                        ProteinsTime = protein.ProteinsTime
                    };
                    context.Proteins.Add(existingInDb);
                }
                else
                {
                    if (existingInDb.ProteinsDetails != protein.ProteinsDetails)
                    {

                        existingInDb.ProteinsDetails = protein.ProteinsDetails;
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