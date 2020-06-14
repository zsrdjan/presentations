using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace CraftersCloud.KeyVault.Demo.Api.Tests.Infrastructure.Configuration
{
    public class TestConfigurationBuilder
    {
        private bool _keyVaultEnabled;

        public IConfiguration Build()
        {
            var configurationBuilder = new ConfigurationBuilder();

            var dict = new Dictionary<string, string>
            {
                {"App:SampleKeyVaultSecret", "Value1FromInMemoryConfiguration"},
                {"App:GitHubApi:SuperSecretKey", "Value2FromInMemoryConfiguration"}
            };

            configurationBuilder.AddInMemoryCollection(dict);
            if (_keyVaultEnabled)
            {
                AddKeyVaultConfigurationProvider(configurationBuilder);
            }
            return configurationBuilder.Build();
        }

        private static void AddKeyVaultConfigurationProvider(IConfigurationBuilder configurationBuilder)
        {
            var keyVault = Environment.GetEnvironmentVariable("KEY_VAULT_NAME") ?? "cc-keyvault-demo";
            var accessToken = Environment.GetEnvironmentVariable("KEY_VAULT_ACCESS_TOKEN");

            var keyVaultClient = new KeyVaultClient(
                AuthenticationCallback(accessToken));
            
            configurationBuilder.AddAzureKeyVault(
                $"https://{keyVault}.vault.azure.net/",
                keyVaultClient,
                new DefaultKeyVaultSecretManager());
        }

        private static KeyVaultClient.AuthenticationCallback AuthenticationCallback(string? accessToken)
        {
            if (accessToken != null)
            {
                return (authority, resource, scope) => Task.FromResult(accessToken);
            }

            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            return new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback);
        }

        public TestConfigurationBuilder WithKeyVaultEnabled(in bool keyVaultEnabled)
        {
            _keyVaultEnabled = keyVaultEnabled;
            return this;
        }
    }
}