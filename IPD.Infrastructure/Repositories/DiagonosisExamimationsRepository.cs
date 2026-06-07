using IPD.Domain.Entities;
using IPD.Infrastructure.Contracts;
using IPD.Infrastructure.Sql;

namespace IPD.Infrastructure.Repositories
{
    public class DiagonosisExamimationsRepository : Repository<DiagnosisExamination>, IDiagonosisExamimationsRepository
    {
        private readonly DataContext context;

        public DiagonosisExamimationsRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public IList<DiagnosisExamination> GetAllDiagonosisExamination()
        {
            var list = (from d in context.DiagonosisExaminations

                        where d.IsRowDeleted.Equals(false)
                        select new DiagnosisExamination
                        {
                            DigonosisExaminationsName = d.DigonosisExaminationsName,
                        }).OrderByDescending(e => e.DateCreated)
                        .ToList();
            return list;
        }
    }
}