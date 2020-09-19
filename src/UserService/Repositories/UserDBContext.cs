using System;
using Invoicer.Common.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Polly;
using UserService.Models;

namespace UserService.Repositories
{
    public class UserDbContext : DbContext, IDbContext<User>
    {
        public DbSet<User> DataSet { get; set; }

        // Causing problems for migration generation:
        // System.MissingMethodException: No parameterless constructor defined for type 'UserService.Repositories.UserDbContext'.
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(m => m.Id);
            builder.Entity<User>().ToTable("User");
            base.OnModelCreating(builder);
        }

        public void MigrateDB()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.Migrate());
        }

    }
}
