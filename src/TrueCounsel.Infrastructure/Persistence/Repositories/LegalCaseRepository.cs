using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Infrastructure.Data;

namespace TrueCounsel.Infrastructure.Persistence.Repositories
{
    public class LegalCaseRepository : ILegalCaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LegalCaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<LegalCase>> GetAllAsync()
        {
            return await _dbContext.LegalCases
                .Where(c => c.DeletedAt == null)
                .Include(c => c.Client)
                .Include(c => c.Lawyer)
                .Include(c => c.CaseCategory)
                .Include(c => c.Court)
                .Include(c => c.CaseType)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LegalCase?> GetByIdAsync(int id)
        {
            var entity = await _dbContext.LegalCases
                .Include(c => c.Client)
                .Include(c => c.Lawyer)
                .Include(c => c.CaseCategory)
                .Include(c => c.Court)
                .Include(c => c.CaseType)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity?.DeletedAt != null) return null;
            return entity;
        }

        public async Task AddAsync(LegalCase legalCase)
        {
            await _dbContext.LegalCases.AddAsync(legalCase);
        }

        public Task UpdateAsync(LegalCase legalCase)
        {
            _dbContext.LegalCases.Update(legalCase);
            return Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var legalCase = await _dbContext.LegalCases.FindAsync(id);
            if (legalCase == null) return false;

            legalCase.DeletedAt = DateTime.UtcNow;
            _dbContext.LegalCases.Update(legalCase);
            return true;
        }
    }
}
