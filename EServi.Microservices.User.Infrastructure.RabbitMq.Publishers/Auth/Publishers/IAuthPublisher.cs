using EServi.Microservices.User.Infrastructure.RabbitMq.Publishers.Auth.Models;

namespace EServi.Microservices.User.Infrastructure.RabbitMq.Publishers.Auth.Publishers
{
    public interface IAuthPublisher
    {
        void RegisterAuth(AuthRegister authRegister);
    }
}