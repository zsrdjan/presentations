using Autofac;
using CraftersCloud.KeyVault.Demo.Infrastructure.AspNetCore.Init;
using CraftersCloud.KeyVault.Demo.Infrastructure.AspNetCore.Logging;
using CraftersCloud.KeyVault.Demo.Infrastructure.Configuration;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CraftersCloud.KeyVault.Demo.Api
{
    [UsedImplicitly]
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [UsedImplicitly]
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            if (Configuration.AppUseDeveloperExceptionPage()) app.UseDeveloperExceptionPage();

            if (env.IsDevelopment())
                app.UseCors(builder => builder
                    .WithOrigins("http://localhost:4200")
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            else
                app.UseHttpsRedirection();

            app.UseMiddleware<LogContextMiddleware>();
            app.UseHsts();

            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });

            app.AppUseSwagger();
        }

        [UsedImplicitly]

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureServicesExceptMvc(services, Configuration);
            services.AppAddMvc(Configuration);
            services.AddApplicationInsightsTelemetry(Configuration.ReadApplicationInsightsSettings().InstrumentationKey);
        }

        // this also called by tests. Mvc is configured slightly differently in integration tests
        public static void ConfigureServicesExceptMvc(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddApplicationInsightsTelemetry();

            services.AppAddSettings(configuration);
            services.AppAddAutoMapper();
            services.AppAddMediatR();
            services.AppAddSwagger("KeyVault Demo API");
        }

        [UsedImplicitly]
        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AppRegisterModules(Configuration);
        }
    }
}