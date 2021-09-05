using System;
using Microsoft.EntityFrameworkCore;
using Polly;
using ProjectsService.DTO;

namespace ProjectsService.Repositories
{
    public class ProjectDbContext: DbContext
    {
        public DbSet<ProjectDto> Projects { get; set; }
        public DbSet<TodoDto> Todos { get; set; }
        public DbSet<TodoTagDto> TodoTags { get; set; }
        
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProjectDto>().ToTable("Project");
            builder.Entity<TodoDto>().ToTable("Todo");
            builder.Entity<TodoTagDto>().ToTable("TodoTag");
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