using Microsoft.Extensions.DependencyInjection;

namespace CraftersCloud.KeyVault.Demo.Api.Tests.Infrastructure.Api
{
    public static class ServiceScopeExtensions
    {
        public static T Resolve<T>(this IServiceScope scope)
        {
            return scope.ServiceProvider.GetRequiredService<T>();
        }
    }
}