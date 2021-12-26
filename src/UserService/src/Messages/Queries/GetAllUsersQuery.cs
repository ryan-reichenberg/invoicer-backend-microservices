using System;
using System.Collections.Generic;
using Convey.CQRS.Queries;
using UserService.DTO;

namespace UserService.Queries
{
    public class GetAllUsersQuery : IQuery<List<UserDto>>
    {
        public GetAllUsersQuery()
        {
        }
    }
}
