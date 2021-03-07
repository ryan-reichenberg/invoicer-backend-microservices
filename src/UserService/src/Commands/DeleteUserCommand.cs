using System;
using Invoicer.Common;

namespace UserService.Commands
{
    public class DeleteUserCommand : ICommand
    {
        public string Id { get; set; }

        public DeleteUserCommand()
        {
        }
        
    }
}
