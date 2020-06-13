using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CraftersCloud.KeyVault.Demo.Api.Features.Secrets
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("[controller]")]
    public class SecretsController : Controller
    {
        private readonly IMediator _mediator;

        public SecretsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Gets secrets from Azure Key Vault
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<GetSecrets.Response> Get()
        {
            var response = await _mediator.Send(new GetSecrets.Query());
            return response;
        }
    }
}