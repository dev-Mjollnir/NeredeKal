using HotelService.Application.Hotel.Dtos;
using NeredeKal.Infrastructure.Constants;
using NeredeKal.Infrastructure.Infrastructure.Data.Repositories;
using NeredeKal.Infrastructure.Infrastructure.Models;
using MediatR;

namespace HotelService.Application.Hotel.Query
{
    public class GetHotelsQuery : IRequest<Response<List<HotelDto>>>
    {

    }

    public class GetHotelsQueryHandler : IRequestHandler<GetHotelsQuery, Response<List<HotelDto>>>
    {
        private readonly IHotelRepository _repository;

        public GetHotelsQueryHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<HotelDto>>> Handle(GetHotelsQuery request, CancellationToken cancellationToken)
        {
            var hotels = await _repository.GetAllHotelsAsync();
            if (!hotels.Any())
                return Response<List<HotelDto>>.Fail(ErrorMessage.HotelsNotFound);
            return Response<List<HotelDto>>.Success(hotels.Select(HotelDto.Map).ToList());
        }
    }
}
