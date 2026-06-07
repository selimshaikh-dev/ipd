using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class LanguageRepository : Repository<Language>, ILanguageRepository
    {
        private readonly DataContext context;

        public LanguageRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<Language> GetAllLanguage()
        {
            var list = (from l in context.Language

                        where l.IsRowDeleted.Equals(false)
                        select new Language
                        {
                            LanguageName = l.LanguageName,
                        }).ToList();
            return list;
        }
    }
}