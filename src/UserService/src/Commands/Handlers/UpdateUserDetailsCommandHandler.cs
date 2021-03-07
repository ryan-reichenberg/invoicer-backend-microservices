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

        public Task HandleAsync(UpdateUserDetailsCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
