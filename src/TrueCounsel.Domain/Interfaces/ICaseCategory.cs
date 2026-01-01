using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Domain.Interfaces
{
    public interface ICaseCategory
    {
        Task<IEnumerable<CaseCategory>> GetAllAsync();
        Task<CaseCategory> GetByIdAsync(int id);
        Task AddAsync(CaseCategory caseCategory);
    }
}
