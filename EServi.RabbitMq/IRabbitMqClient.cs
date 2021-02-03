using RabbitMQ.Client;

namespace EServi.RabbitMq
{
    public interface IRabbitMqClient
    {
        IConnection Connect();
    }
}