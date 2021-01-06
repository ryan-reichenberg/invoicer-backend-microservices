using System;
using System.Threading.Tasks;
using AuthenticationService.Domain;

namespace AuthenticationService.Services
{
    public interface IRefreshTokenService
    {
        Task AddTokenAsync(Guid userId);
        Task<JWT> CreateAccessTokenAsync(string refreshToken);
        Task RevokeTokenAsync(string refreshToken, Guid userId);
    }
}