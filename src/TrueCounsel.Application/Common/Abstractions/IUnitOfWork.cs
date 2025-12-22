using System.Threading.Tasks;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Application.Common.Abstractions
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        /// <summary>
        /// Exposes the LawerRepo to app layer
        /// </summary>
        ILawyerRepository LawyerRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        /// <summary>
        /// Commits all changes in a single transaction.
        /// </summary>
        Task CommitAsync();

    }
}
