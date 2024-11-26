using HotelService.Application.Contact.Dtos;
using HotelService.Infrastructure.Data.Entities;
using HotelService.Infrastructure.Data.Repositories;
using HotelService.Infrastructure.Models;
using MediatR;

namespace HotelService.Application.Hotel.Command
{
    public class CreateHotelCommand : IRequest<Response<Guid>>
    {
        public string FirmName { get; set; } = string.Empty;
        public string ResponsibleName { get; set; } = string.Empty;
        public string ResponsibleSurname { get; set; } = string.Empty;
        public List<ContactDto> Contacts { get; set; } = new List<ContactDto>();
    }

    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Response<Guid>>
    {
        private readonly IHotelRepository _repository;

        public CreateHotelCommandHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<Guid>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = new HotelEntity
            {
                FirmName = request.FirmName,
                ResponsibleName = request.ResponsibleName,
                ResponsibleSurname = request.ResponsibleSurname,
                Contacts = request.Contacts.Select(ContactDto.Map).ToList()
            };

            await _repository.AddHotelAsync(hotel);
            return Response<Guid>.Success(hotel.Id);
        }
    }
}
