using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Infrastructure.Data;

namespace TrueCounsel.Infrastructure.Persistence.Repositories
{
    public class CaseTypeRepository : ICaseTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CaseTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(CaseType caseType)
        {
            await _dbContext.CaseTypes.AddAsync(caseType);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CaseType>> GetAllAsync()
        {
            return await _dbContext.CaseTypes.ToListAsync();
        }

        public async Task<CaseType> GetByIdAsync(int id)
        {
            return await _dbContext.CaseTypes.FindAsync(id);
        }
    }
}