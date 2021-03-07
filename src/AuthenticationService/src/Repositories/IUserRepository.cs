using System;
using System.Threading.Tasks;
using AuthenticationService.Domain;

namespace AuthenticationService.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(Guid id);
        Task<User> GetUserAsync(string email);
        Task AddUserAsync(User user);
    }
}