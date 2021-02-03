using System;
using System.Linq;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
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

            if (!(app.Properties["server.Features"] is FeatureCollection features))
            {
                return;
            }

            var addresses = features.Get<IServerAddressesFeature>();

            var address = addresses.Addresses.First();

            var uri = new Uri(address);

            var options = consulOptions.Value;

            var agentServiceRegistration = new AgentServiceRegistration
            {
                ID = Guid.NewGuid().ToString(),
                Name = options.ServiceName,
                Address = $"{uri.Host}",
                Port = uri.Port
            };

            consulClient.Agent.ServiceRegister(agentServiceRegistration).GetAwaiter().GetResult();

            applicationLifetime.ApplicationStopping.Register(() =>
            {
                consulClient.Agent.ServiceDeregister(agentServiceRegistration.ID).GetAwaiter().GetResult();
            });
        }
    }
}