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
    public class CasePartyRepository : ICasePartyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CasePartyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CaseParty>> GetAllAsync()
        {
            return await _dbContext.CaseParties
                .Where(cp => cp.DeletedAt == null)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<CaseParty>> GetByCaseIdAsync(int caseId)
        {
            return await _dbContext.CaseParties
                .Where(cp => cp.CaseId == caseId && cp.DeletedAt == null)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CaseParty?> GetByIdAsync(int id)
        {
            return await _dbContext.CaseParties
                .FirstOrDefaultAsync(cp => cp.Id == id && cp.DeletedAt == null);
        }

        public async Task UpdateAsync(CaseParty caseParty)
        {
            _dbContext.CaseParties.Update(caseParty);
            await Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var caseParty = await _dbContext.CaseParties.FindAsync(id);
            if (caseParty == null) return false;

            caseParty.DeletedAt = DateTime.UtcNow;
            _dbContext.CaseParties.Update(caseParty);
            return true;
        }

        public async Task AddAsync(CaseParty caseParty)
        {
            await _dbContext.CaseParties.AddAsync(caseParty);
        }
    }
}
