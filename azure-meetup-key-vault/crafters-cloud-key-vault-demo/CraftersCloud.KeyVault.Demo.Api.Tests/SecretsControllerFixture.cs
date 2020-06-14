using System.Threading.Tasks;
using CraftersCloud.KeyVault.Demo.Api.Features.Secrets;
using CraftersCloud.KeyVault.Demo.Api.Tests.Infrastructure.Api;
using FluentAssertions;
using NUnit.Framework;

namespace CraftersCloud.KeyVault.Demo.Api.Tests
{
    public static class SecretsControllerFixtureContext
    {
        [Category("integration")]
        public class SecretsControllerFixtureGivenKeyVaultIsDisabled : IntegrationFixtureBase
        {
            [Test]
            public async Task TestGetSecrets()
            {
                var secrets = await Client.GetAsync<GetSecrets.Response>("secrets");

                secrets.SampleKeyVaultSecret.Should().Be("Value1FromInMemoryConfiguration");
                secrets.GitHubApiSuperSecretKey.Should().Be("Value2FromInMemoryConfiguration");
            }
        }

        [Category("integration")]
        public class SecretsControllerFixtureGivenKeyVaultIsEnabled : IntegrationFixtureBase
        {
            public SecretsControllerFixtureGivenKeyVaultIsEnabled()
            {
                EnableKeyVault();
            }

            [Test]
            public async Task TestGetSecrets()
            {
                var secrets = await Client.GetAsync<GetSecrets.Response>("secrets");

                secrets.SampleKeyVaultSecret.Should().Be("I am secret coming from keyvault");
                secrets.GitHubApiSuperSecretKey.Should().Be("I am GitHubApi secret coming from keyvault");
            }
        }
    }
}