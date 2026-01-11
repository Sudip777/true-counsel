using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.LegalCase.Commands;
using TrueCounsel.Application.Features.LegalCase.Commands.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.LegalCase.Validators
{
    public class DeleteLegalCaseCommandValidatorTests
    {
        private readonly DeleteLegalCaseCommandValidator _validator;

        public DeleteLegalCaseCommandValidatorTests()
        {
            _validator = new DeleteLegalCaseCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new DeleteLegalCaseCommand(0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Id);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var command = new DeleteLegalCaseCommand(1);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
