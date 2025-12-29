using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Domain.Interfaces
{
    public interface ICasePartyRepository
    {
        Task<IEnumerable<CaseParty>> GetAllAsync();
        Task<IEnumerable<CaseParty>> GetByCaseIdAsync(int caseId);
        Task<CaseParty?> GetByIdAsync(int id);
        Task AddAsync(CaseParty caseParty);
        Task UpdateAsync(CaseParty caseParty);
        Task<bool> DeleteAsync(int id);
    }
}
