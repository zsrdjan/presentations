﻿using CraftersCloud.KeyVault.Demo.Infrastructure.MediatR;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CraftersCloud.KeyVault.Demo.Api.Infrastructure.Init
{
    public static class MediatRStartupExtensions
    {
        public static void AppAddMediatR(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            //services.AddScoped(typeof(IRequestPreProcessor<>), typeof(SamplePreRequestBehavior<>));
            //services.AddScoped(typeof(IRequestPostProcessor<,>), typeof(SamplePostRequestBehavior<,>));

            services.AddMediatR(
                AssemblyFinder.ApiAssembly);
        }
    }
}