using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;

namespace TrueCounsel.Infrastructure.Persistence.Repositories
{
    public class CaseCategory : ICaseCategory
    {
        public Task AddAsync(Domain.Entities.CaseCategory caseCategory)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Domain.Entities.CaseCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.CaseCategory> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
