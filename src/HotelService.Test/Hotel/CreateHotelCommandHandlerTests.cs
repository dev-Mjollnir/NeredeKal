using Moq;
using Xunit;
using HotelService.Application.Hotel.Command;
using NeredeKal.Infrastructure.Infrastructure.Data.Repositories;
using HotelService.Application.Contact.Dtos;
using NeredeKal.Infrastructure.Constants.Enums;
using NeredeKal.Infrastructure.Infrastructure.Data.Entities;


namespace HotelService.Test.Hotel
{
 
    public class CreateHotelCommandHandlerTests
    {
        private readonly Mock<IHotelRepository> _mockRepository;
        private readonly CreateHotelCommandHandler _handler;

        public CreateHotelCommandHandlerTests()
        {
            _mockRepository = new Mock<IHotelRepository>();
            _handler = new CreateHotelCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ShouldAddHotelAndReturnSuccessResponse()
        {
            // Arrange
            var command = new CreateHotelCommand
            {
                FirmName = "Test Firm",
                ResponsibleName = "John",
                ResponsibleSurname = "Doe",
                Contacts = new List<ContactDto>
            {
                new ContactDto { ContactInfoType = ContactInfoType.Sms, ContactInfoContent = "123456789" },
                new ContactDto { ContactInfoType = ContactInfoType.Location, ContactInfoContent = "test@example.com" }
            }
            };

            _mockRepository
                .Setup(repo => repo.AddHotelAsync(It.IsAny<HotelEntity>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response.Succeeded);
            _mockRepository.Verify(repo => repo.AddHotelAsync(It.IsAny<HotelEntity>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NullContacts_ShouldStillAddHotelAndReturnSuccessResponse()
        {
            // Arrange
            var command = new CreateHotelCommand
            {
                FirmName = "Test Firm",
                ResponsibleName = "John",
                ResponsibleSurname = "Doe",
                Contacts = new List<ContactDto>()
            };

            _mockRepository
                .Setup(repo => repo.AddHotelAsync(It.IsAny<HotelEntity>()))
                .Returns(Task.CompletedTask);

            // Act
            var response = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(response.Succeeded);
            _mockRepository.Verify(repo => repo.AddHotelAsync(It.Is<HotelEntity>(
                hotel => hotel.FirmName == "Test Firm" &&
                         hotel.ResponsibleName == "John" &&
                         hotel.ResponsibleSurname == "Doe" &&
                         hotel.Contacts.Count == 0)), Times.Once);
        }
    }

}
