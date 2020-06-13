using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace CraftersCloud.KeyVault.Demo.Api.Tests.Infrastructure.Configuration
{
    public class TestConfigurationBuilder
    {
        public IConfiguration Build()
        {
            var configurationBuilder = new ConfigurationBuilder();

            var dict = new Dictionary<string, string>
            {
                {"App:SampleKeyVaultSecret", "Value1FromInMemoryConfiguration"},
                {"App:GitHubApi:SuperSecretKey", "Value2FromInMemoryConfiguration"},
            };

            configurationBuilder.AddInMemoryCollection(dict);
            return configurationBuilder.Build();
        }
    }
}