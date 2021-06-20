using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectsService.Domain;

namespace ProjectsService.Repositories
{
    public interface ITodoRepository
    {
        Task<Todo> FindByIdAsync(Guid id);
        Task<List<Todo>> FindTodosByProjectId(Guid projectId);

        Task<Todo> ChangeStatusOfTodo(Guid id, TodoStatus status);
        
        Task<Todo> SaveAsync(Todo entity);
        Task<Todo> UpdateAsync(Todo entity);
        Task<Todo> DeleteAsync(Guid id);
    }
}