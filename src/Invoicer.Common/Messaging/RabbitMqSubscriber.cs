using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Invoicer.Common.Messaging
{
    public class RabbitMqSubscriber : ISubscriber
    {
        private IConnection _connection;
        private IModel _model;
        private AsyncEventingBasicConsumer _consumer;
        private string _consumerTag;

        public RabbitMqSubscriber(RabbitMqConnectionSettings settings)
        {
        }

        public void start()
        {
            
        }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }
    }
}