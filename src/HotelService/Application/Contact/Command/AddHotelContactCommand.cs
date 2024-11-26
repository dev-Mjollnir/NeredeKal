using HotelService.Constants.Enums;
using HotelService.Infrastructure.Data.Entities;
using HotelService.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.Application.Contact.Command
{
    public class AddHotelContactCommand : IRequest<Guid>
    {
        [FromRoute]
        public Guid HotelId { get; set; }
        public ContactInfoType ContactInfoType { get; set; }
        public string ContactInfoContent { get; set; } = string.Empty;
    }

    public class AddHotelContactCommandHandler : IRequestHandler<AddHotelContactCommand, Guid>
    {
        private readonly IHotelRepository _repository;

        public AddHotelContactCommandHandler(IHotelRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(AddHotelContactCommand request, CancellationToken cancellationToken)
        {
            var hotels = await _repository.GetHotelByIdAsync(request.HotelId, false);
            if (hotels == null)
                return Guid.Empty;

            var contact = new ContactEntity
            {
                HotelId = hotels.Id,
                ContactInfoContent = request.ContactInfoContent,
                ContactInfoType = request.ContactInfoType
            };
            await _repository.AddContactAsync(contact);
            return contact.Id;
        }
    }
}
