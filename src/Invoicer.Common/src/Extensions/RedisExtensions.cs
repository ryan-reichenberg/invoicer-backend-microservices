using System;
using Invoicer.Common.Redis;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Invoicer.Common.Extensions
{
    public static class RedisExtensions
    {
        private const string SectionName = "redis";
        private const string RegistryName = "persistence.redis";

        public static IInitializationContainer AddRedis(this IInitializationContainer container, string sectionName = SectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                sectionName = SectionName;
            }

            var options = container.GetOptions<RedisOptions>(sectionName);
            return container.AddRedis(options);
        }

        public static IInitializationContainer AddRedis(this IInitializationContainer container,
            Func<IRedisOptionsBuilder, IRedisOptionsBuilder> buildOptions)
        {
            var options = buildOptions(new RedisOptionsBuilder()).Build();
            return container.AddRedis(options);
        }

        public static IInitializationContainer AddRedis(this IInitializationContainer container, RedisOptions options)
        {

            if (!container.TryRegister(RegistryName))
            {
                return container;
            }

            container.Services
                .AddSingleton(options)
                .AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(options.ConnectionString))
                .AddTransient(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase(options.Database))
                .AddStackExchangeRedisCache(o =>
                {
                    o.Configuration = options.ConnectionString;
                    o.InstanceName = options.Instance;
                });

            return container;
        }
    }
}