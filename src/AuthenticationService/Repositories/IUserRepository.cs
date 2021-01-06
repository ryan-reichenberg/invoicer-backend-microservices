using System.Threading.Tasks;
using AuthenticationService.Domain;

namespace AuthenticationService.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string email);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}