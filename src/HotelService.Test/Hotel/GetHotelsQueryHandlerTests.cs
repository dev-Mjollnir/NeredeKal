using HotelService.Application.Hotel.Query;
using Moq;
using NeredeKal.Infrastructure.Constants;
using NeredeKal.Infrastructure.Constants.Enums;
using NeredeKal.Infrastructure.Infrastructure.Data.Entities;
using NeredeKal.Infrastructure.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelService.Test.Hotel
{
    public class GetHotelsQueryHandlerTests
    {
        private readonly Mock<IHotelRepository> _repositoryMock;
        private readonly GetHotelsQueryHandler _handler;

        public GetHotelsQueryHandlerTests()
        {
            _repositoryMock = new Mock<IHotelRepository>();
            _handler = new GetHotelsQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_HotelsExist_ReturnsSuccessResponseWithHotelList()
        {
            // Arrange
            var hotelEntities = new List<HotelEntity>
        {
            new HotelEntity
            {
                Id = Guid.NewGuid(),
                FirmName = "Test Hotel 1",
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
            },
            new HotelEntity
            {
                Id = Guid.NewGuid(),
                FirmName = "Test Hotel 2",
                ResponsibleName = "Jane",
                ResponsibleSurname = "Smith",
                CreatedAt = DateTime.UtcNow.AddDays(-6),
                ModifiedAt = DateTime.UtcNow.AddDays(-2),
                Contacts = new List<ContactEntity>()
            }
        };

            _repositoryMock
                .Setup(repo => repo.GetAllHotelsAsync(true))
                .ReturnsAsync(hotelEntities);

            var query = new GetHotelsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data!.Count);

            _repositoryMock.Verify(repo => repo.GetAllHotelsAsync(true), Times.Once);
        }

        [Fact]
        public async Task Handle_NoHotelsExist_ReturnsFailResponse()
        {
            // Arrange
            _repositoryMock
                .Setup(repo => repo.GetAllHotelsAsync(true))
                .ReturnsAsync(new List<HotelEntity>());

            var query = new GetHotelsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Succeeded);
            Assert.Equal(ErrorMessage.HotelsNotFound, result.Message);

            _repositoryMock.Verify(repo => repo.GetAllHotelsAsync(true), Times.Once);
        }
    }
}
