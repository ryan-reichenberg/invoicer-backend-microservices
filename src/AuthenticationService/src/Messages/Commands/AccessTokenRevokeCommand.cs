using System;
using Newtonsoft.Json;

namespace AuthenticationService.Messages.Commands
{
    public class AccessTokenRevokeCommand
    {
        public Guid UserId { get; }
        
        [JsonConstructor]
        public AccessTokenRevokeCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}