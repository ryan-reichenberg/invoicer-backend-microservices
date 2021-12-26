using System.Threading.Tasks;
using Convey.CQRS.Queries;
using UserService.DTO;
using UserService.Queries;
using UserService.Repositories;

namespace UserService.Messages.Queries.Handlers
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
    {
        private IUserRepository _repository;
        public GetUserByIdQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public  async Task<UserDto> HandleAsync(GetUserByIdQuery query)
        {
            var user = await _repository.FindByIdAsync(query.Id);

            return user?.AsDto();
        }
    }
}
