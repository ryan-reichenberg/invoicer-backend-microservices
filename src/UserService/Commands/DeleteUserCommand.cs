using System;
using Invoicer.Common;

namespace UserService.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public string Id { get; private set; }

        public DeleteUserCommand(string id)
        {
            Id = id;
        }
        
    }
}
