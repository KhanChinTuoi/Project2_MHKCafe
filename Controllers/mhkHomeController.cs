using System.Diagnostics;
using MHKCafe.Models;
using Microsoft.AspNetCore.Mvc;

namespace MHKCafe.Controllers
{
    public class mhkHomeController : Controller
    {
        private readonly ILogger<mhkHomeController> _logger;

        public mhkHomeController(ILogger<mhkHomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
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
