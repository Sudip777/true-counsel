using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Lawyer.Validators
{
    public class RegisterLawyerCommandValidatorTests
    {
        private readonly RegisterLawyerCommandValidator _validator;

        public RegisterLawyerCommandValidatorTests()
        {
            _validator = new RegisterLawyerCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_UserId_Is_Invalid()
        {
            var command = new RegisterLawyerCommand { UserId = 0, BarNumber = "BAR123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.UserId);
        }

        [Fact]
        public void Should_Have_Error_When_BarNumber_Is_Empty()
        {
            var command = new RegisterLawyerCommand { BarNumber = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.BarNumber);
        }

        [Fact]
        public void Should_Have_Error_When_HourlyRate_Is_Negative()
        {
            var command = new RegisterLawyerCommand { HourlyRate = -10, BarNumber = "BAR123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.HourlyRate);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new RegisterLawyerCommand 
            { 
                UserId = 1, 
                BarNumber = "BAR12345",
                HourlyRate = 150.00m,
                YearsOfExperience = 5,
                Specialization = "Criminal Law",
                Bio = "Experienced criminal lawyer."
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
