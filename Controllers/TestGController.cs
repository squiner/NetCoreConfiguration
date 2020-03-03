using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Controllers
{
    public class TestGController : Controller
    {
        private SettingsG _settings;

        public TestGController(IOptionsMonitor<SettingsG> options)
        {
            _settings = options.CurrentValue;
            options.OnChange(OnSettingsChange);
        }

        private void OnSettingsChange(SettingsG arg1)
        {
            _settings = arg1;
        }


        public IActionResult Index()
        {
            ViewData["Title"] = _settings.TestG;
            return View();
        }
    }
}