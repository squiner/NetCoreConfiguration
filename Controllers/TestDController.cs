using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Controllers
{
    public class TestDController : Controller
    {
        private readonly SettingsD _settings;

        public TestDController(IOptions<SettingsD> options)
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