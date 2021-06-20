using System.Collections.Generic;
using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using UserService.DTO;
using UserService.Queries;
using UserService.Repositories;

namespace UserService.Messages.Queries.Handlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<UserDto>>
    {
        private IUserRepository _repository;
        public GetAllUsersQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<List<UserDto>> HandleAsync(GetAllUsersQuery query)
        {
            return null;
        }
    }
}
