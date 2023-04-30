using ETicaret.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Proje.Admin.Controllers
{
    [Authorize]
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
            //return View(/*new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }*/);
            var errorCode = HttpContext.Request.Query["errorCode"].ToString();
            var errorMessage = HttpContext.Request.Query["errorMessage"].ToString();

            var model = new ErrorViewModel
            {
                ErrorCode = errorCode,
                ErrorMessage = errorMessage
            };

            return View(model);
        }
    }
}