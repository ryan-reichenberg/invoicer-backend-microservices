
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Entities;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindByIdAsync(Guid id);
        Task<User> SaveAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task<User> DeleteAsync(Guid id);
    }
}
