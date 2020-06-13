using JetBrains.Annotations;

namespace CraftersCloud.KeyVault.Demo.Core.Settings
{
    [UsedImplicitly]
    public class GitHubApiSettings
    {
        public string SuperSecretKey { get; set; } = string.Empty;
    }
}