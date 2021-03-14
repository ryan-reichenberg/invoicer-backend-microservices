using System;

namespace Invoicer.Common.RabbitMq.Conventions
{
    public interface IConventionsProvider
    {
        IConventions Get<T>();
        IConventions Get(Type type);
    }
}