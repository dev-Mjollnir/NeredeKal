using HotelService.Application.Hotel.Dtos;
using HotelService.Infrastructure.Data.Repositories;
using MediatR;

namespace HotelService.Application.Hotel.Query
{
    public class GetHotelsQuery : IRequest<List<HotelDto>>
    {
        
    }

    public class GetHotelsQueryHandler : IRequestHandler<GetHotelsQuery, List<HotelDto>>
    {
        private readonly IHotelRepository _repository;

        public GetHotelsQueryHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<HotelDto>> Handle(GetHotelsQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _repository.GetAllHotelsAsync();
            if (hotels == null)
                return new List<HotelDto>();
            return hotels.Select(HotelDto.Map).ToList();
        }
    }
}
