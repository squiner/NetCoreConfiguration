using Microsoft.Extensions.Options;
using NetCoreConfiguration.Services;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Configuration
{
    public class ConfigureSettingsDOptions : IConfigureOptions<SettingsD>
    {
        private readonly IdService _service;

        public ConfigureSettingsDOptions(IdService service)
        {
            _service = service;
        }

        public void Configure(SettingsD options)
        {
            options.Guid = _service.GetId();
        }
    }

}
