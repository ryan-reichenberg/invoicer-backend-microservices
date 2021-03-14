using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Invoicer.Common;
using Invoicer.Common.Handlers;
using UserService.Messages.Commands;
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

        public Task HandleAsync(DeleteUserCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
