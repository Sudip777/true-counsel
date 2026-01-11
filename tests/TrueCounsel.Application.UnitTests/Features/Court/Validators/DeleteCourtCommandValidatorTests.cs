using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.Court.Commands;
using TrueCounsel.Application.Features.Court.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Court.Validators
{
    public class DeleteCourtCommandValidatorTests
    {
        private readonly DeleteCourtCommandValidator _validator;

        public DeleteCourtCommandValidatorTests()
        {
            _validator = new DeleteCourtCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new DeleteCourtCommand(0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Id);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var command = new DeleteCourtCommand(1);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
