using IPD.Domain.Entities;

namespace IPD.Infrastructure.Contracts
{
    public interface IDiagonosisExamimationsRepository : IRepository<DiagnosisExamination>
    {
        IList<DiagnosisExamination> GetAllDiagonosisExamination();
    }
}