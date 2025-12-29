using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly ILawyerRepository _lawyerRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICourtRepository _courtRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
      
        public IUserRepository UserRepository => _userRepository;
        public ILawyerRepository LawyerRepository => _lawyerRepository;
        public IClientRepository ClientRepository => _clientRepository;
        public ICourtRepository CourtRepository => _courtRepository;
        public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository;

        public UnitOfWork(
           ApplicationDbContext context,
           IUserRepository userRepository,
           ILawyerRepository lawyerRepository,
           IClientRepository clientRepository,
           ICourtRepository courtRepository,
           IRefreshTokenRepository refreshTokenRepository)
        {
            _dbContext = context;
            _userRepository = userRepository;
            _lawyerRepository = lawyerRepository;
            _clientRepository = clientRepository;
            _courtRepository = courtRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        /// <summary>
        /// Commits all pending changes from all repositories in a single database transaction.
        /// This ensures data consistency across multiple repository operations.
        /// </summary>
        public Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
