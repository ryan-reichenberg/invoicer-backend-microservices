using System;
using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using UserService.Repositories;

namespace UserService.Messages.Commands.Handlers
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
