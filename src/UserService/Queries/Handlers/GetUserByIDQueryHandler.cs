using System;
using System.Threading;
using System.Threading.Tasks;
using Invoicer.Common.Handlers;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Queries.Handlers
{
    public class GetUserByIDQueryHandler : IQueryHandler<GetUserByIdQuery, User>
    {
        private IUserRepository _repository;
        public GetUserByIDQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public  Task<User> HandleAsync(GetUserByIdQuery query)
        {
            return _repository.FindByIdAsync(query.Id);
        }
    }
}
