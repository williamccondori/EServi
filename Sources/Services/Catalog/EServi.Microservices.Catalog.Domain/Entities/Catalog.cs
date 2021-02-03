using System;
using EServi.Microservices.Catalog.Domain.Entities.Base;

namespace EServi.Microservices.Catalog.Domain.Entities
{
    public class Catalog : IDocument
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
    }
}