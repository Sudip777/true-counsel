using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TrueCounsel.Application.Features.Lawyer.Commands;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Features.Lawyer.Commands.Handlers
{
    public class DeleteLawyerHandler : IRequestHandler<DeleteLawyerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly FluentValidation.IValidator<DeleteLawyerCommand> _validator;

        public DeleteLawyerHandler(IUnitOfWork unitOfWork, FluentValidation.IValidator<DeleteLawyerCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        /// <summary> deletz lawer by id </summary>
        public async Task<bool> Handle(DeleteLawyerCommand command, CancellationToken cancellationToken)
        {
            if (command == null) return false;

            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                 throw new TrueCounsel.Application.Common.Exceptions.AppValidationException(validationResult.Errors);
            }

            var result = await _unitOfWork.LawyerRepository.DeleteAsync(command.Id);
            if (result)
            {
                await _unitOfWork.CommitAsync();
            }
            return result;
        }
    }
}
