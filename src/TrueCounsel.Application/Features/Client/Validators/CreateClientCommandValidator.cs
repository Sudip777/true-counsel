using FluentValidation;
using TrueCounsel.Application.Features.Client.Commands;

namespace TrueCounsel.Application.Features.Client.Validators
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(v => v.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(v => v.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format.");

            RuleFor(v => v.Address)
                .MaximumLength(250).WithMessage("Address must not exceed 250 characters.");

            RuleFor(v => v.DateOfBirth)
                .LessThan(System.DateTime.UtcNow).WithMessage("Date of Birth cannot be in the future.");

            RuleFor(v => v.Occupation)
                .MaximumLength(100).WithMessage("Occupation must not exceed 100 characters.");

            RuleFor(v => v.EmergencyContact)
                .MaximumLength(100).WithMessage("Emergency Contact must not exceed 100 characters.");
        }
    }
}
