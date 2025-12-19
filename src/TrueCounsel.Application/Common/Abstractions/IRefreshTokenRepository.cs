using System.Threading.Tasks;
using TrueCounsel.Domain.Entities;

namespace TrueCounsel.Application.Common.Abstractions
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken refreshToken);
 
        Task UpdateAsync(RefreshToken refreshToken);
    }
}