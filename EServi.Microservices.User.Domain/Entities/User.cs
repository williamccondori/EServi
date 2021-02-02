using System;
using EServi.Microservices.User.Domain.Entities.Base;

namespace EServi.Microservices.User.Domain.Entities
{
    public class User : IDocument
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public string Lastname { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Salt { get; private set; }
        public bool IsEnabled { get; private set; }
        public bool IsActive { get; set; }

        public static User Create(string email, string password, string salt, string name, string lastname,
            string phone, bool isEnabled)
        {
            return new User
            {
                Username = email.Split("@")[0],
                Password = password,
                Salt = salt,
                Name = name,
                Lastname = lastname,
                Email = email,
                Phone = phone,
                IsEnabled = isEnabled,
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserCreated = email,
                UserUpdated = email
            };
        }
    }
}