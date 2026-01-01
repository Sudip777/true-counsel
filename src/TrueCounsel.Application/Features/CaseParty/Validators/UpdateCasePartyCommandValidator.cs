using FluentValidation;
using TrueCounsel.Application.Features.CaseParty.Commands;

namespace TrueCounsel.Application.Features.CaseParty.Validators
{
    public class UpdateCasePartyCommandValidator : AbstractValidator<UpdateCasePartyCommand>
    {
        public UpdateCasePartyCommandValidator()
        {
            RuleFor(v => v.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(v => v.Role)
                .IsInEnum().WithMessage("Role must be a valid CasePartyRole.");

            RuleFor(v => v.Organization)
                .MaximumLength(150).WithMessage("Organization must not exceed 150 characters.");

            RuleFor(v => v.ContactEmail)
                .EmailAddress().WithMessage("ContactEmail must be a valid email address.")
                .When(x => !string.IsNullOrEmpty(x.ContactEmail));

            RuleFor(v => v.ContactPhone)
                .MaximumLength(20).WithMessage("ContactPhone must not exceed 20 characters.");
                
            RuleFor(v => v.Address)
                .MaximumLength(250).WithMessage("Address must not exceed 250 characters.");
        }
    }
}
