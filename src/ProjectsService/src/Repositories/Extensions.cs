using System.Linq;
using ProjectsService.Domain;
using ProjectsService.DTO;

namespace ProjectsService.Repositories
{
    public static class Extensions
    {
        public static Project AsEntity(this ProjectDto projectDto)
                => new(projectDto.Id, projectDto.Name, projectDto.Description, projectDto.ClientId, projectDto.UserId);
        
        public static ProjectDto AsDto(this Project project)
            => new(project.Id,  project.Name, project.Description, project.ClientId, project.UserId);
        
        public static Todo AsEntity(this TodoDto todoDto)
            => new(todoDto.Id, todoDto.Name, todoDto.Description, todoDto.ProjectId, todoDto.Status, todoDto.Tags.Select(x => new TodoTag(x.Name, x.Color)).ToList());
        
        public static TodoDto AsDto(this Todo todo)
            => new(todo.Id, todo.Name, todo.Description, todo.ProjectId, todo.Status, todo.Tags.Select(x => new TodoTagDto(x.Name, x.Color)).ToList());
    }
}