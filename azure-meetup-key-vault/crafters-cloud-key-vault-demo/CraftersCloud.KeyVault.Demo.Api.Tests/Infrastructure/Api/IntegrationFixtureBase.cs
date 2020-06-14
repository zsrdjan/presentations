using System.Net.Http;
using Autofac.Extensions.DependencyInjection;
using CraftersCloud.KeyVault.Demo.Api.Tests.Infrastructure.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CraftersCloud.KeyVault.Demo.Api.Tests.Infrastructure.Api
{
    public class IntegrationFixtureBase
    {
        private IConfiguration _configuration = null!;
        private TestServer _server = null!;
        private IServiceScope _testScope = null!;
        protected HttpClient Client = null!;
        private bool _keyVaultEnabled;

        [SetUp]
        protected void Setup()
        {
            _configuration = new TestConfigurationBuilder()
                .WithKeyVaultEnabled(_keyVaultEnabled)
                .Build();

            IWebHostBuilder webHostBuilder = new WebHostBuilder()
                .ConfigureServices(services => services.AddAutofac())
                .UseConfiguration(_configuration)
                .UseStartup<TestStartup>();

            _server = new TestServer(webHostBuilder);
            Client = _server.CreateClient();
            _testScope = CreateScope();
        }

        protected void EnableKeyVault()
        {
            _keyVaultEnabled = true;
        }

        private IServiceScope CreateScope()
        {
            return _server.Host.Services.CreateScope();
        }

        protected T Resolve<T>()
        {
            return _testScope.Resolve<T>();
        }

        private static void WriteLine(string message)
        {
            TestContext.WriteLine(message);
        }
    }
}