using System.Threading;
using System.Threading.Tasks;
using CraftersCloud.KeyVault.Demo.Core.Settings;
using JetBrains.Annotations;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CraftersCloud.KeyVault.Demo.Api.Features.Secrets
{
    public static class GetSecrets
    {
        [PublicAPI]
        public class Query : IRequest<Response>
        {
        }

        [PublicAPI]
        public class Response
        {
            public string SampleKeyVaultSecret { get; set; } = string.Empty;
            public string GitHubApiSuperSecretKey { get; set; } = string.Empty;
        }

        [UsedImplicitly]
        public class RequestHandler : IRequestHandler<Query, Response>
        {
            private readonly IConfiguration _configuration;
            private readonly GitHubApiSettings _gitHubApiSettings;

            public RequestHandler(IConfiguration configuration, GitHubApiSettings gitHubApiSettings)
            {
                _configuration = configuration;
                _gitHubApiSettings = gitHubApiSettings;
            }

            public Task<Response> Handle(Query query, CancellationToken cancellationToken)
            {
                var sampleKeyVaultSecret = _configuration.GetValue<string>("App:SampleKeyVaultSecret");
                var gitHubApiSuperSecretKey = _gitHubApiSettings.SuperSecretKey;
                return Task.FromResult(new Response
                {
                    SampleKeyVaultSecret = sampleKeyVaultSecret,
                    GitHubApiSuperSecretKey = gitHubApiSuperSecretKey
                });
            }
        }
    }
}