using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace CraftersCloud.KeyVault.Demo.Infrastructure.AspNetCore.Init
{
    public static class AutoMapperStartupExtensions
    {
        public static void AppAddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AssemblyFinder.ApiAssembly);
        }
    }
}