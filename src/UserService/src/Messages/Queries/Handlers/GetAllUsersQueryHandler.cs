using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Microsoft.Extensions.Logging;
using UserService.DTO;
using UserService.Messages.Commands;
using UserService.Queries;
using UserService.Repositories;

namespace UserService.Messages.Queries.Handlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<UserDto>>
    {
        private IUserRepository _repository;
        private ILogger<GetAllUsersQueryHandler> _logger;
        
        public GetAllUsersQueryHandler(IUserRepository repository, ILogger<GetAllUsersQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<UserDto>> HandleAsync(GetAllUsersQuery query)
        {
            var users = await _repository.FindAll();
            _logger.LogInformation("Fetched {} users from db", users.Count);
            return users;
        }
    }
}
