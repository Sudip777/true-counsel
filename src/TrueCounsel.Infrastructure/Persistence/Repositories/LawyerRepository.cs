using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Infrastructure.Data;

namespace TrueCounsel.Infrastructure.Persistence.Repositories
{
    public class LawyerRepository : ILawyerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LawyerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Lawyer lawyer)
        {
             await _dbContext.Lawyers.AddAsync(lawyer).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(int id)
        {
             var lawyer = await _dbContext.Lawyers.FindAsync(id).ConfigureAwait(false);
             if(lawyer == null) return false;
             _dbContext.Lawyers.Remove(lawyer);
             return true;
        }

        /// <summary> getz all lawers list </summary>
        public async Task<IEnumerable<Lawyer>> GetAllAsync()
        {
            return await _dbContext.Lawyers.ToListAsync().ConfigureAwait(false);
        }

        public async Task<Lawyer?> GetByIdAsync(int id)
        {
             return await _dbContext.Lawyers.FindAsync(id).ConfigureAwait(false);
        }

        public  Task UpdateAsync(Lawyer lawyer)
        {
             _dbContext.Lawyers.Update(lawyer);
             return Task.CompletedTask;
        }
    }
}
