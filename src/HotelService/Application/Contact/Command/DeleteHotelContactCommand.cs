using HotelService.Infrastructure.Data.Repositories;
using MediatR;

namespace HotelService.Application.Contact.Command
{
    public class DeleteHotelContactCommand : IRequest<bool>
    {
        public Guid HotelId { get; set; }
        public Guid ContactId { get; set; }
    }

    public class DeleteHotelContactCommandHandler : IRequestHandler<DeleteHotelContactCommand, bool>
    {
        private readonly IHotelRepository _repository;

        public DeleteHotelContactCommandHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteHotelContactCommand request, CancellationToken cancellationToken)
        {
            var hotels = await _repository.GetHotelByIdAsync(request.HotelId, false);
            if (hotels == null)
                return false;

            var contact = hotels.Contacts.FirstOrDefault(contact => contact.Id == request.ContactId);
            if (contact == null)
                return false;

            await _repository.DeleteContactAsync(contact);
            return true;
        }
    }
}
