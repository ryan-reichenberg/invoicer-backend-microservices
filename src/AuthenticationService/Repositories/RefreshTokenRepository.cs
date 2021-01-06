using System.Threading.Tasks;
using AuthenticationService.Domain;

namespace AuthenticationService.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        public Task<RefreshToken> GetTokenAsync(string token)
        {
            throw new System.NotImplementedException();
        }

        public Task AddTokenAsync(RefreshToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateTokenAsync(RefreshToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}