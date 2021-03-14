using System;

namespace Invoicer.Common.RabbitMq
{
     public interface IExceptionToMessageMapper
        {
            object Map(Exception exception, object message);
        }
}