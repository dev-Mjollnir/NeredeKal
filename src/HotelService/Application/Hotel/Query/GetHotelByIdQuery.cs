using HotelService.Application.Hotel.Dtos;
using HotelService.Infrastructure.Data.Repositories;
using MediatR;

namespace HotelService.Application.Hotel.Query
{
    public class GetHotelByIdQuery : IRequest<HotelDto>
    {
        public Guid Id { get; set; }
    }

    public class GetHotelByIdQueryHandler : IRequestHandler<GetHotelByIdQuery, HotelDto>
    {
        private readonly IHotelRepository _repository;

        public GetHotelByIdQueryHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<HotelDto> Handle(GetHotelByIdQuery request, CancellationToken cancellationToken)
        {
            var hotel = await _repository.GetHotelByIdAsync(request.Id);
            return HotelDto.Map(hotel);
        }
    }
}
