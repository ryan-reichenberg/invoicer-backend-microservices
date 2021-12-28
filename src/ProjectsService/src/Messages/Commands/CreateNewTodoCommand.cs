using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Convey.CQRS.Commands;
using Invoicer.Common;
using ProjectsService.Domain;

namespace ProjectsService.Messages.Commands.Todos
{
    public class CreateNewTodoCommand : ICommand
    {
        public Guid TodoId { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string ProjectId { get;  set; }
        public TodoStatus Status { get;  set; }
        
        public List<TodoTag> Tags { get;  set; }
    
        [JsonConstructor]
        public CreateNewTodoCommand(Guid todoId, string name, string description, string projectId, TodoStatus status, List<TodoTag> tags)
        {
            TodoId = todoId == Guid.Empty ? Guid.NewGuid() : todoId;
            Name = name;
            Description = description;
            ProjectId = projectId;
            Status = status;
            Tags = tags;
        }
    }
}