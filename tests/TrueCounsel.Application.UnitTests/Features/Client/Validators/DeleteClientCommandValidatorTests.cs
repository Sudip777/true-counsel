using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.Client.Commands;
using TrueCounsel.Application.Features.Client.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Client.Validators
{
    public class DeleteClientCommandValidatorTests
    {
        private readonly DeleteClientCommandValidator _validator;

        public DeleteClientCommandValidatorTests()
        {
            _validator = new DeleteClientCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new DeleteClientCommand(0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Id);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var command = new DeleteClientCommand(1);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
