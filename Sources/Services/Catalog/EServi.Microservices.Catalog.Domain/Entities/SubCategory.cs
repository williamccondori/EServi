using System;
using EServi.Microservices.Catalog.Domain.Entities.Base;

namespace EServi.Microservices.Catalog.Domain.Entities
{
    public class SubCategory : IEntity
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public string Description { get; set; }
        public virtual Category Category { get; set; }
    }
}