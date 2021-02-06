using System;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EServi.Consul
{
    public static class ConsulBuilder
    {
        public static void AddConsul(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IConsulClient>(p => new ConsulClient(o =>
            {
                var options = p.GetRequiredService<IOptions<ConsulOptions>>().Value;

                Console.WriteLine(options.HttpEndpoint);

                if (!string.IsNullOrEmpty(options.HttpEndpoint))
                {
                    o.Address = new Uri(options.HttpEndpoint);
                }
            }));
        }

        public static void UseConsul(this IApplicationBuilder app)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();
            var consulOptions = app.ApplicationServices.GetRequiredService<IOptions<ConsulOptions>>();
            var applicationLifetime = app.ApplicationServices.GetRequiredService<IApplicationLifetime>();

            var options = consulOptions.Value;

            var agentServiceRegistration = new AgentServiceRegistration
            {
                ID = Guid.NewGuid().ToString(),
                Address = $"{options.ServiceHost}",
                Port = options.ServicePort,
                Name = options.ServiceName,
                Check = new AgentCheckRegistration
                {
                    TCP = $"{options.ServiceHost}:{options.ServicePort}",
                    Notes = $"Runs a TCP check on port {options.ServicePort}",
                    Timeout = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(10)
                }
            };

            consulClient.Agent.ServiceRegister(agentServiceRegistration).GetAwaiter().GetResult();

            applicationLifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(agentServiceRegistration.ID).GetAwaiter().GetResult();
            });
        }
    }
}