using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Convey.Types;
using Invoicer.Common.Types;
using ProjectsService.Domain;

namespace ProjectsService.DTO
{
    public class TodoDto : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get;  set; }
        public List<TodoTagDto> Tags { get; set; }

        public TodoDto()
        {
        }

        public TodoDto(Guid id, string name, string description, TodoStatus status, List<TodoTagDto> tags)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            Tags = tags;
        }
    }
}