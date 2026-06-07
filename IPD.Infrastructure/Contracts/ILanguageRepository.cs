using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface ILanguageRepository : IRepository<Language>
    {
        IList<Language> GetAllLanguage();
    }
}