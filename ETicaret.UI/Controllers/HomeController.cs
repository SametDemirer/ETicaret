using ETicaret.Data.Entities;
using ETicaret.Services.Interfaces;
using ETicaret.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ETicaret.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<Category> _repository;
        public HomeController(ILogger<HomeController> logger, IRepository<Category> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            var categories = _repository.GetAll().Where(x=>x.IsActive).Select(x => new CategoryViewModel
            {
                CategoryName = x.CategoryName,
                Id=x.Id,
                ImageByteArray=x.Image,
            }).ToList();
            categories.ForEach(x => x.ImageString = Convert.ToBase64String(x.ImageByteArray));
            return View(categories);
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