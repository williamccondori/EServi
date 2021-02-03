using EServi.Microservices.User.Domain.Repositories;
using EServi.Microservices.User.Infrastructure.MongoDb.Contexts;
using EServi.Microservices.User.Infrastructure.MongoDb.Repositories;
using EServi.Microservices.User.Infrastructure.RabbitMq.Publishers.Auth.Publishers;
using EServi.Microservices.User.UseCase.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EServi.Microservices.User.IoC
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(s => new
                MongoDbContext("52.152.98.91", "27017", "root", "ficticio", "eservi-user"));

            // Publishers
            serviceCollection.AddScoped<IAuthPublisher, AuthPublisher>();

            // Repositories.
            serviceCollection.AddTransient<IUserRepository, UserRepository>();

            // Services.
            serviceCollection.AddTransient<IUserService, UserService>();
        }
    }
}