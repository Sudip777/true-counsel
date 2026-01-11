using System;
using FluentValidation.TestHelper;

using TrueCounsel.Application.Features.Client.Commands;
using TrueCounsel.Application.Features.Client.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Client.Validators
{
    public class CreateClientCommandValidatorTests
    {
        private readonly CreateClientCommandValidator _validator;

        public CreateClientCommandValidatorTests()
        {
            _validator = new CreateClientCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_UserId_Is_Zero()
        {
            var command = new CreateClientCommand { UserId = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.UserId);
        }

        [Fact]
        public void Should_Have_Error_When_Phone_Is_Empty()
        {
            var command = new CreateClientCommand { Phone = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        [Fact]
        public void Should_Have_Error_When_Phone_Format_Is_Invalid()
        {
            var command = new CreateClientCommand { Phone = "abc" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Phone);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new CreateClientCommand
            {
                UserId = 1,
                Phone = "+1234567890",
                Address = "Valid Address",
                DateOfBirth = DateTime.UtcNow.AddYears(-25)
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void Should_Have_Error_When_DateOfBirth_Is_In_Future()
        {
            var command = new CreateClientCommand { DateOfBirth = DateTime.UtcNow.AddDays(1) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
        }
    }
}
