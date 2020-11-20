using System;
using System.Net;
using System.Reflection;
using System.Threading;
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


        public async Task<CommandResult> Handle(RegisterUserCommand command, CancellationToken token)
        {
            User user = command.MapToUser();
            foreach (PropertyInfo property in typeof(User).GetProperties())
            {
                if (String.IsNullOrEmpty(property.GetValue(user)?.ToString()))
                {
                    return CommandResult.Failure(HttpStatusCode.UnprocessableEntity, $"Property: '{property.Name}' cannot be processed.");
                }
            }

            try
            {
                var result = await _repository.SaveAsync(user);
            }
            catch (Exception e)
            {
                return CommandResult.Failure(HttpStatusCode.InternalServerError, "Something went wrong saving the user to the db.");
            }

            return CommandResult.Success();
        }
    }
}
