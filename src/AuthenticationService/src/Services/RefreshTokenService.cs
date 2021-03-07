using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AuthenticationService.Domain;
using AuthenticationService.DTO;
using AuthenticationService.Exceptions;
using AuthenticationService.Exceptions.ApplicationExceptions;
using AuthenticationService.Providers;
using AuthenticationService.Repositories;
using Invoicer.Common.Types.DDD;

namespace AuthenticationService.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> CreateTokenAsync(Guid userId)
        {
            var token = GenerateToken();
            var refreshToken = new RefreshToken(new AggregateId(), userId, token, DateTime.UtcNow);
            await _refreshTokenRepository.AddTokenAsync(refreshToken);

            return token;
        }

        private string GenerateToken(int length = 50, bool removeSpecialChars = true) {
            
            string[] specialChars = {"/", "\\", "=", "+", "?", ":", "&"};
            
            using var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            var result = Convert.ToBase64String(bytes);

            return removeSpecialChars
                ? specialChars.Aggregate(result, (current, chars) => current.Replace(chars, string.Empty))
                : result;
        
        }

        public async Task RevokeTokenAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetTokenAsync(refreshToken);
            if (token is null)
            {
                throw new InvalidRefreshTokenException();
            }

            token.Revoke(DateTime.UtcNow);
            await _refreshTokenRepository.UpdateTokenAsync(token);
        }

        public async Task<AuthDto> RefreshAccessTokenAsync(string refreshToken)
        {
            var token = await _refreshTokenRepository.GetTokenAsync(refreshToken);
            if (token is null)
            {
                throw new InvalidRefreshTokenException();
            }

            if (token.Revoked)
            {
                throw new RevokedRefreshTokenException();
            }

            var user = await _userRepository.GetUserAsync(token.UserId);
            if (user is null)
            {
                throw new UserNotFoundException(token.UserId);
            }

            var claims = user.Permissions.Any()
                ? new Dictionary<string, IEnumerable<string>>
                {
                    ["permissions"] = user.Permissions
                }
                : null;
            var auth = _jwtProvider.Create(token.UserId, user.Role, claims: claims);
            auth.RefreshToken = refreshToken;

            return auth;
        }
    }
}