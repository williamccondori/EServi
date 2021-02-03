using System.Text;
using System.Text.Json;
using EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers.Email.Models;
using EServi.RabbitMq;
using RabbitMQ.Client;

namespace EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers.Email.Publishers
{
    public class EmailPublisher : IEmailPublisher
    {
        private const string QueueName = "email--send-activation-code";

        private readonly IConnection _connection;

        public EmailPublisher(IRabbitMqClient rabbitMqClient)
        {
            _connection = rabbitMqClient.Connect();
        }
        
        public void SendActivationCode(ActivationCodeEmail activationCodeEmail)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(QueueName, true, false, false, null);

            var message = JsonSerializer.Serialize(activationCodeEmail);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(string.Empty, QueueName, properties, body);
        }
    }
}