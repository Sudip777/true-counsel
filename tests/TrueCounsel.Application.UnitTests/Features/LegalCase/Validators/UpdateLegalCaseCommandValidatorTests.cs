using FluentValidation.TestHelper;
using TrueCounsel.Application.Features.LegalCase.Commands;
using TrueCounsel.Application.Features.LegalCase.Commands.Validators;
using Xunit;

namespace TrueCounsel.Application.UnitTests.Features.LegalCase.Validators
{
    public class UpdateLegalCaseCommandValidatorTests
    {
        private readonly UpdateLegalCaseCommandValidator _validator;

        public UpdateLegalCaseCommandValidatorTests()
        {
            _validator = new UpdateLegalCaseCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Invalid()
        {
            var command = new UpdateLegalCaseCommand { Id = 0 };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.Id);
        }

        [Fact]
        public void Should_Have_Error_When_CaseNumber_Is_Empty()
        {
            var command = new UpdateLegalCaseCommand { CaseNumber = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(v => v.CaseNumber);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new UpdateLegalCaseCommand 
            { 
                Id = 1,
                CaseNumber = "LC-2024-001-Updated", 
                Title = "Smith vs Jones II",
                ClientId = 1,
                LawyerId = 1,
                EstimatedValue = 60000.00m
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
