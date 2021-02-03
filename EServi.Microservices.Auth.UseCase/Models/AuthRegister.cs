namespace EServi.Microservices.Auth.UseCase.Models
{
    public class AuthRegister
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public (string, string) Password { get; set; }
    }
}