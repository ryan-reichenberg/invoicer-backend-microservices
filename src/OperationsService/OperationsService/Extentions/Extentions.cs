using System;
using Convey;
using Convey.Persistence.Redis;
using Microsoft.Extensions.DependencyInjection;
using OperationsService.Types;

namespace OperationsService.Extentions
{
    public static class Extentions
    {
        public static string ToUserGroup(this Guid userId) => userId.ToString("N").ToUserGroup();
        public static string ToUserGroup(this string userId) => $"users:{userId}";


        public static IConveyBuilder AddSignalR(this IConveyBuilder builder)
        {
            var options = builder.GetOptions<SignalrOptions>("signalR");
            builder.Services.AddSingleton(options);
            var signalR = builder.Services.AddSignalR();
            if (!options.Backplane.Equals("redis", StringComparison.InvariantCultureIgnoreCase))
            {
                return builder;
            }

            var redisOptions = builder.GetOptions<RedisOptions>("redis");
            signalR.AddStackExchangeRedis(redisOptions.ConnectionString);

            return builder;
        }
    }
}