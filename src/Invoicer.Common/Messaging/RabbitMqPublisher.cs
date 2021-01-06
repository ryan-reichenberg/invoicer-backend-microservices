using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Constants = Invoicer.Common.Utils.Constants;

namespace Invoicer.Common.Messaging
{
    public class RabbitMqPublisher : IPublisher
    {
        private IConnection _connection;
        private IModel _model;
        private RabbitMqConnectionSettings _settings;
        public RabbitMqPublisher(RabbitMqConnectionSettings settings)
        {
            _settings = settings;
            ConnectAndConfigure();
        }

        public Task PublishMessageAsync(string message, String routingKey = "")
        {
            return Task.Run(() =>
            {
                // string data = MessageSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(message);
                IBasicProperties properties = _model.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "MessageType", "Test" } };
                _model.BasicPublish(_settings.Exchange, routingKey, properties, body);
            });


        }

        private void ConnectAndConfigure()
        {
            var factory = new ConnectionFactory()
            {
                UserName = _settings.UserName, Password = _settings.Password, Port = Constants.RabbitMqDefaultPort,
                AutomaticRecoveryEnabled = true
            };
            _connection = factory.CreateConnection(_settings.Host);
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(_settings.Exchange, ExchangeType.Fanout, true);
        }
    }
}