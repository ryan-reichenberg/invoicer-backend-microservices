using System.Collections.Generic;
using Invoicer.Common.Types.DDD;
using OpenTracing.Tag;

namespace ProjectsService.Domain
{
    public class Todo : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ProjectId { get; private set; }
        public TodoStatus Status { get; private set; }
        
        public List<TodoTag> Tags { get; private set; }

        public Todo(AggregateId id, string name, string description, string projectId, TodoStatus status, List<TodoTag> tags)
        {
            Id = id;
            Name = name;
            Description = description;
            ProjectId = projectId;
            Status = status;
            Tags = tags;
        }

        public void ChangeTodoStatus(TodoStatus status)
        {
            Status = status;
        }

        public void AddNewTag(TodoTag tag)
        {
            Tags.Add(tag);
        }
    }
}