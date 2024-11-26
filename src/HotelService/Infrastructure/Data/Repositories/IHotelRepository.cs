﻿using HotelService.Infrastructure.Data.Entities;

namespace HotelService.Infrastructure.Data.Repositories
{
    public interface IHotelRepository
    {
        Task<IEnumerable<HotelEntity>> GetAllHotelsAsync(bool asNoTracking = true);
        Task<HotelEntity?> GetHotelByIdAsync(Guid id, bool asNoTracking = true);
        Task AddHotelAsync(HotelEntity hotel);
        Task DeleteHotelAsync(HotelEntity hotel);
        Task AddContactAsync(ContactEntity contact);
        Task DeleteContactAsync(ContactEntity contact);
    }
}
