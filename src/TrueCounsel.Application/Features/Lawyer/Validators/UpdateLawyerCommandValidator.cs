using FluentValidation;
using TrueCounsel.Application.Features.Lawyer.Commands;

namespace TrueCounsel.Application.Features.Lawyer.Validators
{
    public class UpdateLawyerCommandValidator : AbstractValidator<UpdateLawyerCommand>
    {
        public UpdateLawyerCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0).WithMessage("{PropertyName} must be valid.");

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
