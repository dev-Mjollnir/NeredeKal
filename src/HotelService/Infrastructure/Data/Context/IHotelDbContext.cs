using HotelService.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelService.Infrastructure.Data.Context
{
    public interface IHotelDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<HotelEntity> Hotels { get; set; }
        DbSet<ContactEntity> Contacts { get; set; }
    }
}