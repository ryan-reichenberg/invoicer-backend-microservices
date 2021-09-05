using System;
using System.Collections.Generic;
using Invoicer.Common.Types.DDD;
using OpenTracing.Tag;
using ProjectsService.Messages.Events.Todos;

namespace ProjectsService.Domain
{
    public class Todo : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public TodoStatus Status { get; private set; }
        
        public List<TodoTag> Tags { get; private set; }

        public Todo(AggregateId id, string name, string description, TodoStatus status, List<TodoTag> tags)
        {
            Id = id;
            Name = name;
            Description = description;
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

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Description)}: {Description}, {nameof(Status)}: {Status}, {nameof(Tags)}: {Tags}";
        }
    }
}