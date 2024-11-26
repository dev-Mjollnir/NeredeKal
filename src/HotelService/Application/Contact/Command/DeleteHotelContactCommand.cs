using HotelService.Constants;
using HotelService.Infrastructure.Data.Repositories;
using HotelService.Infrastructure.Models;
using MediatR;

namespace HotelService.Application.Contact.Command
{
    public class DeleteHotelContactCommand : IRequest<Response<bool>>
    {
        public Guid HotelId { get; set; }
        public Guid ContactId { get; set; }
    }

    public class DeleteHotelContactCommandHandler : IRequestHandler<DeleteHotelContactCommand, Response<bool>>
    {
        private readonly IHotelRepository _repository;

        public DeleteHotelContactCommandHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<bool>> Handle(DeleteHotelContactCommand request, CancellationToken cancellationToken)
        {
            var hotels = await _repository.GetHotelByIdAsync(request.HotelId, false);
            if (hotels == null)
                return Response<bool>.Fail(ErrorMessage.HotelNotFound);

            var contact = hotels.Contacts.FirstOrDefault(contact => contact.Id == request.ContactId);
            if (contact == null)
                return Response<bool>.Fail(ErrorMessage.ContactNotFound);

            await _repository.DeleteContactAsync(contact);
            return Response<bool>.Success(true);
        }
    }
}
