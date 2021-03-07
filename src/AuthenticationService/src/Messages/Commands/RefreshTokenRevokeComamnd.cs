using System;
using Newtonsoft.Json;

namespace AuthenticationService.Messages.Commands
{
    public class RefreshTokenRevokeComamnd
    {
        public string RefreshToken { get; }

        [JsonConstructor]
        public RefreshTokenRevokeComamnd(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}