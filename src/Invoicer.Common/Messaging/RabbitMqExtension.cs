using System;
using System.Net;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Invoicer.Common.Messaging
{
    public static class RabbitMqExtension
    {
        private static RabbitMqConnectionSettings _connectionSettings;

        public static void UseRabbitMqPubSub(this IServiceCollection services, IConfiguration config)
        {
            UserRabbitMqPublisher(services,config);
            UseRabbitMqSubscriber(services, config);
        }

        public static void UseRabbitMqSubscriber(this IServiceCollection services, IConfiguration config)
        {
            if (_connectionSettings == null)
            {
                _connectionSettings = ValidateConfig(config, "RabbitMqConnectionSettings");
            }

            services.AddTransient<ISubscriber>(_ => new RabbitMqSubscriber(_connectionSettings));
        }

        public static void UserRabbitMqPublisher(this IServiceCollection services, IConfiguration config)
        {
            if (_connectionSettings == null)
            {
                _connectionSettings = ValidateConfig(config, "RabbitMqConnectionSettings");
            }
            services.AddTransient<IPublisher>(_ => new RabbitMqPublisher(_connectionSettings));
        }

        private static RabbitMqConnectionSettings ValidateConfig(IConfiguration config, string sectionName)
        {
            RabbitMqConnectionSettings settings = new RabbitMqConnectionSettings();
            foreach (PropertyInfo property in typeof(RabbitMqConnectionSettings).GetProperties())
            {
                string value = config.GetSection(sectionName)[property.Name];
                if (!String.IsNullOrEmpty(value))
                {
                    property.SetValue(settings, value);
                }
                else
                {
                    property.SetValue(settings, "");
                }
            }
            return settings;
        }

    }
}