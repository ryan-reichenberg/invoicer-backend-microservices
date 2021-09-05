using System;
using System.Collections.Generic;
using Invoicer.Common.Types;
using ProjectsService.Domain;

namespace ProjectsService.DTO
{
    public class ProjectDto : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public List<TodoDto> Todos { get; set; }
        
        public string ClientId { get;  set; }
        public string UserId { get;  set; }

        public ProjectDto()
        {
        }

        public ProjectDto(Guid id, string name, string description, List<TodoDto> todos, string clientId, string userId)
        {
            Id = id;
            Name = name;
            Description = description;
            Todos = todos;
            ClientId = clientId;
            UserId = userId;
        }
    }
}