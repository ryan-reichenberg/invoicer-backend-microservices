using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectsService.Domain;
using ProjectsService.DTO;

namespace ProjectsService.Repositories
{
    public class ProjectsRepository : IProjectRepository
    {
        private readonly ProjectDbContext _projectDbContext;
        private readonly ILogger<ProjectsRepository> _logger;

        public ProjectsRepository(ProjectDbContext projectDbContext, ILogger<ProjectsRepository> logger)
        {
            _projectDbContext = projectDbContext;
            _logger = logger;
        }

        public async Task<Project> FindByIdAsync(string id)
        {
            ProjectDto project = await _projectDbContext.Projects.FindAsync(id);
            if (project == null) return null;
            return project.AsEntity();
        }

        public async Task<List<Project>> FindAllProjectsByUserIdAsync(string id)
        {
            List<ProjectDto> projects = await _projectDbContext.Projects.Where(x => x.UserId == id).ToListAsync();
            if (projects == null) return null;
            return projects.Select(x => x.AsEntity()).ToList();
        }

        public async Task<Todo> FindTodoByIdAsync(string todoId)
        {
            var todo = await _projectDbContext.Todos.FirstOrDefaultAsync(x => x.Id.ToString() == todoId);
            return todo.AsEntity();
        }

        public async Task SaveTodoAsync(Todo entity)
        {
            var todo = _projectDbContext.Todos.FirstOrDefault(item => item.Id.ToString() == entity.Id.ToString());
            if (todo == null)
            {
                 _projectDbContext.Todos.Add(entity.AsDto());
            }
            else
            {
                _logger.LogDebug($"Todo with id: {entity.Id} does not exist");
                return;
                
            }
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task SaveProjectAsync(Project entity)
        {
            var project = _projectDbContext.Projects.FirstOrDefault(item => item.Id.ToString() == entity.Id.ToString());
            if (project == null)
            {
                 _projectDbContext.Projects.Add(entity.AsDto());
            }
            else
            {
                _logger.LogDebug($"Project with id: {entity.Id} already exists");
                return;
            }
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task UpdateTodoAsync(Todo entity)
        {
            var todo = _projectDbContext.Todos.FirstOrDefault(item => item.Id.ToString() == entity.Id.ToString());
            if (todo == null)
            {
                _logger.LogDebug($"Todo with id: {entity.Id} does not exist");
                return;
            }
  
            _projectDbContext.Todos.Update(entity.AsDto());
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project entity)
        {
            var project = _projectDbContext.Projects.FirstOrDefault(item => item.Id.ToString() == entity.Id.ToString());
            if (project == null)
            {
                _logger.LogDebug($"Project with id: {entity.Id} does not exist");
                return;
            }
  
            _projectDbContext.Projects.Update(entity.AsDto());
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task DeleteTodoAsync(string id)
        {
            var todo = _projectDbContext.Todos.FirstOrDefault(item => item.Id.ToString() == id);
            if (todo == null)
            {
                _logger.LogDebug($"Couldn't find todo with id: {id}. Skipping...");
                return;
            }
            _projectDbContext.Todos.Remove(todo);
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(string id)
        {
            var project = _projectDbContext.Projects.FirstOrDefault(item => item.Id.ToString() == id);
            if (project == null)
            {
                _logger.LogDebug($"Couldn't find project with id: {id}. Skipping...");
                return;
            }
            _projectDbContext.Projects.Remove(project);
            await _projectDbContext.SaveChangesAsync();
        }

        public async Task DeleteTodoTag(string id)
        {
            var todoTag = _projectDbContext.TodoTags.FirstOrDefault(item => item.Id.ToString() == id);
            if (todoTag == null)
            {
                _logger.LogDebug($"Couldn't find Todo Tag with id: {id}. Skipping...");
                return;
            }
            _projectDbContext.TodoTags.Remove(todoTag);
            await _projectDbContext.SaveChangesAsync();
        }
    }
}