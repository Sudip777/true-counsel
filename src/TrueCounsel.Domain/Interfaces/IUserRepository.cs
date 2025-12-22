using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
    }
}
