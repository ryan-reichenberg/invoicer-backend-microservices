using System.Threading.Tasks;

namespace AuthenticationService.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        public Task<bool> IsCurrentActiveToken()
        {
            throw new System.NotImplementedException();
        }

        public Task DeactivateCurrentAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsActiveAsync(string token)
        {
            throw new System.NotImplementedException();
        }

        public Task DeactivateAsync(string userId, string token)
        {
            throw new System.NotImplementedException();
        }
    }
}