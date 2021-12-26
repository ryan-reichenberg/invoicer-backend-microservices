using System;
using Convey.CQRS.Queries;
using Newtonsoft.Json;
using UserService.DTO;

namespace UserService.Queries
{
    public class GetUserByIdQuery : IQuery<UserDto>
    {
        public Guid Id { get; set; }
        
        [JsonConstructor]
        public GetUserByIdQuery()
        {
        }
    }
}
