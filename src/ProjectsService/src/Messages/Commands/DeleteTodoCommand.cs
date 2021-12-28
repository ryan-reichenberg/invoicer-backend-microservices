using System;
using Convey.CQRS.Commands;
using Invoicer.Common;
using Newtonsoft.Json;

namespace ProjectsService.Messages.Commands
{
    public class DeleteTodoCommand : ICommand
    {
        public Guid Id { get; set; }
        
        [JsonConstructor]
        public DeleteTodoCommand()
        {
        }
    }
}