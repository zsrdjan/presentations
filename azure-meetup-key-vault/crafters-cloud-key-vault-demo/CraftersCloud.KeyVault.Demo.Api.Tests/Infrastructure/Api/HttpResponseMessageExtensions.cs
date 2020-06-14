﻿using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Rest;
using Newtonsoft.Json;

namespace CraftersCloud.KeyVault.Demo.Api.Tests.Infrastructure.Api
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static async Task<T> DeserializeWithStatusCodeCheckAsync<T>(this HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<T>(content);

            throw DisposeResponseContentAndThrowException(response, content);
        }

        public static async Task EnsureSuccessStatusCodeAsync(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                DisposeResponseContentAndThrowException(response, content);
            }
        }

        private static HttpOperationException DisposeResponseContentAndThrowException(HttpResponseMessage response,
            string content)
        {
            // Disposing the content should help users: If users call EnsureSuccessStatusCode(), an exception is
            // thrown if the statusCode status code is != 2xx. I.e. the behavior is similar to a failed request (e.g.
            // connection failure). Users are not expected to dispose the content in this case: If an exception is 
            // thrown, the object is responsible fore cleaning up its state.
            response.Content?.Dispose();
            throw new HttpOperationException(content);
        }
    }
}