using System;
using RabbitMQ.Client;

namespace EServi.RabbitMq
{
    public class RabbitMqClient
    {
        private readonly string _hostname;
        private readonly string _username;
        private readonly string _password;

        public RabbitMqClient(string hostname, string username, string password)
        {
            _hostname = hostname;
            _username = username;
            _password = password;
        }

        public IConnection Connect()
        {
            IConnection connection;
            try
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = _hostname,
                    Port = AmqpTcpEndpoint.UseDefaultPort,
                    UserName = _username,
                    Password = _password,
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