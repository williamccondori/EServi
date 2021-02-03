using EServi.Consul;
using EServi.Microservices.Auth.Infrastructure.Jwt;
using EServi.Microservices.Auth.IoC;
using EServi.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EServi.Microservices.Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RabbitMqOptions>(Configuration.GetSection("RabbitMq"));
            services.Configure<ConsulOptions>(Configuration.GetSection("Consul"));
            services.Configure<JwtOptions>(Configuration.GetSection("Jwt"));
            
            services.AddControllers();
            services.AddRabbitMq();
            services.AddConsul();

            ServiceConfiguration.Configure(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseConsul();
        }
    }
}