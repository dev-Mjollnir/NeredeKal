using HotelService.Application.Hotel.Query;
using Moq;
using NeredeKal.Infrastructure.Constants;
using NeredeKal.Infrastructure.Constants.Enums;
using NeredeKal.Infrastructure.Infrastructure.Data.Entities;
using NeredeKal.Infrastructure.Infrastructure.Data.Repositories;

namespace HotelService.Test.Hotel
{
    public class GetHotelByIdQueryHandlerTests
    {
        private readonly Mock<IHotelRepository> _repositoryMock;
        private readonly GetHotelByIdQueryHandler _handler;

        public GetHotelByIdQueryHandlerTests()
        {
            _repositoryMock = new Mock<IHotelRepository>();
            _handler = new GetHotelByIdQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidHotelId_ReturnsSuccessResponseWithHotelDto()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var hotelEntity = new HotelEntity
            {
                Id = hotelId,
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
                }
            }
            };

            _repositoryMock
                .Setup(repo => repo.GetHotelByIdAsync(hotelId, true))
                .ReturnsAsync(hotelEntity);

            var query = new GetHotelByIdQuery { Id = hotelId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Data);
            Assert.Equal(hotelId, result.Data!.Id);
            Assert.Equal(hotelEntity.FirmName, result.Data.FirmName);
            Assert.Equal(hotelEntity.ResponsibleName, result.Data.ResponsibleName);
            Assert.Equal(hotelEntity.ResponsibleSurname, result.Data.ResponsibleSurname);

            _repositoryMock.Verify(repo => repo.GetHotelByIdAsync(hotelId, true), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidHotelId_ReturnsFailResponse()
        {
            // Arrange
            var hotelId = Guid.NewGuid();

            _repositoryMock
                .Setup(repo => repo.GetHotelByIdAsync(hotelId, true))
                .ReturnsAsync((HotelEntity?)null);

            var query = new GetHotelByIdQuery { Id = hotelId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeeded);
            Assert.Equal(ErrorMessage.HotelNotFound, result.Message);

            _repositoryMock.Verify(repo => repo.GetHotelByIdAsync(hotelId, true), Times.Once);
        }
    }
}
