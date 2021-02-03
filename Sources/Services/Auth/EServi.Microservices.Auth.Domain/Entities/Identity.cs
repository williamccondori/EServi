using System;
using EServi.Microservices.Auth.Domain.Entities.Base;

namespace EServi.Microservices.Auth.Domain.Entities
{
    public class Identity : IDocument
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public string UserId { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string SecretKey { get; private set; }
        public bool IsEnabled { get; private set; }

        public static Identity Create(string userId, string email, string passwordEncrypted, string secretKey)
        {
            return new Identity
            {
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserCreated = userId,
                UserUpdated = userId,
                UserId = userId,
                Email = email,
                Password = passwordEncrypted,
                SecretKey = secretKey,
                IsEnabled = false
            };
        }
    }
}