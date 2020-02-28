using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetCoreConfiguration.Services;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Configuration
{
    public class ConfigureSettingsFOptions : IConfigureOptions<SettingsF>
    {
        private readonly IServiceProvider _provider;

        public ConfigureSettingsFOptions(IServiceProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SettingsF options)
        {
            using (var scope = _provider.CreateScope())
            {
                var idService = scope.ServiceProvider.GetService<IdServiceScope>();
                options.Guid = idService.GetId();
            }
        }
    }
}
