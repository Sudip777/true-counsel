using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Infrastructure.Data;

namespace TrueCounsel.Infrastructure.Persistence.Repositories
{
    public class CaseCategory : ICaseCategory
    {
        private readonly ApplicationDbContext _dbContext;

        public CaseCategory(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Domain.Entities.CaseCategory>> GetAllAsync()
        {
            return await _dbContext.CaseCategories
                .Include(c => c.CaseType)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Domain.Entities.CaseCategory> GetByIdAsync(int id)
        {
            return await _dbContext.CaseCategories
                .Include(c => c.CaseType)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Domain.Entities.CaseCategory caseCategory)
        {
            await _dbContext.CaseCategories.AddAsync(caseCategory);
            await _dbContext.SaveChangesAsync();
        }
    }
}

