using System;
using System.Collections.Generic;
using Invoicer.Common;
using Newtonsoft.Json;
using ProjectsService.DTO;

namespace ProjectsService.Messages.Queries.Todos
{
    public class GetTodosForProjectQuery : IQuery<List<TodoDto>>
    {
        public Guid Id { get; set; }

        [JsonConstructor]
        public GetTodosForProjectQuery()
        {
        }

    }
}