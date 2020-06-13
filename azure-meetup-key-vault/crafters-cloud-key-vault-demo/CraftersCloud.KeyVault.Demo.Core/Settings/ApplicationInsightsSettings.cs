using JetBrains.Annotations;
using Serilog.Events;

namespace CraftersCloud.KeyVault.Demo.Core.Settings
{
    [UsedImplicitly]
    public class ApplicationInsightsSettings
    {
        [UsedImplicitly] public string InstrumentationKey { get; set; } = string.Empty;

        [UsedImplicitly] public LogEventLevel SerilogLogsRestrictedToMinimumLevel { get; set; }
    }
}