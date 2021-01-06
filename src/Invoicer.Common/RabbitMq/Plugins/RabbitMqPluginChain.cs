using System;

namespace Invoicer.Common.RabbitMq.Plugins
{
    internal sealed class RabbitMqPluginChain
    {
        public Type PluginType { get; set; }
        public IRabbitMqPlugin Plugin { get; set; }
    }
}