using FluentValidation;
using HotelService.Application.Hotel.Command;

namespace HotelService.Application.Hotel.Validator
{
    public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
    {
        public CreateHotelCommandValidator()
        {
            RuleFor(x => x.FirmName).NotEmpty().MaximumLength(250);
            RuleFor(x => x.ResponsibleName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ResponsibleSurname).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Contacts).NotEmpty();
            RuleForEach(x => x.Contacts).ChildRules(contact =>
            {
                contact.RuleFor(x => x.ContactInfoType).IsInEnum();
                contact.RuleFor(x => x.ContactInfoContent).NotEmpty().MaximumLength(100);
            });
        }
    }
}
