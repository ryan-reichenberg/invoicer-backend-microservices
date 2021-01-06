using System.Threading.Tasks;
using AuthenticationService.Domain;

namespace AuthenticationService.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetUserAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task AddUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}