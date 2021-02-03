using System;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EServi.RabbitMq
{
    public class RabbitMqClient : IRabbitMqClient
    {
        private readonly RabbitMqOptions _options;

        public RabbitMqClient(IOptions<RabbitMqOptions> options)
        {
            _options = options.Value;
        }

        public IConnection Connect()
        {
            IConnection connection;
            try
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = _options.Hostname,
                    Port = AmqpTcpEndpoint.UseDefaultPort,
                    UserName = _options.Username,
                    Password = _options.Password,
                    VirtualHost = "/"
                };

                connection = connectionFactory.CreateConnection();
            }
            catch (Exception)
            {
                connection = null;
            }

            return connection;
        }
    }
}