using Kwetter.Services.Shared.Messaging.Interfaces;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kwetter.Services.Shared.Messaging
{
    internal class MessagePublisher : IMessagePublisher
    {
        RabbitMQConnector _connection;
        public MessagePublisher(RabbitMQConnector connector)
        {
            _connection = connector;
        }

        public Task PublishMessageAsync<T>(string messageType, T value)
        {
            using var channel = _connection.CreateChannel();
            var message = channel.CreateBasicProperties();
            message.ContentType = "application/json";
            message.DeliveryMode = 2;
            // Add a MessageType header, this part is crucial for our solution because it is our way of distinguishing messages
            message.Headers = new Dictionary<string, object> { ["MessageType"] = messageType };
            var body = JsonSerializer.SerializeToUtf8Bytes(value);

            channel.BasicPublish("Kwetter", string.Empty, message, body);
            return Task.CompletedTask;
        }
    }
}
