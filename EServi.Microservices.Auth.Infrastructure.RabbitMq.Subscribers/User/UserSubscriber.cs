using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EServi.Microservices.Auth.UseCase.Models;
using EServi.Microservices.Auth.UseCase.Services;
using EServi.RabbitMq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EServi.Microservices.Auth.Infrastructure.RabbitMq.Subscribers.User
{
    public class UserSubscriber : BackgroundService
    {
        private const string QueueName = "auth--register-auth";

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public UserSubscriber(IRabbitMqClient rabbitMqClient, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            
            _connection = rabbitMqClient.Connect();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(QueueName, true, false, false, null);
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

            _channel.BasicConsume(QueueName, false, consumer);

            return Task.CompletedTask;
        }

        private async void HandleMessage(AuthRegister authRegister)
        {
            using var scope = _serviceScopeFactory.CreateScope();
                
            var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
            await authService.Register(authRegister);
        }

        public override void Dispose()
        {
            _connection.Close();
            base.Dispose();
        }
    }
}