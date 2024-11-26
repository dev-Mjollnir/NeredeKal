using FluentValidation;
using FluentValidation.TestHelper;
using HotelService.Application.Contact.Dtos;
using HotelService.Application.Hotel.Command;
using HotelService.Application.Hotel.Validator;
using NeredeKal.Infrastructure.Constants.Enums;

namespace HotelService.Test.Hotel
{
    public class CreateHotelCommandValidatorTests
    {
        private readonly CreateHotelCommandValidator _validator;

        public CreateHotelCommandValidatorTests()
        {
            _validator = new CreateHotelCommandValidator();
        }

        [Fact]
        public void Validate_ValidCommand_ShouldPassValidation()
        {
            // Arrange
            var command = new CreateHotelCommand
            {
                FirmName = "Valid Firm Name",
                ResponsibleName = "John",
                ResponsibleSurname = "Doe",
                Contacts = new List<ContactDto>
            {
                new ContactDto
                {
                    ContactInfoType = ContactInfoType.Sms,
                    ContactInfoContent = "1234567890"
                },
                new ContactDto
                {
                    ContactInfoType = ContactInfoType.Email,
                    ContactInfoContent = "test@example.com"
                }
            }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Validate_EmptyFirmName_ShouldFailValidation()
        {
            // Arrange
            var command = new CreateHotelCommand
            {
                FirmName = "",
                ResponsibleName = "John",
                ResponsibleSurname = "Doe",
                Contacts = new List<ContactDto>
            {
                new ContactDto
                {
                    ContactInfoType = ContactInfoType.Sms,
                    ContactInfoContent = "1234567890"
                }
            }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FirmName);
        }

        [Fact]
        public void Validate_InvalidContactInfoType_ShouldFailValidation()
        {
            // Arrange
            var command = new CreateHotelCommand
            {
                FirmName = "Valid Firm Name",
                ResponsibleName = "John",
                ResponsibleSurname = "Doe",
                Contacts = new List<ContactDto>
            {
                new ContactDto
                {
                    ContactInfoType = (ContactInfoType)999, // Invalid enum value
                    ContactInfoContent = "1234567890"
                }
            }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor("Contacts[0].ContactInfoType");
        }

        [Fact]
        public void Validate_EmptyContactList_ShouldFailValidation()
        {
            // Arrange
            var command = new CreateHotelCommand
            {
                FirmName = "Valid Firm Name",
                ResponsibleName = "John",
                ResponsibleSurname = "Doe",
                Contacts = new List<ContactDto>() // Empty list
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Contacts);
        }

        [Fact]
        public void Validate_LongFirmName_ShouldFailValidation()
        {
            // Arrange
            var command = new CreateHotelCommand
            {
                FirmName = new string('A', 251), // Exceeds max length
                ResponsibleName = "John",
                ResponsibleSurname = "Doe",
                Contacts = new List<ContactDto>
            {
                new ContactDto
                {
                    ContactInfoType = ContactInfoType.Sms,
                    ContactInfoContent = "1234567890"
                }
            }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.FirmName);
        }

    }
}
