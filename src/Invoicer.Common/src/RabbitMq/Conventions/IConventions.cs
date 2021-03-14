using System;

namespace Invoicer.Common.RabbitMq.Conventions
{
    public interface IConventions
    {
        Type Type { get; }
        string RoutingKey { get; }
        string Exchange { get; }
        string Queue { get; }
    }
}