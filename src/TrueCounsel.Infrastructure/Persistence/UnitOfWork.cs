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
        private readonly IRefreshTokenRepository _refreshTokenRepository;
      
        public IUserRepository UserRepository => _userRepository;
        public ILawyerRepository LawyerRepository => _lawyerRepository;
        public IClientRepository ClientRepository => _clientRepository;
        public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository;

        public UnitOfWork(
           ApplicationDbContext context,
           IUserRepository userRepository,
           ILawyerRepository lawyerRepository,
           IClientRepository clientRepository,
           IRefreshTokenRepository refreshTokenRepository)
        {
            _dbContext = context;
            _userRepository = userRepository;
            _lawyerRepository = lawyerRepository;
            _clientRepository = clientRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public Task CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
