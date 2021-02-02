using System;

namespace EServi.Microservices.User.Domain.Entities.Base
{
    public interface IDocument
    {
        string Id { get; set; }
        bool IsActive { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        string UserCreated { get; set; }
        string UserUpdated { get; set; }
    }
}