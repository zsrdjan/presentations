using CraftersCloud.KeyVault.Demo.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CraftersCloud.KeyVault.Demo.Api.Infrastructure.Init
{
    public static class SettingsStartupExtensions
    {
        public static void AppAddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            // Options for particular external services
            services.Configure<GitHubApiSettings>(configuration.GetSection("App:GitHubApi"));
        }
    }
}