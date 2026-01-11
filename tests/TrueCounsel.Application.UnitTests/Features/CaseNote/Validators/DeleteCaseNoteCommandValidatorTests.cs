using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.CaseNote.Commands;
using TrueCounsel.Application.Features.CaseNote.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseNote.Validators
{
    public class DeleteCaseNoteCommandValidatorTests
    {
        private readonly DeleteCaseNoteCommandValidator _validator;

        public DeleteCaseNoteCommandValidatorTests()
        {
            _validator = new DeleteCaseNoteCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new DeleteCaseNoteCommand(0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Id);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var command = new DeleteCaseNoteCommand(1);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
