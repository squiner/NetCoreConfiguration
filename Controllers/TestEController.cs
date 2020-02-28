using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Controllers
{
    public class TestEController : Controller
    {
        private readonly SettingsE _settings;

        public TestEController(IOptionsSnapshot<SettingsE> options)
        {
            _settings = options.Value;
        }

        public IActionResult Index()
        {
            ViewData["Id"] = _settings.Guid;
            return View();
        }
    }
}