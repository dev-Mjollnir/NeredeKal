using HotelService.Infrastructure.Data.Entities;
using HotelService.Infrastructure.Data.Repositories;
using MediatR;

namespace HotelService.Application.Hotel.Command
{
    public class CreateHotelCommand : IRequest<Guid>
    {
        public string FirmName { get; set; } = string.Empty;
        public string ResponsibleName { get; set; } = string.Empty;
        public string ResponsibleSurname { get; set; } = string.Empty;
        public IEnumerable<ContactEntity> Contacts { get; set; } = new List<ContactEntity>();
    }

    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Guid>
    {
        private readonly IHotelRepository _repository;

        public CreateHotelCommandHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var hotel = new HotelEntity
            {
                FirmName = request.FirmName,
                ResponsibleName = request.ResponsibleName,
                ResponsibleSurname = request.ResponsibleSurname,
                Contacts = request.Contacts
            };

            await _repository.AddHotelAsync(hotel);
            return hotel.Id;
        }
    }
}
