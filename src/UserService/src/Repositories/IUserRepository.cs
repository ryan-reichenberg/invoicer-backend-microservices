
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Domain;
using UserService.DTO;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDto>> FindAll();
        Task<User> FindByIdAsync(Guid id);
        Task<User> SaveAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task<User> DeleteAsync(Guid id);
    }
}
