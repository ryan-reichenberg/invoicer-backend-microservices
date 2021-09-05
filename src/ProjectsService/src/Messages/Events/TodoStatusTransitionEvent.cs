using System.Text.Json.Serialization;
using Invoicer.Common;
using Invoicer.Common.RabbitMq.Attributes;
using Invoicer.Common.Types.DDD;
using ProjectsService.Domain;

namespace ProjectsService.Messages.Events.Todos
{
    [Message("todos")]
    public class TodoStatusTransitionEvent : IEvent
    {
        public AggregateId Id { get; set; }
        public TodoStatus PreviousStatus { get; set; }
        public TodoStatus TransitionedStatus { get; set; }

        [JsonConstructor]
        public TodoStatusTransitionEvent()
        {
        }
    }
}