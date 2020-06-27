using System;
using Invoicer.Common;
using UserService.Models;

namespace UserService.Queries
{
    public class GetUserByIDQuery : IQuery<User>
    {
        public string Id { get; set; }
        public GetUserByIDQuery()
        {
        }
    }
}
