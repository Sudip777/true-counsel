using FluentValidation;
using TrueCounsel.Application.Features.LegalCase.Commands;

namespace TrueCounsel.Application.Features.LegalCase.Commands.Validators
{
    public class UpdateLegalCaseCommandValidator : AbstractValidator<UpdateLegalCaseCommand>
    {
        public UpdateLegalCaseCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(p => p.CaseNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 200 characters.");

            RuleFor(p => p.ClientId)
                .GreaterThan(0).WithMessage("{PropertyName} must be a valid Client ID.");

            RuleFor(p => p.LawyerId)
                .GreaterThan(0).WithMessage("{PropertyName} must be a valid Lawyer ID.");
                
            RuleFor(p => p.EstimatedValue)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be negative.");
        }
    }
}
