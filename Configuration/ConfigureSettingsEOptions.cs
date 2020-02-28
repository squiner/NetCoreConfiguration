using Microsoft.Extensions.Options;
using NetCoreConfiguration.Services;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Configuration
{
    public class ConfigureSettingsEOptions : IConfigureOptions<SettingsE>
    {
        private readonly IdServiceSnap _service;

        public ConfigureSettingsEOptions(IdServiceSnap service)
        {
            _service = service;
        }

        public void Configure(SettingsE options)
        {
            options.Guid = _service.GetId();
        }
    }
}
