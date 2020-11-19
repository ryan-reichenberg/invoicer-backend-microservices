using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicer.Common.Messaging
{
    public static class RabbitMqConnector
    {
        private static string _host;
        private static string _userName;
        private static string _password;
        private static string _exchange;
        private static string _queue;
        private static string _routingKey;
        private static int _port;
        
        public static void UseRabbitMqSubscriber(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ISubscriber>(_ => new RabbitMqSubscriber(validateConfig(config, "RabbitMqConnection")));
        }

        public static void UserRabbitMqPublisher(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IPublisher>(_ => new RabbitMqPublisher(validateConfig(config, "RabbitMqConnection")));
        }

        private static RabbitMqConnectionSettings validateConfig(IConfiguration config, string sectionName)
        {
            RabbitMqConnectionSettings settings = new RabbitMqConnectionSettings();
            return settings;
        }

    }
}