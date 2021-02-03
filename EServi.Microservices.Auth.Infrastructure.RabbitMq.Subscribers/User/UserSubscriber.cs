using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EServi.Microservices.Auth.UseCase.Models;
using EServi.Microservices.Auth.UseCase.Services;
using EServi.RabbitMq;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EServi.Microservices.Auth.Infrastructure.RabbitMq.Subscribers.User
{
    public class UserSubscriber : BackgroundService
    {
        private const string QUEUE_NAME = "send-activation-code--email";

        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly IAuthService _authService;

        public UserSubscriber(IOptions<RabbitMqOptions> options, IAuthService authService)
        {
            _authService = authService;

            var connectionOptions = options.Value;

            var rabbitClient = new RabbitMqClient(connectionOptions.Hostname,
                connectionOptions.Username, connectionOptions.Password);

            _connection = rabbitClient.Connect();

            _channel = _connection.CreateModel();
            _channel.QueueDeclare(QUEUE_NAME, true, false, false, null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var authRegister = JsonConvert.DeserializeObject<AuthRegister>(content);

                HandleMessage(authRegister);

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(QUEUE_NAME, false, consumer);

            return Task.CompletedTask;
        }

        private void HandleMessage(AuthRegister authRegister)
        {
            _authService.Register(authRegister);
        }

        public override void Dispose()
        {
            _connection.Close();
            base.Dispose();
        }
    }
}