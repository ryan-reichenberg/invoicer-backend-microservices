using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Constants = Invoicer.Common.Utils.Constants;

namespace Invoicer.Common.Messaging
{
    public class RabbitMqSubscriber : ISubscriber
    {
        private IConnection _connection;
        private IModel _model;
        private ISubscriberCallback _callback;
        private AsyncEventingBasicConsumer _consumer;
        private string _consumerTag;
        private RabbitMqConnectionSettings _settings; 

        public RabbitMqSubscriber(RabbitMqConnectionSettings settings)
        {
            _settings = settings;
        }

        public void Start(ISubscriberCallback callback)
        {
            _callback = callback;
            ConnectAndConfigure(_settings);
        }

        public void Stop()
        {
            _model.BasicCancel(_consumerTag);
            _model.Close(200, "Closing.");
            _connection.Close();
        }

        private void ConnectAndConfigure(RabbitMqConnectionSettings settings)
        {
            var factory = new ConnectionFactory() { UserName = settings.UserName, Password = settings.Password, DispatchConsumersAsync = true, Port = Constants.RabbitMqDefaultPort };
            _connection = factory.CreateConnection(settings.Host);
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(settings.Exchange, ExchangeType.Fanout, true);
            _model.QueueDeclare(settings.Queue,  true, autoDelete: false, exclusive: false);
            _model.QueueBind(settings.Queue, settings.Exchange, "");
            _consumer = new AsyncEventingBasicConsumer(_model);
            _consumer.Received += Consumer_Received;
            _consumerTag = _model.BasicConsume(settings.Queue, false, _consumer);
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            if (await HandleEvent(@event))
            {
                _model.BasicAck(@event.DeliveryTag, false);
            }
        }

        private Task<bool> HandleEvent(BasicDeliverEventArgs @event)
        {
            string body = Encoding.UTF8.GetString(@event.Body.ToArray());
            // call callback to handle the message
            return _callback.HandleEventAsync( body);
        }
    }
}