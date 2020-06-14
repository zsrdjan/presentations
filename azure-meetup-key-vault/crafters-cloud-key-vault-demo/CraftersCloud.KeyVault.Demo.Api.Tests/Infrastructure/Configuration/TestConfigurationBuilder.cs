using System.Collections.Generic;
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
                {"App:GitHubApi:SuperSecretKey", "Value2FromInMemoryConfiguration"},
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
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(
                new KeyVaultClient.AuthenticationCallback(
                    azureServiceTokenProvider.KeyVaultTokenCallback));

            configurationBuilder.AddAzureKeyVault(
                "https://cc-keyvault-demo.vault.azure.net/",
                keyVaultClient,
                new DefaultKeyVaultSecretManager());
        }

        public TestConfigurationBuilder WithKeyVaultEnabled(in bool keyVaultEnabled)
        {
            _keyVaultEnabled = keyVaultEnabled;
            return this;
        }
    }
}