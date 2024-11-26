using NeredeKal.Infrastructure.Infrastructure.Data.Entities;

namespace NeredeKal.Infrastructure.Infrastructure.Data.Repositories
{
    public interface IReportRepository
    {
        Task AddReportAsync(ReportEntity entity);
        Task<ReportEntity> GetReportByIdAsync(Guid id, bool asNoTracking = true);
        Task<List<ReportEntity>> GetReportsAsync(bool asNoTracking = true);
    }
}