namespace Invoicer.Common.Messaging
{
    public class RabbitMqConnectionSettings
    {
        public string Host { get; set; } = "localhost";

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Exchange { get; set; }

        public string Queue { get; set; }
    }
}