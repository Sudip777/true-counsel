using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Abstractions;

namespace TrueCounsel.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserRepository _userRepository;
      
        //Exposes the UserRepository to the Application layer via the interface property from IUnitOfWork.
        public IUserRepository UserRepository => _userRepository;

        public UnitOfWork(
           ApplicationDbContext context,
           IUserRepository userRepository)
        {
            _dbContext = context;
            _userRepository = userRepository;
        }

        public Task CommitAsync()
        {
                return _dbContext.SaveChangesAsync();
        }
    }

}
