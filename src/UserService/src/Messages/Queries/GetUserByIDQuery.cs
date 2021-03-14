using System;
using Invoicer.Common;
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
