using System;
using Invoicer.Common.DataAccess;
using Microsoft.EntityFrameworkCore;
using Polly;
using UserService.Models;
using UserService.Repositories;

namespace UserService.DataAccess
{
    public class UserDbContext : DbContext, IDbContext<User>
    {
        public DbSet<User> DataSet { get; set; }

        //public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        //{
        //}
        public UserDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost,1434;user id=sa;password=tSlY4ETLAZ;database=UserManagement;");
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
