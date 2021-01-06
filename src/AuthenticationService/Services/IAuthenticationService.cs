using System;
using System.Threading.Tasks;
using AuthenticationService.Domain;

namespace AuthenticationService.Services
{
    public interface IAuthenticationService
    {
        Task RegisterAsync(Guid id, string email, string password, string role = Role.User);
        Task<JWT> LoginAsync(string email, string password);
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}