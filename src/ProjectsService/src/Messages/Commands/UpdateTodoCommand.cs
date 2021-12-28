using System;
using System.Collections.Generic;
using Convey.CQRS.Commands;
using Invoicer.Common;
using Newtonsoft.Json;
using ProjectsService.Domain;
using ProjectsService.DTO;

namespace ProjectsService.Messages.Commands
{
    public class UpdateTodoCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get;  set; }
        public List<TodoTagDto> Tags { get; set; }
        
        [JsonConstructor]
        public UpdateTodoCommand(Guid id, string name, string description, TodoStatus status, List<TodoTagDto> tags)
        {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            Tags = tags;
        }
    }
}