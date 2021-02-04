using System;
using System.Threading.Tasks;
using EServi.Microservices.Catalog.Domain.Entities;

namespace EServi.Microservices.Catalog.Domain.Repositories
{
    public interface IPostRepository
    {
        Task<Post> Create(Post post);
        Task<Post> GetById(Guid id);
    }
}