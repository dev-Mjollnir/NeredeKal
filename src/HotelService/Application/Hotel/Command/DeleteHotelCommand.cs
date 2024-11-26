using HotelService.Constants;
using HotelService.Infrastructure.Data.Repositories;
using HotelService.Infrastructure.Models;
using MediatR;

namespace HotelService.Application.Hotel.Command
{
    public class DeleteHotelCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, Response<bool>>
    {
        private readonly IHotelRepository _repository;

        public DeleteHotelCommandHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<bool>> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = await _repository.GetHotelByIdAsync(request.Id, false);
            if (hotel == null)
                return Response<bool>.Fail(ErrorMessage.HotelNotFound);
            await _repository.DeleteHotelAsync(hotel);
            return Response<bool>.Success(true);
        }
    }
}
