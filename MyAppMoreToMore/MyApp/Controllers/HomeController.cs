//obs³uguje nadchodz¹ce zapytania HTTP i oddzialowuje na model w celu zwrocenia odpowiedniego widoku
using Microsoft.AspNetCore.Mvc;
using MyAppMoreToMore.Models;
using System.Diagnostics;

namespace MyAppMoreToMore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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
