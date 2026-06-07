using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class PartographRepository : Repository<Partograph>, IPartographRepository
    {
        private readonly DataContext context;

        public PartographRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Guid> GetPartographIdByAdmissionId(Guid aid)
        {
            var Partograph = await context.Partograph.Where(e=>e.AdmissionID == aid).FirstOrDefaultAsync();
            if(Partograph == null)
            {
                Partograph = new Partograph();
            }
            return Partograph.PartographID;
        }
        public async Task<string> GetPartographByAdmissionId(Guid aid)
        {
            var Partograph = await context.Partograph.Where(e=>e.AdmissionID == aid).FirstOrDefaultAsync();
            if(Partograph == null)
            {   
                Partograph= new Partograph();       
            }
            return Partograph.InitiateTime;
        }

    }
}