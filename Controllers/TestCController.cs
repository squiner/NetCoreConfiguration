using Microsoft.AspNetCore.Mvc;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Controllers
{
    public class TestCController : Controller
    {
        private ISettingsC _settingsC;

        public TestCController(ISettingsC settingsC)
        {
            _settingsC = settingsC;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = _settingsC.TestC;
            return View();
        }
    }
}