using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Controllers
{
    public class TestAController : Controller
    {
        private SettingsA _settingsA;

        public TestAController(IOptions<SettingsA> options)
        {
            _settingsA = options.Value;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = _settingsA.TestA;
            return View();
        }
    }
}