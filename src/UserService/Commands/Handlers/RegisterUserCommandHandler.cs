using System;
using System.Net;
using System.Threading.Tasks;
using Invoicer.Common;
using Invoicer.Common.Handlers;
using UserService.Mappers;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Commands.Handlers
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private IUserRepository _repository;
        public RegisterUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }


        public async Task<CommandResult> Handle(RegisterUserCommand command)
        {
            User user =  await _repository.SaveAsync(command.MapToUser());
            if (user == null) CommandResult.Failure(HttpStatusCode.NotModified, "No new details to update");
            return CommandResult.Success();
        }
    }
}
