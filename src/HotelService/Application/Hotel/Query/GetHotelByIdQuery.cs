using HotelService.Application.Hotel.Dtos;
using HotelService.Constants;
using HotelService.Infrastructure.Data.Repositories;
using HotelService.Infrastructure.Models;
using MediatR;

namespace HotelService.Application.Hotel.Query
{
    public class GetHotelByIdQuery : IRequest<Response<HotelDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, Response<HotelDto>>
    {
        private readonly IHotelRepository _repository;

        public GetHotelByIdQueryHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<HotelDto>> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _repository.GetHotelByIdAsync(request.Id);
            if(hotel is null)
                return Response<HotelDto>.Fail(ErrorMessage.HotelNotFound);
            return Response<HotelDto>.Success(HotelDto.Map(hotel));
        }
    }
}
