using System;

namespace EServi.Microservices.Catalog.Domain.Entities.Base
{
    public interface IEntity
    {
        Guid Id { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        string UserCreated { get; set; }
        string UserUpdated { get; set; }
    }
}