using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Invoicer.Common;
using Invoicer.Common.Handlers;
using UserService.Models;
using UserService.Mappers;
using UserService.Repositories;

namespace UserService.Commands.Handlers
{
    public class UpdateUserDetailsCommandHandler : ICommandHandler<UpdateUserDetailsCommand>
    {
        private IUserRepository _repository;
        public UpdateUserDetailsCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }


        public async Task<CommandResult> Handle(UpdateUserDetailsCommand command, CancellationToken token)
        {
            User user =  await _repository.SaveAsync(command.MapToUser());
            if (user == null) CommandResult.Failure(HttpStatusCode.NotModified, "No new details to update");
            return CommandResult.Success();
        }
    }
}
