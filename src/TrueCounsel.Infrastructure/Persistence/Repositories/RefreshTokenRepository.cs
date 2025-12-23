using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Infrastructure.Data;

namespace TrueCounsel.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RefreshTokenRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(RefreshToken refreshToken)
        {
             await _dbContext.RefreshTokens.AddAsync(refreshToken).ConfigureAwait(false);
        }

        /// <summary> find token by string val </summary>
        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
             return await _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token).ConfigureAwait(false);
        }

        public Task UpdateAsync(RefreshToken refreshToken)
        {
             _dbContext.RefreshTokens.Update(refreshToken);
             return Task.CompletedTask;
        }
    }
}
