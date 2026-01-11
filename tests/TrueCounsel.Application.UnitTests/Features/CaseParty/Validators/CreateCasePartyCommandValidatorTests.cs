using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.CaseParty.Commands;
using TrueCounsel.Application.Features.CaseParty.Validators;
using TrueCounsel.Domain.Enums;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseParty.Validators
{
    public class CreateCasePartyCommandValidatorTests
    {
        private readonly CreateCasePartyCommandValidator _validator;

        public CreateCasePartyCommandValidatorTests()
        {
            _validator = new CreateCasePartyCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_CaseId_Is_Empty()
        {
            var command = new CreateCasePartyCommand { CaseId = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.CaseId);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new CreateCasePartyCommand { Name = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Name);
        }

        [Fact]
        public void Should_Have_Error_When_ContactEmail_Is_Invalid()
        {
            var command = new CreateCasePartyCommand { ContactEmail = "invalid-email" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.ContactEmail);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new CreateCasePartyCommand 
            { 
                CaseId = 1, 
                Name = "John Doe", 
                Role = CasePartyRole.Plaintiff,
                ContactEmail = "john@example.com"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
