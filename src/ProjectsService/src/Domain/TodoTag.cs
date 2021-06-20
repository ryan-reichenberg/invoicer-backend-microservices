namespace ProjectsService.Domain
{
    public class TodoTag
    {
        public string Name { get; private set; }
        public string Color { get; private set; }

        public TodoTag(string name, string color)
        {
            Name = name;
            Color = color;
        }
    }
}