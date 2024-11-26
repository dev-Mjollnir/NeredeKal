using NeredeKal.Infrastructure.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace NeredeKal.Infrastructure.Infrastructure.Data.Context
{
    public interface IHotelDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
        DbSet<HotelEntity> Hotels { get; set; }
        DbSet<ContactEntity> Contacts { get; set; }
        DbSet<ReportEntity> Reports { get; set; }
    }
}