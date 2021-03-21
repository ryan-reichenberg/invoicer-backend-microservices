using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Domain;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        private readonly ILogger _logger;
        public UserRepository(UserDbContext dbContext, ILogger<UserRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<User> DeleteAsync(Guid id)
        {
            var user =  await _dbContext.DataSet.FirstOrDefaultAsync(u => u.Id == id)
                        ?? throw new NullReferenceException($"Cannot find user with id: {id}");
            _dbContext.DataSet.Remove(user);
            await _dbContext.SaveChangesAsync();
            return null;

        }

        public async Task<User> FindByIdAsync(Guid id)
        {
            var user = await _dbContext.DataSet.FirstOrDefaultAsync(u => u.Id == id);
            return user.AsEntity(); 

        }

        public async Task<User> UpdateAsync(User entity)
        {
            _dbContext.DataSet.Update(entity.AsDto());
            await _dbContext.SaveChangesAsync();
            return entity;
            // var user = await FindByIdAsync(entity.Id);
            // if (user == null)
            // {
            //     // problem
            //     return null;
            // }
            //
            // if (user == entity)
            // {
            //     _logger.LogDebug("No new data to update");
            //     user = null;
            // }
            // else
            // {
            //     foreach (PropertyInfo property in typeof(User).GetProperties())
            //     {
            //         if (!String.Equals(property.GetValue(user)?.ToString(), property.GetValue(entity)?.ToString()))
            //         {
            //             property.SetValue(user, property.GetValue(entity));
            //         }
            //     }
            //
            //     _dbContext.DataSet.Update(user);
            //     await _dbContext.SaveChangesAsync();
            // }
            //
            // return user;
        }

        public async Task<User> SaveAsync(User entity)
        {
            await _dbContext.DataSet.AddAsync(entity.AsDto());
            await _dbContext.SaveChangesAsync();
            // // TODO: Is this really needed? Can a new registered entity have a ID
            // // Can save some time here too.
            // var user = await FindByIdAsync(entity.Id);
            // if (user == null)
            // {
            //     _dbContext.DataSet.Add(entity);
            //     await _dbContext.SaveChangesAsync();
            //     user = entity;
            // }
            // else
            // {
            //     _logger.LogDebug($"User[{entity}] already exists");
            //     user = null;
            // }
            // _logger.LogInformation($"Successfully saved user: {user}");
            return entity;
        }
        
    }
}
