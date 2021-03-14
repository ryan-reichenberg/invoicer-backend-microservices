using System;
using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using UserService.Messages.Commands;
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

        public Task HandleAsync(UpdateUserDetailsCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
