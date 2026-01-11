using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.CaseCategory.Commands;
using TrueCounsel.Application.Features.CaseCategory.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.CaseCategory.Validators
{
    public class CreateCaseCategoryCommandValidatorTests
    {
        private readonly CreateCaseCategoryCommandValidator _validator;

        public CreateCaseCategoryCommandValidatorTests()
        {
            _validator = new CreateCaseCategoryCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_CaseTypeId_Is_Empty()
        {
            var command = new CreateCaseCategoryCommand { CaseTypeId = 0, Name = "Test" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.CaseTypeId);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new CreateCaseCategoryCommand { Name = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Exceeds_MaxLength()
        {
            var command = new CreateCaseCategoryCommand { Name = new string('a', 101) };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Name);
        }

        [Fact]
        public void Should_Have_Error_When_Description_Exceeds_MaxLength()
        {
            var command = new CreateCaseCategoryCommand { Description = new string('a', 501), Name = "Test" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Description);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new CreateCaseCategoryCommand 
            { 
                Name = "Valid Name", 
                Description = "Valid Description", 
                CaseTypeId = 1 
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
