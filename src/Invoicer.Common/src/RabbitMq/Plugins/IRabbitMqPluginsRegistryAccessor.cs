using System.Collections.Generic;

namespace Invoicer.Common.RabbitMq.Plugins
{
    internal interface IRabbitMqPluginsRegistryAccessor
    {
        LinkedList<RabbitMqPluginChain> Get();
    }
}