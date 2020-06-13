using System.Threading.Tasks;
using CraftersCloud.KeyVault.Demo.Api.Features.Secrets;
using CraftersCloud.KeyVault.Demo.Api.Tests.Infrastructure.Api;
using FluentAssertions;
using NUnit.Framework;

namespace CraftersCloud.KeyVault.Demo.Api.Tests
{
    [Category("integration")]
    public class SecretsControllerFixture : IntegrationFixtureBase
    {
        [Test]
        public async Task TestGetSecrets()
        {
            var secrets = await Client.GetAsync<GetSecrets.Response>("secrets");

            secrets.SampleKeyVaultSecret.Should().Be("Value1FromInMemoryConfiguration");
            secrets.GitHubApiSuperSecretKey.Should().Be("Value2FromInMemoryConfiguration");
        }
    }
}