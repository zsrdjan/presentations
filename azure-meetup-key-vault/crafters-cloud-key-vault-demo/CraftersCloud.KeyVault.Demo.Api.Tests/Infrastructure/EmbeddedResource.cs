using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace CraftersCloud.KeyVault.Demo.Api.Tests.Infrastructure
{
    public static class EmbeddedResource
    {
        public static string ReadResourceContent(string namespaceAndFileName)
        {
            try
            {
                using var stream = typeof(EmbeddedResource).GetTypeInfo().Assembly
                    .GetManifestResourceStream(namespaceAndFileName);
                if (stream == null) return string.Empty;
                using var reader = new StreamReader(stream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch (Exception exception)
            {
                throw new Exception($"Failed to read Embedded Resource {namespaceAndFileName}", exception);
            }
        }
    }
}