using System.Text;
using System.Text.Json;
using EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers.Email.Models;
using EServi.RabbitMq;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers.Email.Publishers.Implementations
{
    public class EmailPublisher : IEmailPublisher
    {
        private const string QUEUE_NAME = "send-activation-code--email";
        
        private readonly IConnection _connection;

        public EmailPublisher(IOptions<RabbitMqOptions> options)
        {
            var connectionOptions = options.Value;

            var rabbitClient = new RabbitMqClient(connectionOptions.Hostname,
                connectionOptions.Username, connectionOptions.Password);

            _connection = rabbitClient.Connect();
        }

        public void SendActivationCodeEmail(ActivationCodeEmail activationCodeEmail)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(QUEUE_NAME, true, false, false, null);

            var message = JsonSerializer.Serialize(activationCodeEmail);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(string.Empty, QUEUE_NAME, properties, body);
        }
    }
}