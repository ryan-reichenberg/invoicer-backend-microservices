using System.Collections.Generic;
using AuthenticationService.Domain;

namespace AuthenticationService.Handlers
{
    public interface IJwtHandler
    {
        JWT CreateToken(string userId, string role = null, string audience = null,
            IDictionary<string, IEnumerable<string>> claims = null);

        JWTPayload GetTokenPayload(string accessToken);
    }
}