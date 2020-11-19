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
            try {
                User user = await _repository.UpdateAsync(command.MapToUser());
                if (user == null) CommandResult.Failure(HttpStatusCode.NotModified, "No new details to update");
            }
            catch (Exception e)
            {
                return CommandResult.Failure(HttpStatusCode.InternalServerError, "Something went wrong saving the user to the db.");
            }
            return CommandResult.Success();
        }
    }
}
