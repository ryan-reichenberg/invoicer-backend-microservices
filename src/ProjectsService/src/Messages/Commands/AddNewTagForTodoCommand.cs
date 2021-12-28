using System;
using Convey.CQRS.Commands;
using Invoicer.Common;
using Newtonsoft.Json;

namespace ProjectsService.Messages.Commands
{
    public class AddNewTagForTodoCommand : ICommand
    {
        public Guid Id { get; set; }
        public Guid TodoId { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }

        [JsonConstructor]

        public AddNewTagForTodoCommand(Guid id, Guid todoId, string name, string color)
        {
            Id = Id == Guid.Empty ? Guid.NewGuid() : todoId;;
            TodoId = todoId;
            Name = name;
            Color = color;
        }
    }
}