using HotelService.Application.Hotel.Command;
using Moq;
using NeredeKal.Infrastructure.Constants;
using NeredeKal.Infrastructure.Infrastructure.Data.Entities;
using NeredeKal.Infrastructure.Infrastructure.Data.Repositories;

namespace HotelService.Test.Hotel
{
    public class DeleteHotelCommandHandlerTests
    {
        private readonly Mock<IHotelRepository> _mockRepository;
        private readonly DeleteHotelCommandHandler _handler;

        public DeleteHotelCommandHandlerTests()
        {
            _mockRepository = new Mock<IHotelRepository>();
            _handler = new DeleteHotelCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_HotelExists_ReturnsSuccessResponse()
        {
            // Arrange
            var hotelId = Guid.NewGuid();
            var hotel = new HotelEntity { Id = hotelId, FirmName = "", ResponsibleName = "", ResponsibleSurname = "" };

            _mockRepository
                .Setup(repo => repo.GetHotelByIdAsync(hotelId, false))
                .ReturnsAsync(hotel);

            _mockRepository
                .Setup(repo => repo.DeleteHotelAsync(hotel))
                .Returns(Task.CompletedTask);

            var command = new DeleteHotelCommand { Id = hotelId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Succeeded);
            Assert.True(result.Data);

            _mockRepository.Verify(repo => repo.GetHotelByIdAsync(hotelId, false), Times.Once);
            _mockRepository.Verify(repo => repo.DeleteHotelAsync(hotel), Times.Once);
        }

        [Fact]
        public async Task Handle_HotelDoesNotExist_ReturnsFailResponse()
        {
            // Arrange
            var hotelId = Guid.NewGuid();

            _mockRepository
                .Setup(repo => repo.GetHotelByIdAsync(hotelId, false))
                .ReturnsAsync((HotelEntity)null);

            var command = new DeleteHotelCommand { Id = hotelId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal(ErrorMessage.HotelNotFound, result.Message);

            _mockRepository.Verify(repo => repo.GetHotelByIdAsync(hotelId, false), Times.Once);
            _mockRepository.Verify(repo => repo.DeleteHotelAsync(It.IsAny<HotelEntity>()), Times.Never);
        }

    }
}
