using FluentValidation;
using TrueCounsel.Application.Features.CaseNote.Commands;

namespace TrueCounsel.Application.Features.CaseNote.Validators
{
    public class CreateCaseNoteCommandValidator : AbstractValidator<CreateCaseNoteCommand>
    {
        public CreateCaseNoteCommandValidator()
        {
            RuleFor(v => v.CaseId)
                .NotEmpty().WithMessage("CaseId is required.")
                .GreaterThan(0).WithMessage("CaseId must be greater than 0.");

            RuleFor(v => v.AuthorId)
                .NotEmpty().WithMessage("AuthorId is required.")
                .GreaterThan(0).WithMessage("AuthorId must be greater than 0.");

            RuleFor(v => v.Note)
                .NotEmpty().WithMessage("Note is required.")
                .MaximumLength(2000).WithMessage("Note must not exceed 2000 characters.")
                .MinimumLength(5).WithMessage("Note must be at least 5 characters.");
        }
    }
}
