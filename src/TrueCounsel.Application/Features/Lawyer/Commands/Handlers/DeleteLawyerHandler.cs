using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Lawyer.Commands;

namespace TrueCounsel.Application.Features.Lawyer.Commands.Handlers
{
    public class DeleteLawyerHandler : ICommandHandler<DeleteLawyerCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLawyerHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary> deletz lawer by id </summary>
        public async Task<bool> HandleAsync(DeleteLawyerCommand command, CancellationToken cancellationToken = default)
        {
            if (command == null) return false;
            var result = await _unitOfWork.LawyerRepository.DeleteAsync(command.Id);
            if (result)
            {
                await _unitOfWork.CommitAsync();
            }
            return result;
        }
    }
}
