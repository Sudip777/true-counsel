using System.Threading.Tasks;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Common.Abstractions
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        /// <summary>
        /// Exposes the LawyerRepository to app layer
        /// </summary>
        ILawyerRepository LawyerRepository { get; }
        IClientRepository ClientRepository { get; }
        ICourtRepository CourtRepository { get; }
        ICasePartyRepository CasePartyRepository { get; }
        ILegalCaseRepository LegalCaseRepository { get; }

        IRefreshTokenRepository RefreshTokenRepository { get; }
        /// <summary>
        /// Commits all changes in a single transaction.
        /// Ensures transactional consistency across all repository operations.
        /// </summary>
        Task CommitAsync();

    }
}
