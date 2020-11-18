using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Invoicer.Common;
using Invoicer.Common.Handlers;
using UserService.Mappers;
using UserService.Repositories;

namespace UserService.Commands.Handlers
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _repository;
        public DeleteUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }


        public async Task<CommandResult> Handle(DeleteUserCommand command, CancellationToken token)
        {
            try
            {
                await _repository.DeleteAsync(command.Id);
            }
            catch (Exception e)
            {
                return CommandResult.Failure(HttpStatusCode.InternalServerError, "Something went wrong saving the user to the db.");
            }

            return CommandResult.Success();
        }
    }
}
