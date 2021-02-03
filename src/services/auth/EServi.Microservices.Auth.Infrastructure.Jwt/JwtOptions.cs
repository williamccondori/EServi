namespace EServi.Microservices.Auth.Infrastructure.Jwt
{
    public class JwtOptions
    {
        public string SecretId { get; set; }
        public double ExpiryMinutes { get; set; }
    }
}