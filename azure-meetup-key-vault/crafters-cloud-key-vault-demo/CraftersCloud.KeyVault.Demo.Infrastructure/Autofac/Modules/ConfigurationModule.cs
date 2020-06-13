using Autofac;
using CraftersCloud.KeyVault.Demo.Core.Settings;
using CraftersCloud.KeyVault.Demo.Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

namespace CraftersCloud.KeyVault.Demo.Infrastructure.Autofac.Modules
{
    public class ConfigurationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => c.Resolve<IConfiguration>().ReadAppSettings())
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<AppSettings>().GitHubApi)
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<IConfiguration>()
                    .ReadSettingsSection<ApplicationInsightsSettings>("ApplicationInsights"))
                .AsSelf()
                .SingleInstance();
        }
    }
}