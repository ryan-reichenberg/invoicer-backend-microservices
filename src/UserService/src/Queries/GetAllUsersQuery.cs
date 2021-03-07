using System;
using System.Collections.Generic;
using Invoicer.Common;
using UserService.Models;

namespace UserService.Queries
{
    public class GetAllUsersQuery : IQuery<List<User>>
    {
        public GetAllUsersQuery()
        {
        }
    }
}
