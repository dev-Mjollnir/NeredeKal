using NeredeKal.Infrastructure.Constants;
using NeredeKal.Infrastructure.Constants.Enums;
using NeredeKal.Infrastructure.Infrastructure.Data.Entities;
using NeredeKal.Infrastructure.Infrastructure.Data.Repositories;
using NeredeKal.Infrastructure.Infrastructure.Models;
using MediatR;

namespace HotelService.Application.Contact.Command
{
    public class AddHotelContactCommand : IRequest<Response<Guid>>
    {
        public Guid HotelId { get; set; }
        public ContactInfoType ContactInfoType { get; set; }
        public string ContactInfoContent { get; set; } = string.Empty;
    }

    public class AddHotelContactCommandHandler : IRequestHandler<AddHotelContactCommand, Response<Guid>>
    {
        private readonly IHotelRepository _repository;

        public AddHotelContactCommandHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<Guid>> Handle(AddHotelContactCommand request, CancellationToken cancellationToken)
        {
            var hotels = await _repository.GetHotelByIdAsync(request.HotelId, false);
            if (hotels == null)
                return Response<Guid>.Fail(ErrorMessage.HotelNotFound);

            var contact = new ContactEntity
            {
                HotelId = hotels.Id,
                ContactInfoContent = request.ContactInfoContent,
                ContactInfoType = request.ContactInfoType
            };
            await _repository.AddContactAsync(contact);
            return Response<Guid>.Success(contact.Id);
        }
    }
}
