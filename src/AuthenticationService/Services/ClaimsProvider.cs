using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationService.Services
{
    public class ClaimsProvider : IClaimsProvider
    {
        public async Task<IDictionary<string, string>> GetClaimsAsync(Guid userId)
        {
            return await Task.FromResult(new Dictionary<string, string>());
        }
    }
}