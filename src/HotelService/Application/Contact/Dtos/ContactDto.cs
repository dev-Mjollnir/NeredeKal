using HotelService.Application.Common.Dtos;
using HotelService.Application.Hotel.Dtos;
using HotelService.Constants.Enums;
using HotelService.Infrastructure.Data.Entities;

namespace HotelService.Application.Contact.Dtos
{
    public record ContactDto : BaseDto
    {
        public Guid HotelId { get; set; }
        public ContactInfoType ContactInfoType { get; set; } = ContactInfoType.Sms;
        public string ContactInfoContent { get; set; } = string.Empty;

        public static ContactDto Map(ContactEntity contactEntity)
        {
            if (contactEntity is null)
                return default;

            return new ContactDto
            {
                Id = contactEntity.Id,
                HotelId = contactEntity.HotelId,
                ContactInfoType = contactEntity.ContactInfoType,
                ContactInfoContent = contactEntity.ContactInfoContent,
                CreatedAt = contactEntity.CreatedAt,
                ModifiedAt = contactEntity.ModifiedAt,
            };
        }

        public static ContactEntity Map(ContactDto contactDto)
        {
            if (contactDto is null)
                return default;

            return new ContactEntity
            {
                ContactInfoType = contactDto.ContactInfoType,
                ContactInfoContent = contactDto.ContactInfoContent,
            };
        }
    }
}
