using System;
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
        public void Should_Have_Error_When_UserId_Is_Zero()
        {
            var command = new RegisterLawyerCommand { UserId = 0, BarNumber = "BAR123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }

        [Fact]
        public void Should_Have_Error_When_BarNumber_Is_Empty()
        {
            var command = new RegisterLawyerCommand { BarNumber = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.BarNumber);
        }

        [Fact]
        public void Should_Have_Error_When_YearsOfExperience_Is_Negative()
        {
            var command = new RegisterLawyerCommand { YearsOfExperience = -1, BarNumber = "BAR123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.YearsOfExperience);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new RegisterLawyerCommand
            {
                UserId = 1,
                BarNumber = "BAR999",
                Specialization = "Family Law",
                YearsOfExperience = 10
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
