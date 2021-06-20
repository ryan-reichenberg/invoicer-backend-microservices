namespace ProjectsService.DTO
{
    public class TodoTagDto
    {
        public string Name { get; private set; }
        public string Color { get; private set; }

        public TodoTagDto(string name, string color)
        {
            Name = name;
            Color = color;
        }
    }
}