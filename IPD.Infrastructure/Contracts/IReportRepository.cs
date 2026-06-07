using IPD.Domain.Dto;

namespace IPD.Infrastructure.Contracts
{
    public interface IReportRepository:IRepository<ReportDto>
    {
        Task<IEnumerable<ReportDto>> GetAllLoadReports( DateTimeDto dateTimeDto);
    }
}
