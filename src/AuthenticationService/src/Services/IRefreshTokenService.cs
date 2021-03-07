using System;
using System.Threading.Tasks;
using AuthenticationService.Domain;
using AuthenticationService.DTO;

namespace AuthenticationService.Services
{
    public interface IRefreshTokenService
    {
        Task<string> CreateTokenAsync(Guid userId);
        Task RevokeTokenAsync(string refreshToken);
        Task<AuthDto> RefreshAccessTokenAsync(string refreshToken);
    }
}