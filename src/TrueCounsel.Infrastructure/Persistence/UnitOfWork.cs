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
        private readonly IRefreshTokenRepository _refreshTokenRepository;
      
        public IUserRepository UserRepository => _userRepository;
        /// <summary> lawer repositry instance </summary>
        public ILawyerRepository LawyerRepository => _lawyerRepository;
        public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository;

        public UnitOfWork(
           ApplicationDbContext context,
           IUserRepository userRepository,
           ILawyerRepository lawyerRepository,
           IRefreshTokenRepository refreshTokenRepository)
        {
            _dbContext = context;
            _userRepository = userRepository;
            _lawyerRepository = lawyerRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public Task CommitAsync()
        {
                return _dbContext.SaveChangesAsync();
        }
    }
}
