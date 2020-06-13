using System.Net.Http;

namespace CraftersCloud.KeyVault.Demo.Api.Tests.Common
{
    public static class AssertionExtensions
    {
        public static HttpResponseAssertions Should(this HttpResponseMessage actualValue)
        {
            return new HttpResponseAssertions(actualValue);
        }
    }
}