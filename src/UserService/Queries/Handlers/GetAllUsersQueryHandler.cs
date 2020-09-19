using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invoicer.Common;
using Invoicer.Common.Handlers;
using UserService.Models;
using UserService.Repositories;

namespace UserService.Queries.Handlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<User>>
    {
        private IUserRepository _repository;
        public GetAllUsersQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<List<User>> Handle(GetAllUsersQuery query)
        {
            return _repository.FindAllAsync();
        }
    }
}
