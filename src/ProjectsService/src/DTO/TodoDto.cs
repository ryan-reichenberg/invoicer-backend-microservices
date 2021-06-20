using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Invoicer.Common.Types;
using ProjectsService.Domain;

namespace ProjectsService.DTO
{
    public class TodoDto : IIdentifiable<Guid>
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectId { get;  set; }
        public TodoStatus Status { get;  set; }
        
        public List<TodoTagDto> Tags { get; set; }

        public TodoDto(Guid id, string name, string description, string projectId, TodoStatus status, List<TodoTagDto> tags)
        {
            Id = id;
            Name = name;
            Description = description;
            ProjectId = projectId;
            Status = status;
            Tags = tags;
        }
    }
}