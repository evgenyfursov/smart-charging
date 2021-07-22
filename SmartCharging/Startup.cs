using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SmartCharging.API;
using SmartCharging.Data.DbContext;
using SmartCharging.Data.Interfaces;
using SmartCharging.Data.Repositories;
using SmartCharging.Implementation;

namespace SmartCharging
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmartCharging", Version = "v1" });
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("smartCharging"));

            services.AddScoped<IDbContext, ApplicationDbContext>();

            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IChargeStationRepository, ChargeStationRepository>();
            services.AddScoped<IConnectorRepository, ConnectorRepository>();

            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IChargeStationService, ChargeStationService>();
            services.AddScoped<IConnectorService, ConnectorService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartCharging v1"));
            }

            DbContextInitializer.Initialize(context);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
