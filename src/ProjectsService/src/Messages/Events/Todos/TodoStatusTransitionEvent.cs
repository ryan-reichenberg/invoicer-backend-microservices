using System.Text.Json.Serialization;
using Invoicer.Common;
using Invoicer.Common.Types.DDD;
using ProjectsService.Domain;

namespace ProjectsService.Messages.Events.Todos
{
    public class TodoStatusTransitionEvent : IEvent
    {
        public AggregateId Id { get; set; }
        public TodoStatus PreviousStatus { get; set; }
        public TodoStatus TransitionedStatus { get; set; }

        [JsonConstructor]
        public TodoStatusTransitionEvent(AggregateId id, TodoStatus previousStatus, TodoStatus transitionedStatus)
        {
            Id = id;
            PreviousStatus = previousStatus;
            TransitionedStatus = transitionedStatus;
        }
    }
}