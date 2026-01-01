using FluentValidation;
using TrueCounsel.Application.Features.CaseParty.Commands;

namespace TrueCounsel.Application.Features.CaseParty.Validators
{
    public class DeleteCasePartyCommandValidator : AbstractValidator<DeleteCasePartyCommand>
    {
        public DeleteCasePartyCommandValidator()
        {
            RuleFor(v => v.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
