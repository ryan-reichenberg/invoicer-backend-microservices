using System;
using Invoicer.Common;
using Newtonsoft.Json;
using ProjectsService.Domain;

namespace ProjectsService.Messages.Commands.Todos
{
    public class CreateNewTagForTodoCommand : ICommand
    {
        public Guid Id { get; set; }
        public TodoTag Tag { get; set; }

        [JsonConstructor]
        public CreateNewTagForTodoCommand(Guid id, TodoTag tag)
        {
            Id = id;
            Tag = tag;
        }
    }
}