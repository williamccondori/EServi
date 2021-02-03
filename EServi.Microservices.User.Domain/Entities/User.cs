using System;
using EServi.Microservices.User.Domain.Entities.Base;

namespace EServi.Microservices.User.Domain.Entities
{
    public class User : IDocument
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string Email { get; private set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public string Resume { get; set; }

        public static User Create(string name, string lastName, string phone, string email)
        {
            return new User
            {
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserCreated = email,
                UserUpdated = email,
                Name = name,
                LastName = lastName,
                Phone = phone,
                Email = email
            };
        }
    }
}