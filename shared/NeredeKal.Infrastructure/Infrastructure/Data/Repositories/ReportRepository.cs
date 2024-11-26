using Microsoft.EntityFrameworkCore;
using NeredeKal.Infrastructure.Infrastructure.Data.Context;
using NeredeKal.Infrastructure.Infrastructure.Data.Entities;

namespace NeredeKal.Infrastructure.Infrastructure.Data.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IHotelDbContext _context;

        public ReportRepository(IHotelDbContext context)
        {
            _context = context;
        }

        public async Task AddReportAsync(ReportEntity entity)
        {
            _context.Reports.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ReportEntity> GetReportByIdAsync(Guid id, bool asNoTracking = true)
        {
            var query = _context.Reports.AsQueryable();
            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<ReportEntity>> GetReportsAsync(bool asNoTracking = true)
        {
            var query = _context.Reports.AsQueryable();
            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }
    }
}
