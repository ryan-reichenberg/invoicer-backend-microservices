using System;
using Microsoft.EntityFrameworkCore;
using Polly;
using ProjectsService.DTO;

namespace ProjectsService.Repositories
{
    public class TodoDbContext : DbContext
    {
        public DbSet<TodoDto> DataSet { get; set; }

        // Causing problems for migration generation:
        // System.MissingMethodException: No parameterless constructor defined for type 'UserService.Repositories.UserDbContext'.
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoDto>().HasKey(m => m.Id);
            builder.Entity<TodoDto>().ToTable("Todo");
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