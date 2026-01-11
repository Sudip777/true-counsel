using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.CaseNote.Commands;
using TrueCounsel.Application.Features.CaseNote.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseNote.Validators
{
    public class CreateCaseNoteCommandValidatorTests
    {
        private readonly CreateCaseNoteCommandValidator _validator;

        public CreateCaseNoteCommandValidatorTests()
        {
            _validator = new CreateCaseNoteCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_CaseId_Is_Empty()
        {
            var command = new CreateCaseNoteCommand { CaseId = 0, Note = "Valid Note" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.CaseId);
        }

        [Fact]
        public void Should_Have_Error_When_AuthorId_Is_Empty()
        {
            var command = new CreateCaseNoteCommand { AuthorId = 0, Note = "Valid Note" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.AuthorId);
        }

        [Fact]
        public void Should_Have_Error_When_Note_Is_Empty()
        {
            var command = new CreateCaseNoteCommand { Note = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Note);
        }

        [Fact]
        public void Should_Have_Error_When_Note_Is_Too_Short()
        {
            var command = new CreateCaseNoteCommand { Note = "abc" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Note);
        }

        [Fact]
        public void Should_Have_Error_When_Note_Exceeds_MaxLength()
        {
            var command = new CreateCaseNoteCommand { Note = new string('a', 2001) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Note);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new CreateCaseNoteCommand 
            { 
                CaseId = 1, 
                AuthorId = 1, 
                Note = "This is a valid note." 
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
