using System;
using System.Collections.Generic;
using Invoicer.Common;
using ProjectsService.DTO;

namespace ProjectsService.Messages.Commands
{
    public class UpdateProjectCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public List<TodoDto> Todos { get; set; }
        
        public string ClientId { get;  set; }
        public string UserId { get;  set; }

        public UpdateProjectCommand(Guid id, string name, string description, List<TodoDto> todos, string clientId, string userId)
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