using IPD.Domain.Dto;
using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;
using Microsoft.EntityFrameworkCore;

namespace IPD.Infrastructure.Repositories
{
    public class ChiefdomsRepository : Repository<Chiefdom>, IChiefdomsRepository
    {
        private readonly DataContext context;

        public ChiefdomsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<ChiefdomDto> GetChiefdomList()
        {
            var list = (from a in context.Chiefdoms
                        where a.IsRowDeleted.Equals(false)
                        select new ChiefdomDto
                        {
                            Name = a.Name
                        }).ToList();
            return list;
        }

        public async Task<IList<ChiefdomDto>> GetChiefdomListAsync(int inkundlaId)
        {
            var list = await context.Chiefdoms
                        .Where(i => i.TinkhundlaID.Equals(inkundlaId))
                        .AsNoTracking()
                        .Select(i => new ChiefdomDto()
                        {
                            ChiefdomID = i.ChiefdomID,
                            TinkhundlaID = i.TinkhundlaID,
                            Name = i.Name,
                        })
                        .ToListAsync();
            return list;
        }
    }
}