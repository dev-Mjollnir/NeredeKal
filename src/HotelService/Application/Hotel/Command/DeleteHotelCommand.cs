using HotelService.Application.Hotel.Dtos;
using HotelService.Application.Hotel.Query;
using HotelService.Infrastructure.Data.Repositories;
using MediatR;

namespace HotelService.Application.Hotel.Command
{
    public class DeleteHotelCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, bool>
    {
        private readonly IHotelRepository _repository;

        public DeleteHotelCommandHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _repository.GetHotelByIdAsync(request.Id, false);
            if (hotel == null)
                return false;
            await _repository.DeleteHotelAsync(hotel);
            return true;
        }
    }
}
