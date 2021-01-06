using System;
using Newtonsoft.Json;

namespace AuthenticationService.Messages.Commands
{
    public class RefreshTokenRevokeComamnd
    {
        public Guid UserId { get; }
        public string RefreshToken { get; }

        [JsonConstructor]
        public RefreshTokenRevokeComamnd(Guid userId, string refreshToken)
        {
            UserId = userId;
            RefreshToken = refreshToken;
        }
    }
}