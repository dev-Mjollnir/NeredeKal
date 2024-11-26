using FluentValidation;
using HotelService.Application.Contact.Command;

namespace HotelService.Application.Contact.Validator
{
    public class AddHotelContactCommandValidator : AbstractValidator<AddHotelContactCommand>
    {
        public AddHotelContactCommandValidator()
        {
            RuleFor(x => x.HotelId).NotEmpty();
            RuleFor(x => x.ContactInfoType).IsInEnum();
            RuleFor(x => x.ContactInfoContent).NotEmpty().MaximumLength(100);
        }
    }
}
