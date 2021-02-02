using EServi.Microservices.Auth.Infrastructure.Jwt;
using EServi.Microservices.Auth.Infrastructure.Jwt.Builders;
using EServi.Microservices.Auth.Infrastructure.Jwt.Builders.Implementations;
using EServi.Microservices.Auth.UseCase.Services;
using EServi.Microservices.Auth.UseCase.Services.Implementations;
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
            services.AddControllers();

            services.AddScoped<IAuthService, AuthService>();

            services.Configure<JwtOptions>(o =>
            {
                o.ExpiryMinutes = 30;
                o.SecretId = "8Zz5tw0Ionm3XPZZfN0NOml3z9FMfmpgXwovR9fp6ryDIoGRM8EPHAB6iHsc0fb";
            });

            services.AddScoped<IJwtBuilder, JwtBuilder>();
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