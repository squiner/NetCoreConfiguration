using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Controllers
{
    public class TestFController : Controller
    {
        private readonly SettingsF _settings;

        public TestFController(IOptions<SettingsF> options)
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