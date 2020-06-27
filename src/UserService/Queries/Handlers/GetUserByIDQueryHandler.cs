using System;
using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Queries.Handlers
{
    public class GetUserByIDQueryHandler : IQueryHandler<GetUserByIDQuery, User>
    {
        private IUserRepository _repository;
        public GetUserByIDQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<User> Handle(GetUserByIDQuery query)
        {
            return await _repository.FindByIdAsync(new Guid().ToString());
        }
    }
}
