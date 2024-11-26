using HotelService.Application.Common.Dtos;
using HotelService.Application.Contact.Dtos;
using HotelService.Infrastructure.Data.Entities;

namespace HotelService.Application.Hotel.Dtos
{
    public record HotelDto : BaseDto
    {
        public string FirmName { get; set; } = string.Empty;
        public string ResponsibleName { get; set; } = string.Empty;
        public string ResponsibleSurname { get; set; } = string.Empty;
        public List<ContactDto> Contacts { get; set; } = new List<ContactDto>();

        public static HotelDto? Map(HotelEntity hotelEntity)
        {
            return new HotelDto
            {
                Id = hotelEntity.Id,
                FirmName = hotelEntity.FirmName,
                ResponsibleName = hotelEntity.ResponsibleName,
                ResponsibleSurname = hotelEntity.ResponsibleSurname,
                Contacts = hotelEntity.Contacts.Select(ContactDto.Map).ToList(),
                CreatedAt = hotelEntity.CreatedAt,
                ModifiedAt = hotelEntity.ModifiedAt,
            };
        }
    }
}
