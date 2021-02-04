using System;
using System.Threading.Tasks;
using EServi.Microservices.Catalog.Domain.Entities;
using EServi.Microservices.Catalog.Domain.Repositories;
using EServi.Microservices.Catalog.Infrastructure.PostgreSql.Contexts;

namespace EServi.Microservices.Catalog.Infrastructure.PostgreSql.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly CatalogContext _catalogContext;

        public PostRepository(CatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Post> Create(Post post)
        {
            var task = await _catalogContext.Posts.AddAsync(post);
            return task.Entity;
        }

        public async Task<Post> GetById(Guid id)
        {
            return await _catalogContext.Posts.FindAsync(id);
        }
    }
}