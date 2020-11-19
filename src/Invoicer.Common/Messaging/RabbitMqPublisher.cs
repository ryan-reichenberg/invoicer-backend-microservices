using System.Collections.Generic;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Invoicer.Common.Messaging
{
    public class RabbitMqPublisher : IPublisher
    {
        private IConnection _connection;
        private IModel _model;
        public RabbitMqPublisher(RabbitMqConnectionSettings settings)
        {
        }
        
        public Task PublishMessage()
        {
            throw new System.NotImplementedException();
        }
    }
}