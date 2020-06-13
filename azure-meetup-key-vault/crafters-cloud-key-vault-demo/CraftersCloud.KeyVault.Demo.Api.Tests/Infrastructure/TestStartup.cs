using Autofac;
using CraftersCloud.KeyVault.Demo.Api.Tests.Infrastructure.Autofac;
using CraftersCloud.KeyVault.Demo.Infrastructure.AspNetCore.Init;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CraftersCloud.KeyVault.Demo.Api.Tests.Infrastructure
{
    [UsedImplicitly]
    public class TestStartup
    {
        private readonly IConfiguration _configuration;
        private readonly Startup _startup;

        public TestStartup(IConfiguration configuration)
        {
            _configuration = configuration;
            _startup = new Startup(configuration);
        }

        [UsedImplicitly]
        public void ConfigureServices(IServiceCollection services)
        {
            services.AppAddMvc(_configuration)
                .AddApplicationPart(AssemblyFinder.ApiAssembly); // needed only because of tests
            Startup.ConfigureServicesExceptMvc(services, _configuration);
        }

        [UsedImplicitly]
        public void ConfigureContainer(ContainerBuilder builder)
        {
            _startup.ConfigureContainer(builder);
            builder.RegisterModule<TestModule>(); // this allows certain components to be overriden
        }

        [UsedImplicitly]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _startup.Configure(app, env);
        }
    }
}