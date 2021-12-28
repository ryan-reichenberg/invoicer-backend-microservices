using System;
using Convey.Types;
using Invoicer.Common.Types;

namespace ProjectsService.DTO
{
    public class TodoTagDto: IIdentifiable<Guid>
    {
        public Guid Id { get;   set; }
        public string Name { get;  set; }
        public string Color { get;  set; }
        

        public TodoTagDto(Guid id, string name, string color)
        {
            Id = id;
            Name = name;
            Color = color;
        }
    }
}