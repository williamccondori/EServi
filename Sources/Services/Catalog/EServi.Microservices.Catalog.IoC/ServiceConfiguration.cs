using EServi.Microservices.Catalog.Domain.Repositories;
using EServi.Microservices.Catalog.Infrastructure.PostgreSql.Contexts;
using EServi.Microservices.Catalog.Infrastructure.PostgreSql.Repositories;
using EServi.Microservices.Catalog.Infrastructure.RabbitMq.Publishers.Search.Publishers;
using EServi.Microservices.Catalog.UseCase.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EServi.Microservices.Catalog.IoC
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<CatalogContext>(options =>
                options.UseNpgsql(connectionString));

            // Publishers
            serviceCollection.AddScoped<ISearchPublisher, SearchPublisher>();

            // Repositories.
            serviceCollection.AddTransient<IPostRepository, PostRepository>();

            // Services.
            serviceCollection.AddTransient<ICatalogService, CatalogService>();
        }
    }
}