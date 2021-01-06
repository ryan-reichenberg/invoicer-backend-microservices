using System;
using System.Threading.Tasks;
using AuthenticationService.Domain;

namespace AuthenticationService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public Task RegisterAsync(Guid id, string email, string password, string role = Role.User)
        {
            throw new NotImplementedException();
        }

        public Task<JWT> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}