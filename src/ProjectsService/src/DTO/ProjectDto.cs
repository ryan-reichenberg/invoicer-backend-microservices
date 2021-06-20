using System;
using Invoicer.Common.Types;

namespace ProjectsService.DTO
{
    public class ProjectDto : IIdentifiable<Guid>
    {
        public Guid Id { get; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string ClientId { get;  set; }
        public string UserId { get;  set; }

        public ProjectDto(Guid id, string name, string description, string clientId, string userId)
        {
            Id = id;
            Name = name;
            Description = description;
            ClientId = clientId;
            UserId = userId;
        }
    }
}