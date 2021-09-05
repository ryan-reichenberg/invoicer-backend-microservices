using System.Collections.Generic;
using Invoicer.Common.Types.DDD;

namespace ProjectsService.Domain
{
    public class Project : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<Todo> Todos { get; private set; }
        public string ClientId { get; private set; }
        public string UserId { get; private set; }

        public Project(AggregateId id, string name, string description, List<Todo> todos, string clientId, string userId)
        {
            Id = id;
            Name = name;
            Description = description;
            Todos = todos;
            ClientId = clientId;
            UserId = userId;
        }
        
        public void AddNewTodo(Todo todo)
        {
            Todos.Add(todo);
        }

    }
}