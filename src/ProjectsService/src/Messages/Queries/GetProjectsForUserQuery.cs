using System;
using System.Collections.Generic;
using Invoicer.Common;
using Newtonsoft.Json;
using ProjectsService.Domain;

namespace ProjectsService.Messages.Queries
{
    public class GetProjectsForUserQuery : IQuery<List<Project>>
    {
        public Guid Id { get; set; }

        [JsonConstructor]
        public GetProjectsForUserQuery()
        {
        }
    }
}