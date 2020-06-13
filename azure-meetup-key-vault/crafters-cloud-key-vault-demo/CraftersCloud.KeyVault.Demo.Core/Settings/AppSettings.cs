using JetBrains.Annotations;

namespace CraftersCloud.KeyVault.Demo.Core.Settings
{
    [UsedImplicitly]
    public class AppSettings
    {
        [UsedImplicitly] public GitHubApiSettings GitHubApi { get; set; } = new GitHubApiSettings();
    }
}