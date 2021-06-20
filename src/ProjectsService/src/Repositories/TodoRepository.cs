using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectsService.Domain;

namespace ProjectsService.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private TodoDbContext _todoDbContext;

        public TodoRepository(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<Todo> FindByIdAsync(Guid id)
        {
            var todo = await _todoDbContext.DataSet.FirstOrDefaultAsync(t => t.Id == id);
            return todo.AsEntity();
        }

        public async Task<List<Todo>> FindTodosByProjectId(Guid projectId)
        {
            var todos = await _todoDbContext.DataSet.Where(t => t.ProjectId == projectId.ToString()).ToListAsync();
            return todos.Select(todoDto => todoDto.AsEntity()).ToList();
        }

        public async Task<Todo> ChangeStatusOfTodo(Guid id, TodoStatus status)
        {
            var todo = await _todoDbContext.DataSet.FirstOrDefaultAsync(t => t.Id == id) 
                       ?? throw new NullReferenceException($"Cannot find todo with id: {id}");;
            todo.Status = status;
            await _todoDbContext.SaveChangesAsync();
            return todo.AsEntity();
        }

        public async Task<Todo> SaveAsync(Todo entity)
        {
            await _todoDbContext.DataSet.AddAsync(entity.AsDto());
            await _todoDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Todo> UpdateAsync(Todo entity)
        {
            _todoDbContext.DataSet.Update(entity.AsDto());
            await _todoDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Todo> DeleteAsync(Guid id)
        {
            var todo =  await _todoDbContext.DataSet.FirstOrDefaultAsync(u => u.Id == id)
                        ?? throw new NullReferenceException($"Cannot find todo with id: {id}");
            _todoDbContext.DataSet.Remove(todo);
            await _todoDbContext.SaveChangesAsync();
            return null;
        }
    }
}