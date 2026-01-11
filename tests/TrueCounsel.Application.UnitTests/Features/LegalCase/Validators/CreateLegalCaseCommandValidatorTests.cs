using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.LegalCase.Commands;
using TrueCounsel.Application.Features.LegalCase.Commands.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.LegalCase.Validators
{
    public class CreateLegalCaseCommandValidatorTests
    {
        private readonly CreateLegalCaseCommandValidator _validator;

        public CreateLegalCaseCommandValidatorTests()
        {
            _validator = new CreateLegalCaseCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_CaseNumber_Is_Empty()
        {
            var command = new CreateLegalCaseCommand { CaseNumber = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.CaseNumber);
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            var command = new CreateLegalCaseCommand { Title = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Title);
        }

        [Fact]
        public void Should_Have_Error_When_ClientId_Is_Invalid()
        {
            var command = new CreateLegalCaseCommand { ClientId = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.ClientId);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new CreateLegalCaseCommand 
            { 
                CaseNumber = "LC-2024-001", 
                Title = "Smith vs Jones",
                ClientId = 1,
                LawyerId = 1,
                EstimatedValue = 50000.00m
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
