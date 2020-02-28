using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreConfiguration.Models;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppSettings _settings;

        public HomeController(ILogger<HomeController> logger, AppSettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = _settings.Title;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
