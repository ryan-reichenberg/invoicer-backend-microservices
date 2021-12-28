using System;
using Convey.CQRS.Commands;
using Invoicer.Common;
using Newtonsoft.Json;

namespace ProjectsService.Messages.Commands
{
    public class DeleteProjectCommand : ICommand
    {
     
        public Guid Id { get; set; }
        
        [JsonConstructor]
        public DeleteProjectCommand()
        {
        }
    }
}