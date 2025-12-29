using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;
using TrueCounsel.Domain.Interfaces;
using TrueCounsel.Infrastructure.Data;

namespace TrueCounsel.Infrastructure.Persistence.Repositories
{
    /// <summary>
    /// Repository implementation for Court entity with soft delete support.
    /// All queries automatically exclude soft-deleted records (DeletedAt != null).
    /// </summary>
    public class CourtRepository : ICourtRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CourtRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets all active courts, excluding soft-deleted records.
        /// </summary>
        public async Task<IEnumerable<Court>> GetAllAsync()
        {
            return await _dbContext.Courts
                .Where(c => c.DeletedAt == null)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Gets a court by ID, excluding soft-deleted records.
        /// </summary>
        public async Task<Court?> GetByIdAsync(int id)
        {
            return await _dbContext.Courts
                .Where(c => c.DeletedAt == null && c.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Adds a new court to the database.
        /// </summary>
        public async Task AddAsync(Court court)
        {
            await _dbContext.Courts.AddAsync(court);
        }

        /// <summary>
        /// Updates an existing court in the database.
        /// </summary>
        public Task UpdateAsync(Court court)
        {
            _dbContext.Courts.Update(court);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Soft deletes a court by setting the DeletedAt timestamp.
        /// This approach preserves the record for audit purposes while marking it as logically deleted.
        /// </summary>
        /// <param name="id">The ID of the court to delete.</param>
        /// <returns>True if the court was successfully soft-deleted, false if not found or already deleted.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var court = await _dbContext.Courts
                .Where(c => c.DeletedAt == null && c.Id == id)
                .FirstOrDefaultAsync();
            
            if (court == null) return false;

            // Soft delete: set the DeletedAt timestamp
            court.DeletedAt = DateTime.UtcNow;
            _dbContext.Courts.Update(court);
            
            return true;
        }
    }
}
