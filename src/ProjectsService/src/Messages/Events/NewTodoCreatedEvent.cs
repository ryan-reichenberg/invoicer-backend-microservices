using System.Collections.Generic;
using System.Text.Json.Serialization;
using Invoicer.Common;
using Invoicer.Common.RabbitMq.Attributes;
using Invoicer.Common.Types.DDD;
using ProjectsService.Domain;

namespace ProjectsService.Messages.Events.Todos
{
    [Message("todos")]
    public class NewTodoCreatedEvent : IEvent
    {
        public AggregateId Id { get; set; }
        public List<TodoTag> Tags { get; set; }

        [JsonConstructor]
        public NewTodoCreatedEvent()
        {
        }
    }
}