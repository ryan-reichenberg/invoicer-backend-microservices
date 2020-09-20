using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Invoicer.Common.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        private readonly ILogger _logger;
        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> DeleteAsync(string id)
        {
            var user = await FindByIdAsync(id) ?? throw new NullReferenceException($"Cannot find user with id: {id}");
            _dbContext.DataSet.Remove(user);
            await _dbContext.SaveChangesAsync();
            return user;

        }

        public async Task<User> FindByIdAsync(string id)
        {
            return await _dbContext.DataSet.FirstOrDefaultAsync(user => user.Id == id); ;

        }

        public async Task<User> UpdateAsync(User entity)
        {
            var user = await FindByIdAsync(entity.Id);
            if (user == null)
            {
                // problem
                return null;
            }

            if (user == entity)
            {
                _logger.LogDebug("No new data to update");
                user = null;
            }
            else
            {
                Type type = typeof(User);
                foreach (PropertyInfo property in type.GetProperties())
                {
                    if (property.GetValue(user) != property.GetValue(entity))
                    {
                        property.SetValue(user, property.GetValue(entity));
                    }
                }

                _dbContext.DataSet.Update(user);
                await _dbContext.SaveChangesAsync();
            }

            return user;
        }

        public async Task<User> SaveAsync(User entity)
        {
            var user = await FindByIdAsync(entity.Id);
            if (user == null)
            {
                await _dbContext.DataSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                user = entity;
            }
            else
            {
                _logger.LogDebug($"User[{entity}] already exists");
                user = null;
            }

            return user;
        }

        public async Task<List<User>> FindAllAsync() {
            return await _dbContext.DataSet.ToListAsync();
        }
    }
}
