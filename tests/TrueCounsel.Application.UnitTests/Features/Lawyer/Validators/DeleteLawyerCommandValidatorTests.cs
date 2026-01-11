using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Application.Features.Lawyer.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.Lawyer.Validators
{
    public class DeleteLawyerCommandValidatorTests
    {
        private readonly DeleteLawyerCommandValidator _validator;

        public DeleteLawyerCommandValidatorTests()
        {
            _validator = new DeleteLawyerCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new DeleteLawyerCommand(0);
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Id);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var command = new DeleteLawyerCommand(1);
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
