using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindByIdAsync(string id);
        Task<User> SaveAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task<User> DeleteAsync(string id);
        Task<List<User>> FindAllAsync();
    }
}
