using System;

namespace ProjectsService.Domain
{
    public class TodoTag
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Color { get; private set; }
        

        public TodoTag(Guid id, string name, string color)
        {
            Id = id;
            Name = name;
            Color = color;
        }
    }
}