using System.Linq;
using Jaeger.Thrift;
using ProjectsService.Domain;
using ProjectsService.DTO;

namespace ProjectsService.Repositories
{
    public static class Extensions
    {
        public static Project AsEntity(this ProjectDto projectDto)
                => new(projectDto.Id, projectDto.Name, projectDto.Description, projectDto.Todos.Select(x => new Todo(x.Id, x.Name, x.Description, x.Status, x.Tags.Select(tag => new TodoTag(tag.Id, tag.Name, tag.Color)).ToList())).ToList(), projectDto.ClientId, projectDto.UserId);
        
        public static ProjectDto AsDto(this Project project)
            => new(project.Id,  project.Name, project.Description, project.Todos.Select(x => new TodoDto(x.Id, x.Name, x.Description, x.Status, x.Tags.Select(tag => new TodoTagDto(tag.Id, tag.Name, tag.Color)).ToList())).ToList(), project.ClientId, project.UserId);
        
        public static Todo AsEntity(this TodoDto todoDto)
            => new(todoDto.Id, todoDto.Name, todoDto.Description, todoDto.Status, todoDto.Tags.Select(x => new TodoTag(x.Id,x.Name, x.Color)).ToList());
        
        public static TodoTag AsEntity(this TodoTagDto todoTag)
            => new(todoTag.Id, todoTag.Name, todoTag.Color);
        
        public static TodoDto AsDto(this Todo todo)
            => new(todo.Id, todo.Name, todo.Description, todo.Status, todo.Tags.Select(x => new TodoTagDto(x.Id, x.Name, x.Color)).ToList());
    }
}