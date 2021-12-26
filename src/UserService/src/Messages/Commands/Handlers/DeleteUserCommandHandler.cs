using System;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using UserService.Repositories;

namespace UserService.Messages.Commands.Handlers
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _repository;
        public DeleteUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(DeleteUserCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
