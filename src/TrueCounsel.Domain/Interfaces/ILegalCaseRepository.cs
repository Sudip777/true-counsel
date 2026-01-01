using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Domain.Interfaces
{
    public interface ILegalCaseRepository
    {
        Task<IEnumerable<LegalCase>> GetAllAsync();
        Task<LegalCase?> GetByIdAsync(int id);
        Task AddAsync(LegalCase legalCase);
        Task UpdateAsync(LegalCase legalCase);
        Task<bool> DeleteAsync(int id);
    }
}
