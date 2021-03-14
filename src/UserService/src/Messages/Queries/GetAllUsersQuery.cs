using System;
using System.Collections.Generic;
using Invoicer.Common;
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
