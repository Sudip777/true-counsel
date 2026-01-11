using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.CaseType.Commands;
using TrueCounsel.Application.Features.CaseType.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseType.Validators
{
    public class CreateCaseTypeCommandValidatorTests
    {
        private readonly CreateCaseTypeCommandValidator _validator;

        public CreateCaseTypeCommandValidatorTests()
        {
            _validator = new CreateCaseTypeCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new CreateCaseTypeCommand { Name = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Too_Short()
        {
            var command = new CreateCaseTypeCommand { Name = "a" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Exceeds_MaxLength()
        {
            var command = new CreateCaseTypeCommand { Description = new string('a', 501), Name = "Test" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Description);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new CreateCaseTypeCommand 
            { 
                Name = "Civil", 
                Description = "Civil cases" 
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
