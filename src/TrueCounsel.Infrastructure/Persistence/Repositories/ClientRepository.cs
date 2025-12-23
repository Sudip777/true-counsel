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
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _dbContext.Clients
                .Include(c => c.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _dbContext.Clients
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task UpdateAsync(Client client)
        {
            _dbContext.Clients.Update(client);
            return Task.CompletedTask;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client == null) return false;

            _dbContext.Clients.Remove(client);
            return true;
        }

        public async Task AddAsync(Client client)
        {
            await _dbContext.Clients.AddAsync(client);
        }
    }
}