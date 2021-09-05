using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectsService.Domain;

namespace ProjectsService.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> FindByIdAsync(string id);
        Task<List<Project>> FindAllProjectsByUserIdAsync(string id);

        Task<Todo> FindTodoByIdAsync(string todoId);
        
        Task SaveTodoAsync(Todo entity);
        
        Task SaveProjectAsync(Project entity);
        Task UpdateTodoAsync(Todo entity);
        
        Task UpdateProjectAsync(Project entity);
        Task DeleteTodoAsync(string id);
        Task DeleteProjectAsync(string id);
        Task DeleteTodoTag(string id);
    }
}