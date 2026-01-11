using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.Client.Commands;
using TrueCounsel.Application.Features.Client.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Client.Validators
{
    public class UpdateClientCommandValidatorTests
    {
        private readonly UpdateClientCommandValidator _validator;

        public UpdateClientCommandValidatorTests()
        {
            _validator = new UpdateClientCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new UpdateClientCommand { Id = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Id);
        }

        [Fact]
        public void Should_Have_Error_When_Phone_Is_Empty()
        {
            var command = new UpdateClientCommand { Phone = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Phone);
        }

        [Fact]
        public void Should_Have_Error_When_Phone_Format_Is_Invalid()
        {
            var command = new UpdateClientCommand { Phone = "invalid" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Phone);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new UpdateClientCommand 
            { 
                Id = 1, 
                Phone = "9841234567",
                Address = "Kathmandu",
                Occupation = "Engineer",
                EmergencyContact = "Jane Doe"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
