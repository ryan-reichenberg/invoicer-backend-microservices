using System;
using System.Linq;
using System.Security.Cryptography;
using AuthenticationService.Exceptions;
using Invoicer.Common.Types;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Domain
{
    public class RefreshToken: IIdentifiable
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public bool Revoked => RevokedAt.HasValue;

        protected RefreshToken()
        {
        }

        public RefreshToken(User user)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
            CreatedAt = DateTime.UtcNow;
            Token = GenerateToken();
        }

        public void Revoke()
        {
            if (Revoked)
            {
                throw new TokenAlreadyRevokedException(ErrorCodes.RefreshTokenAlreadyRevoked, 
                    $"Refresh token: '{Id}' was already revoked at '{RevokedAt}'.");
            }
            RevokedAt = DateTime.UtcNow;
        }

        private static string GenerateToken(int length = 50, bool removeSpecialChars = true) {
            
            string[] specialChars = {"/", "\\", "=", "+", "?", ":", "&"};
            
            using var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            var result = Convert.ToBase64String(bytes);

            return removeSpecialChars
                ? specialChars.Aggregate(result, (current, chars) => current.Replace(chars, string.Empty))
                : result;
        
        }
    }
}