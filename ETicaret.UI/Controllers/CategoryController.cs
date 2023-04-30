using ETicaret.Data;
using ETicaret.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETicaret.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ETicaretDbContext _dbContext;

        public CategoryController(ETicaretDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PartialViewResult GetCategories()
        {
            var categories = _dbContext.Categories.Where(x=>x.IsActive).Select(x => new CategoryViewModel()
            {
                Id = x.Id,
                CategoryName = x.CategoryName
            }).ToList();
            return  PartialView("_GetCategoriesPartialView", categories);
        }
    }
}
