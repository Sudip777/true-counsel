using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Domain.Interfaces
{
    public interface ICaseTypeRepository
    {
        Task<IEnumerable<CaseType>> GetAllAsync();
        Task<CaseType> GetByIdAsync(int id);
        Task AddAsync(CaseType caseType);
    }
}
