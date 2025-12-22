using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Domain.Interfaces
{
    public interface ILawyerRepository
    {
        /// <summary>
        /// Gets all lawers from db
        /// </summary>
        Task<IEnumerable<Lawyer>> GetAllAsync();
        Task<Lawyer?> GetByIdAsync(int id);
        Task AddAsync(Lawyer lawyer);
        Task UpdateAsync(Lawyer lawyer);
        Task<bool> DeleteAsync(int id);
    }
}
