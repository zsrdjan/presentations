using Autofac;
using CraftersCloud.KeyVault.Demo.Infrastructure.Autofac.Modules;
using Microsoft.Extensions.Configuration;

namespace CraftersCloud.KeyVault.Demo.Api
{
    public static class ContainerBuilderStartupExtensions
    {
        public static void AppRegisterModules(this ContainerBuilder builder, IConfiguration configuration)
        {
            builder.RegisterModule<ConfigurationModule>();
        }
    }
}