using System;
using System.Runtime.CompilerServices;
using Autofac.Extensions.DependencyInjection;
using CraftersCloud.KeyVault.Demo.Infrastructure.AspNetCore.Init;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Serilog;

[assembly: InternalsVisibleTo("CraftersCloud.KeyVault.Demo.Api.Tests")]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
[assembly: ApiController]
namespace CraftersCloud.KeyVault.Demo.Api
{
    [UsedImplicitly]
    public class Program
    {
        public static void Main(string[] args)
        {
            SerilogProgramHelper.AppConfigureSerilog();
            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.Information("Stopping web host");
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(options => { options.AddServerHeader = false; })
                        .UseStartup<Startup>()
                        .UseSerilog();
                });
        }
    }
}