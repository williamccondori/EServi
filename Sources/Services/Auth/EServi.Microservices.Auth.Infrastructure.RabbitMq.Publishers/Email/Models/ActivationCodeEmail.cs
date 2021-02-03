namespace EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers.Email.Models
{
    public class ActivationCodeEmail
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ActivationCode { get; set; }
    }
}