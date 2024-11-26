using NeredeKal.Infrastructure.Infrastructure.Data.Context;
using NeredeKal.Infrastructure.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace NeredeKal.Infrastructure.Infrastructure.Data.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly IHotelDbContext _context;

        public HotelRepository(IHotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HotelEntity>> GetAllHotelsAsync(bool asNoTracking = true)
        {
            var query = _context.Hotels.Include(h => h.Contacts).AsQueryable();
            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }
        public async Task<HotelEntity?> GetHotelByIdAsync(Guid id, bool asNoTracking = true)
        {
            var query = _context.Hotels.Include(h => h.Contacts).AsQueryable();
            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }
        public async Task AddHotelAsync(HotelEntity hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteHotelAsync(HotelEntity hotel)
        {
            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
        public async Task AddContactAsync(ContactEntity contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteContactAsync(ContactEntity contact)
        {
            _context.Contacts.Remove(contact);
             await _context.SaveChangesAsync();
        }
    }
}
