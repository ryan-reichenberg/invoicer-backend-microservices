using System;
using Invoicer.Common;
using UserService.Models;

namespace UserService.Queries
{
    public class GetUserByIdQuery : IQuery<User>
    {
        public string Id { get; set; }
        public GetUserByIdQuery()
        {
        }
    }
}
