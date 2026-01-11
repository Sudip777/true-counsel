using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.Court.Commands;
using TrueCounsel.Application.Features.Court.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Court.Validators
{
    public class CreateCourtCommandValidatorTests
    {
        private readonly CreateCourtCommandValidator _validator;

        public CreateCourtCommandValidatorTests()
        {
            _validator = new CreateCourtCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new CreateCourtCommand { Name = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Name);
        }

        [Fact]
        public void Should_Have_Error_When_CourtTypeId_Is_Empty()
        {
            var command = new CreateCourtCommand { CourtTypeId = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.CourtTypeId);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new CreateCourtCommand 
            { 
                Name = "District Court", 
                CourtTypeId = 1,
                City = "Kathmandu",
                State = "Bagmati"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
