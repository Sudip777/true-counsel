using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Court entity with soft delete support.
    /// All queries automatically exclude soft-deleted records (where DeletedAt IS NULL).
    /// </summary>
    public interface ICourtRepository
    {
        /// <summary>
        /// Gets all active courts from the database (excluding soft-deleted records).
        /// </summary>
        Task<IEnumerable<Court>> GetAllAsync();

        /// <summary>
        /// Gets a court by ID. Returns null if not found or if the court is soft-deleted.
        /// </summary>
        Task<Court?> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new court to the database.
        /// </summary>
        Task AddAsync(Court court);

        /// <summary>
        /// Updates an existing court in the database.
        /// </summary>
        Task UpdateAsync(Court court);

        /// <summary>
        /// Soft deletes a court by setting the DeletedAt timestamp.
        /// This preserves the record for audit purposes while marking it as deleted.
        /// </summary>
        /// <returns>True if the court was successfully deleted, false if not found or already deleted.</returns>
        Task<bool> DeleteAsync(int id);
    }
}
