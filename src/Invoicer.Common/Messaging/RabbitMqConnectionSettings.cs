namespace Invoicer.Common.Messaging
{
    public class RabbitMqConnectionSettings
    {
        public string Host { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Exchange { get; set; }

        public string Queue { get; set; }

        public string RoutingKey { get; set; }

        public int Port { get; set; }
    }
}