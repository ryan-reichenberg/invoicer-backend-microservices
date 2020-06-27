using System;
using Invoicer.Common.Handlers;

namespace UserService.Commands.Handlers
{
    public class UpdateUserDetailsCommandHandler : ICommandHandler<UpdateUserDetailsCommand>
    {

        public void Handle(UpdateUserDetailsCommand command)
        {
            Console.WriteLine($"Create user {command.Id} {command.UpdateDetais} - handler");
        }
    }
}
