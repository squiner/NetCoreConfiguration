using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Controllers
{
    public class TestBController : Controller
    {
        private readonly SettingsB _settingsB;

        public TestBController(IOptionsSnapshot<SettingsB> options)
        {
            _settingsB = options.Value;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = _settingsB.TestB;
            return View();
        }
    }
}