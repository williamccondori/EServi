using System.Text;
using System.Text.Json;
using EServi.Microservices.User.Infrastructure.RabbitMq.Publishers.Auth.Models;
using EServi.RabbitMq;
using RabbitMQ.Client;

namespace EServi.Microservices.User.Infrastructure.RabbitMq.Publishers.Auth.Publishers
{
    public class AuthPublisher : IAuthPublisher
    {
        private const string QueueName = "auth--register-auth";

        private readonly IConnection _connection;

        public AuthPublisher(IRabbitMqClient rabbitMqClient)
        {
            _connection = rabbitMqClient.Connect();
        }

        public void RegisterAuth(AuthRegister authRegister)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(QueueName, true, false, false, null);

            var message = JsonSerializer.Serialize(authRegister);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(string.Empty, QueueName, properties, body);
        }
    }
}