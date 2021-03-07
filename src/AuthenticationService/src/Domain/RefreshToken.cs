using System;
using System.Linq;
using System.Security.Cryptography;
using AuthenticationService.Exceptions;
using Invoicer.Common.Types;
using Invoicer.Common.Types.DDD;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationService.Domain
{
    public class RefreshToken : AggregateRoot
    {
        public AggregateId UserId { get; private set; }
        public string Token { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        public bool Revoked => RevokedAt.HasValue;

        protected RefreshToken()
        {
        }

        public RefreshToken(AggregateId id, AggregateId userId, string token, DateTime createdAt,
            DateTime? revokedAt = null)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new EmptyRefreshTokenException();
            }

            Id = id;
            UserId = userId;
            Token = token;
            CreatedAt = createdAt;
            RevokedAt = revokedAt;
        }

        public void Revoke(DateTime revokedAt)
        {
            if (Revoked)
            {
                throw new RevokedRefreshTokenException();
            }

            RevokedAt = revokedAt;
        }
    }
}