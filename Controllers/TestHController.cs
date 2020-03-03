using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCoreConfiguration.Settings;

namespace NetCoreConfiguration.Controllers
{
    public class TestHController : Controller
    {
        private SettingsH _settings;

        public TestHController(IOptions<SettingsH> options, ILogger<TestHController> logger)
        {
            try
            {
                _settings = options.Value;
            }
            catch (OptionsValidationException e)
            {
                foreach (var failure in e.Failures)
                {
                    logger.LogError(failure);
                    throw;
                }
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}