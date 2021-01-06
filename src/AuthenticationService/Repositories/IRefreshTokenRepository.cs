using System.Threading.Tasks;
using AuthenticationService.Domain;

namespace AuthenticationService.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetTokenAsync(string token);
        Task AddTokenAsync(RefreshToken token);
        Task UpdateTokenAsync(RefreshToken token);
    }
}