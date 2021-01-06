using System.Collections.Generic;

namespace Invoicer.Common.RabbitMq.Contexts
{
    public interface IContextProvider
    {
        string HeaderName { get; }
        object Get(IDictionary<string, object> headers);
    }
}