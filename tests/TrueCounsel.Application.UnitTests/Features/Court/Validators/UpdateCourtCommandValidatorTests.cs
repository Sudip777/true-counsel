using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.Court.Commands;
using TrueCounsel.Application.Features.Court.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Court.Validators
{
    public class UpdateCourtCommandValidatorTests
    {
        private readonly UpdateCourtCommandValidator _validator;

        public UpdateCourtCommandValidatorTests()
        {
            _validator = new UpdateCourtCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new UpdateCourtCommand { Id = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Id);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new UpdateCourtCommand { Name = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Name);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new UpdateCourtCommand 
            { 
                Id = 1, 
                Name = "District Court Updated",
                City = "Lalitpur"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
