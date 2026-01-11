using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Lawyer.Validators
{
    public class UpdateLawyerCommandValidatorTests
    {
        private readonly UpdateLawyerCommandValidator _validator;

        public UpdateLawyerCommandValidatorTests()
        {
            _validator = new UpdateLawyerCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Invalid()
        {
            var command = new UpdateLawyerCommand { Id = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Id);
        }

        [Fact]
        public void Should_Have_Error_When_HourlyRate_Is_Negative()
        {
            var command = new UpdateLawyerCommand { HourlyRate = -50 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.HourlyRate);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new UpdateLawyerCommand 
            { 
                Id = 1, 
                HourlyRate = 200.00m,
                YearsOfExperience = 10,
                Specialization = "Family Law",
                Bio = "Experienced family lawyer."
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
