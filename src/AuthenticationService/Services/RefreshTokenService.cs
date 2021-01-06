using System;
using System.Threading.Tasks;
using AuthenticationService.Domain;
using AuthenticationService.Repositories;

namespace AuthenticationService.Services
{
    public class RefreshTokenService  : IRefreshTokenService
    {
        public Task AddTokenAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<JWT> CreateAccessTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task RevokeTokenAsync(string refreshToken, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}