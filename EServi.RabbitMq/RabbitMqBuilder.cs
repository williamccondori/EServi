using System;
using Microsoft.Extensions.DependencyInjection;

namespace EServi.RabbitMq
{
    public static class RabbitMqBuilder
    {
        public static void AddRabbitMq(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IRabbitMqClient, RabbitMqClient>();
        }
    }
}