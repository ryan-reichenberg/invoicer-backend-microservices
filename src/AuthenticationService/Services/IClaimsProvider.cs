using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationService.Services
{
    public interface IClaimsProvider
    {
        Task<IDictionary<string, string>> GetClaimsAsync(Guid userId);
    }
}