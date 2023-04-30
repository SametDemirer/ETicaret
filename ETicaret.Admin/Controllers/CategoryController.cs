using ETicaret.Admin.Models;
using ETicaret.Data;
using ETicaret.Data.Common;
using ETicaret.Data.Entities;
using ETicaret.Data.Enums;
using ETicaret.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaret.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private readonly IRepository<Category> _repository;
        private readonly ETicaretDbContext _dbcontext;

        public CategoryController(IRepository<Category> repository, ETicaretDbContext dbcontext)
        {
            _repository = repository;
            _dbcontext = dbcontext;
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var newCategory = new Category()
            {
                CategoryName = model.CategoryName,
                Description = model.Description,
                IsActive = true,
                CreatedById = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value),
                CreatedDate = DateTime.Now,
            };
            var ms = new MemoryStream();
            model.Picture.CopyTo(ms);
            newCategory.Image = ms.ToArray();

            var result = _repository.Create(newCategory);
            if (result)
            {
                TempData["CreateSuccess"] = AlertService.ShowAlert(AlertStates.Success, "Kategori eklendi.");
                return RedirectToAction(nameof(List));
            }
            ViewBag.CreateFailed = AlertService.ShowAlert(AlertStates.Danger, "Kategori ekleme esnasında bir problem oluştu lütfen daha sonra tekrar deneyiniz.");
            return View(model);
        }
        public IActionResult List()
        {
            var kategoriler = _repository.GetAll().Select(x => new CategoryViewModel
            {
                Id = x.Id,
                CategoryName = x.CategoryName,
                Description = x.Description,
                ImageByteArray = x.Image
            }).ToList();
            kategoriler.ForEach(x =>
            {
                x.ImageString = Convert.ToBase64String(x.ImageByteArray);
            });

            return View(kategoriler);
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var category = _dbcontext.Categories.FirstOrDefault(x => x.Id == id && x.IsActive);
            if (category == null)
            {
                ViewBag.WrongId = AlertService.ShowAlert(AlertStates.Warning, "Bu Id'ye sahip bir kategori bulunamadı.");
                return View(model);
            }
            category.CategoryName = model.CategoryName;
            category.Description = model.Description;
            category.UpdatedById = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            category.UpdatedDate = DateTime.Now;
            if (model.Picture != null && model.Picture.Length > 0)
            {
                var ms = new MemoryStream();
                model.Picture.CopyTo(ms);
                category.Image = ms.ToArray();
            }
            var result = _repository.Update(category);
            if (result)
            {
                TempData["CategoryUpdate"] = AlertService.ShowAlert(AlertStates.Success, "Kategori başarıyla güncellendi.");
                return RedirectToAction(nameof(List));
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = _repository.Delete(id);

            if (result)
                TempData["DeletingState"] = AlertService.ShowAlert(AlertStates.Success, "Kategori başarıyla silindi");
            else            
                TempData["DeletingState"] = AlertService.ShowAlert(AlertStates.Danger, "Kategori silinirken bir hata oluştu lütfen daha sonra tekrar deneyiniz.");
            
            return RedirectToAction(nameof(List));
        }

    }
}
