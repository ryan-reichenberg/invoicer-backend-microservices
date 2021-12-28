using System;
using System.Collections.Generic;
using Convey.CQRS.Queries;
using Invoicer.Common;
using Newtonsoft.Json;
using ProjectsService.Domain;

namespace ProjectsService.Messages.Queries
{
    public class GetTodosForProjectQuery : IQuery<List<Todo>>
    {
        public string Id { get; set; }

        [JsonConstructor]
        public GetTodosForProjectQuery()
        {
        }

    }
}