using System;
using Invoicer.Common;
using Newtonsoft.Json;
using ProjectsService.Domain;

namespace ProjectsService.Messages.Commands.Todos
{
    public class ChangeTodoStatusCommand : ICommand
    {
        public Guid Id { get; set; }
        public TodoStatus TodoStatus { get; set; }
        
        [JsonConstructor]
        public ChangeTodoStatusCommand(Guid id, TodoStatus todoStatus)
        {
            Id = id;
            TodoStatus = todoStatus;
        }
    }
}