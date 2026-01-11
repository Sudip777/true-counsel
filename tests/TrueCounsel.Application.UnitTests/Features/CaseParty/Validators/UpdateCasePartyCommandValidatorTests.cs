using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.CaseParty.Commands;
using TrueCounsel.Application.Features.CaseParty.Validators;
using TrueCounsel.Domain.Enums;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseParty.Validators
{
    public class UpdateCasePartyCommandValidatorTests
    {
        private readonly UpdateCasePartyCommandValidator _validator;

        public UpdateCasePartyCommandValidatorTests()
        {
            _validator = new UpdateCasePartyCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new UpdateCasePartyCommand { Id = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Id);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new UpdateCasePartyCommand { Name = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Name);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new UpdateCasePartyCommand 
            { 
                Id = 1, 
                Name = "John Doe", 
                Role = CasePartyRole.Defendant,
                ContactEmail = "john.doe@example.com"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
