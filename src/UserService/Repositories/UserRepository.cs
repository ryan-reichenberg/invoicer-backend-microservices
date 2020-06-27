using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Invoicer.Common.DataAccess;
using Microsoft.EntityFrameworkCore;
using UserService.DataAccess;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task DeleteAsync(string id)
        {
            var user = await FindByIdAsync(id) ?? throw new NullReferenceException($"Cannot find user with id: {id}");
            _dbContext.DataSet.Remove(user);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<User> FindByIdAsync(string id)
        {
            return await _dbContext.DataSet.FirstOrDefaultAsync(user => user.Id == id); ;

        }

        public async Task SaveOrUpdateAsync(User entity)
        {
            var user = await FindByIdAsync(entity.Id);
            if (user == null)
            {
                await _dbContext.DataSet.AddAsync(entity);
            }
            else {
                if (user == entity) { throw new Exception("Nothing to update"); }
                Type type = typeof(User);
                foreach(PropertyInfo property in type.GetProperties()){
                    if (property.GetValue(user) != property.GetValue(entity)) {
                        property.SetValue(user, property.GetValue(entity));
                    }
                }

                _dbContext.DataSet.Update(user);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<List<User>> FindAllAsync() {
            return await _dbContext.DataSet.ToListAsync();
        }
    }
}
