using EServi.Consul;
using EServi.Microservices.Catalog.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EServi.Microservices.Catalog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<RabbitMqOptions>(Configuration.GetSection("RabbitMq"));
            //services.Configure<ConsulOptions>(Configuration.GetSection("Consul"));

            var connectionString = Configuration.GetConnectionString("CatalogConnection");
            services.AddOptions<string>(connectionString);
            
            services.AddControllers();
            //services.AddRabbitMq();
            //services.AddConsul();
            
            ServiceConfiguration.Configure(services, connectionString);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
        }
    }
}