using HotelService.Application.Hotel.Dtos;
using NeredeKal.Infrastructure.Constants.Enums;
using NeredeKal.Infrastructure.Infrastructure.Data.Entities;


namespace HotelService.Test.Hotel
{
    public class HotelDtoTests
    {
        [Fact]
        public void Map_ValidHotelEntity_ReturnsMappedHotelDto()
        {
            // Arrange
            var hotelEntity = new HotelEntity
            {
                Id = Guid.NewGuid(),
                FirmName = "Test Hotel",
                ResponsibleName = "John",
                ResponsibleSurname = "Doe",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                ModifiedAt = DateTime.UtcNow.AddDays(-1),
                Contacts = new List<ContactEntity>
            {
                new ContactEntity
                {
                    Id = Guid.NewGuid(),
                    ContactInfoType = ContactInfoType.Sms,
                    ContactInfoContent = "123456789",
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                    ModifiedAt = DateTime.UtcNow.AddDays(-1)
                },
                new ContactEntity
                {
                    Id = Guid.NewGuid(),
                    ContactInfoType = ContactInfoType.Email,
                    ContactInfoContent = "test@example.com",
                    CreatedAt = DateTime.UtcNow.AddDays(-4),
                    ModifiedAt = DateTime.UtcNow.AddDays(-2)
                }
            }
            };

            // Act
            var result = HotelDto.Map(hotelEntity);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(hotelEntity.Id, result!.Id);
            Assert.Equal(hotelEntity.FirmName, result.FirmName);
            Assert.Equal(hotelEntity.ResponsibleName, result.ResponsibleName);
            Assert.Equal(hotelEntity.ResponsibleSurname, result.ResponsibleSurname);
            Assert.Equal(hotelEntity.CreatedAt, result.CreatedAt);
            Assert.Equal(hotelEntity.ModifiedAt, result.ModifiedAt);

            Assert.NotNull(result.Contacts);
            Assert.Equal(hotelEntity.Contacts.Count, result.Contacts.Count);
           
        
        }

        [Fact]
        public void Map_NullHotelEntity_ReturnsNull()
        {
            // Arrange
            HotelEntity? hotelEntity = null;

            // Assert
            Assert.Throws<NullReferenceException>(() => HotelDto.Map(hotelEntity));
        }
    }
}
