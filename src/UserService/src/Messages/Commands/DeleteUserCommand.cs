using System;
using Convey.CQRS.Commands;

namespace UserService.Messages.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public Guid Id { get; set; }
        
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
        
    }
}
