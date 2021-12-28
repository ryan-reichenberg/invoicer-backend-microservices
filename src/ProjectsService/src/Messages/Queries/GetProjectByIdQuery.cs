using System;
using Convey.CQRS.Queries;
using Invoicer.Common;
using Newtonsoft.Json;
using ProjectsService.Domain;

namespace ProjectsService.Messages.Queries
{
    public class GetProjectByIdQuery :IQuery<Project>
    {
        public Guid Id { get; set; }

        [JsonConstructor]
        public GetProjectByIdQuery()
        {
        }
    }
}