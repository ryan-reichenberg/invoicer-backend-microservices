using System;
using System.Collections.Generic;
using AuthenticationService.DTO;

namespace AuthenticationService.Providers
{
    public interface IJwtProvider
    {
        AuthDto Create(Guid userId, string role, string audience = null,
            IDictionary<string, IEnumerable<string>> claims = null);
    }
}