using System;
using Microsoft.EntityFrameworkCore;
using Polly;
using UserService.DTO;

namespace UserService.Repositories
{
    public class UserDbContext : DbContext
    {
        public DbSet<UserDto> DataSet { get; set; }

        // Causing problems for migration generation:
        // System.MissingMethodException: No parameterless constructor defined for type 'UserService.Repositories.UserDbContext'.
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserDto>().HasKey(m => m.Id);
            builder.Entity<UserDto>().ToTable("User");
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
