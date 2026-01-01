using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Domain.Interfaces
{
    public interface ICaseNoteRepository
    {
        Task<IEnumerable<CaseNote>> GetAllAsync();
        Task<CaseNote?> GetByIdAsync(int id);
        Task<IEnumerable<CaseNote>> GetByCaseIdAsync(int caseId);
        Task AddAsync(CaseNote caseNote);
        Task<bool> DeleteAsync(int id);
    }
}
