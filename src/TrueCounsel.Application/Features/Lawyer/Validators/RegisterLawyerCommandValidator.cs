using FluentValidation;
using TrueCounsel.Application.Features.Lawyer.Commands;

namespace TrueCounsel.Application.Features.Lawyer.Validators
{
    public class RegisterLawyerCommandValidator : AbstractValidator<RegisterLawyerCommand>
    {
        public RegisterLawyerCommandValidator()
        {
            RuleFor(p => p.UserId)
                .GreaterThan(0).WithMessage("{PropertyName} must be valid.");

            RuleFor(p => p.BarNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.HourlyRate)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be positive.");

            RuleFor(p => p.YearsOfExperience)
                .GreaterThanOrEqualTo(0).When(p => p.YearsOfExperience.HasValue)
                .WithMessage("{PropertyName} cannot be negative.");

            RuleFor(p => p.Specialization)
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
            
            RuleFor(p => p.Bio)
                 .MaximumLength(1000).WithMessage("{PropertyName} must not exceed 1000 characters.");
        }
    }
}
