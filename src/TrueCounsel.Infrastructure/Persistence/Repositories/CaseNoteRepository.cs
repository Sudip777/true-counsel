using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Infrastructure.Data;

namespace TrueCounsel.Infrastructure.Persistence.Repositories
{
    public class CaseNoteRepository : ICaseNoteRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CaseNoteRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CaseNote>> GetAllAsync()
        {
            return await _dbContext.CaseNotes
                .Include(cn => cn.LegalCase)
                .Include(cn => cn.Author)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CaseNote?> GetByIdAsync(int id)
        {
            return await _dbContext.CaseNotes
                .Include(cn => cn.LegalCase)
                .Include(cn => cn.Author)
                .FirstOrDefaultAsync(cn => cn.Id == id);
        }

        public async Task<IEnumerable<CaseNote>> GetByCaseIdAsync(int caseId)
        {
            return await _dbContext.CaseNotes
                .Where(cn => cn.CaseId == caseId)
                .Include(cn => cn.Author)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task AddAsync(CaseNote caseNote)
        {
            await _dbContext.CaseNotes.AddAsync(caseNote);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var caseNote = await _dbContext.CaseNotes.FindAsync(id);
            if (caseNote == null) return false;

            _dbContext.CaseNotes.Remove(caseNote);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
