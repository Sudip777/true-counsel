using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Application.Features.Court.Commands;

namespace TrueCounsel.Application.Features.Court.Commands.Handlers
{
    /// <summary>
    /// Handler for DeleteCourtCommand.
    /// Performs a soft delete by setting the DeletedAt timestamp.
    /// This preserves the record for audit purposes while marking it as logically deleted.
    /// The record will be excluded from all queries (GetAll, GetById) after deletion.
    /// </summary>
    public class DeleteCourtHandler : IRequestHandler<DeleteCourtCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourtHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCourtCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            
            // Soft delete through repository (sets DeletedAt timestamp)
            var result = await _unitOfWork.CourtRepository.DeleteAsync(request.Id);
            if (!result) return false;

            // Commit the deletion through Unit of Work (single transaction)
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
