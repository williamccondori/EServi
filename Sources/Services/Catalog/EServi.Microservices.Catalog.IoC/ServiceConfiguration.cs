using EServi.Microservices.Catalog.Domain.Repositories;
using EServi.Microservices.Catalog.Infrastructure.RabbitMq.Publishers.Search.Publishers;
using EServi.Microservices.Catalog.UseCase.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EServi.Microservices.Catalog.IoC
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddSingleton(s => new
            //    MongoDbContext("52.152.98.91", "27017", "root", "ficticio", "eservi-user"));

            // Publishers
            serviceCollection.AddScoped<ISearchPublisher, SearchPublisher>();

            // Repositories.
            //serviceCollection.AddTransient<ICatalogRepository, CatalogRepository>();

            // Services.
            serviceCollection.AddTransient<ICatalogService, CatalogService>();
        }
    }
}