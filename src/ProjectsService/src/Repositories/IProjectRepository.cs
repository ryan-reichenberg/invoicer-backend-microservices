using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectsService.Domain;

namespace ProjectsService.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> FindByIdAsync(Guid id);
        Task<List<Project>> FindAllProjectsByUserId(Guid id);
        
        Task<Project> SaveAsync(Project entity);
        Task<Project> UpdateAsync(Project entity);
        Task<Project> DeleteAsync(Guid id);
    }
}