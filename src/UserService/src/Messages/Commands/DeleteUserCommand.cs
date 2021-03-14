using System;
using Invoicer.Common;
using Newtonsoft.Json;

namespace UserService.Messages.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public Guid Id { get; set; }
        
        [JsonConstructor]
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
        
    }
}
