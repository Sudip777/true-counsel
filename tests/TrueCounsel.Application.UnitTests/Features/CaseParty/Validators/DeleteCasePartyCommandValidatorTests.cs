using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.CaseParty.Commands;
using TrueCounsel.Application.Features.CaseParty.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseParty.Validators
{
    public class DeleteCasePartyCommandValidatorTests
    {
        private readonly DeleteCasePartyCommandValidator _validator;

        public DeleteCasePartyCommandValidatorTests()
        {
            _validator = new DeleteCasePartyCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new DeleteCasePartyCommand(0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Id);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var command = new DeleteCasePartyCommand(1);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
