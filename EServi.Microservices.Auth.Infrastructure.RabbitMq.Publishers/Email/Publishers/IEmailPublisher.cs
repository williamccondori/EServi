using EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers.Email.Models;

namespace EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers.Email.Publishers
{
    public interface IEmailPublisher
    {
        void SendActivationCode(ActivationCodeEmail activationCodeEmail);
    }
}