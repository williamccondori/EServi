using EServi.Microservices.Auth.Domain.Repositories;
using EServi.Microservices.Auth.Infrastructure.Jwt.Builders;
using EServi.Microservices.Auth.Infrastructure.MongoDb.Contexts;
using EServi.Microservices.Auth.Infrastructure.MongoDb.Repositories;
using EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers.Email.Publishers;
using EServi.Microservices.Auth.Infrastructure.RabbitMq.Subscribers.User;
using EServi.Microservices.Auth.UseCase.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EServi.Microservices.Auth.IoC
{
    public static class ServiceConfiguration
    {
        public static void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(s => new
                MongoDbContext("52.152.98.91", "27017", "root", "ficticio", "eservi-auth"));

            // Publishers
            serviceCollection.AddScoped<IEmailPublisher, EmailPublisher>();

            // Subscribers.
            serviceCollection.AddHostedService<UserSubscriber>();

            // JWT Builders.
            serviceCollection.AddScoped<IJwtBuilder, JwtBuilder>();

            // Repositories.
            serviceCollection.AddTransient<ICodeRepository, CodeRepository>();
            serviceCollection.AddTransient<IIdentityRepository, IdentityRepository>();

            // Services.
            serviceCollection.AddScoped<IAuthService, AuthService>();
        }
    }
}