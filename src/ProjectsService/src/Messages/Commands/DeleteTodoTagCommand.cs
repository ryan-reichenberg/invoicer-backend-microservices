using System;
using Invoicer.Common;
using Newtonsoft.Json;

namespace ProjectsService.Messages.Commands
{
    public class DeleteTodoTagCommand : ICommand
    {
        public Guid Id { get; set; }
        
        [JsonConstructor]
        public DeleteTodoTagCommand()
        {
        }
    }
}