using EServi.Microservices.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EServi.Microservices.Catalog.Infrastructure.PostgreSql.Contexts
{
    public class CatalogContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Post> Posts { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseHiLo();
            base.OnModelCreating(modelBuilder);
        }
    }
}