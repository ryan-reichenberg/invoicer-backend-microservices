using System;
using System.Threading;
using System.Threading.Tasks;
using Invoicer.Common;
using Invoicer.Common.Handlers;
using UserService.Repositories;

namespace UserService.Commands.Handlers
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private IUserRepository _repository;
        public DeleteUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }


        public async Task<CommandResult> Handle(DeleteUserCommand command, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
