using ETicaret.Admin.Models;
using ETicaret.Data;
using ETicaret.Data.Common;
using ETicaret.Data.Entities;
using ETicaret.Data.Enums;
using ETicaret.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ETicaret.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ETicaretDbContext _dbcontext;
        public ProductController(IRepository<Product> repository, ETicaretDbContext dbcontext, IRepository<Category> categoryRepository)
        {
            _repository = repository;
            _dbcontext = dbcontext;
            _categoryRepository = categoryRepository;
        }
        public void SelectCategories()
        {
            ViewBag.Categories = _categoryRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.Id.ToString()
            }).ToList();
        }
        public IActionResult Create()
        {
            SelectCategories();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel model)
        {
            SelectCategories();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var ms = new MemoryStream();
            model.Picture.CopyTo(ms);
            var newProduct = new Product()
            {
                ProductName = model.ProductName,
                UnitInStock = model.UnitInStock,
                UnitPrice = model.UnitPrice,
                CategoryId = model.CategoryId,
                CreatedById = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value),
                Picture = ms.ToArray(),
                Description = model.Description,
            };
            var result = _repository.Create(newProduct);
            if (result)
            {
                TempData["CreateSuccess"] = AlertService.ShowAlert(AlertStates.Success, "Ürün ekleme işlemi başarılı.");
                return RedirectToAction(nameof(List));
            }
            ViewBag.CreateError = AlertService.ShowAlert(AlertStates.Danger, "Ürün ekleme esnasında bir hata oluştu.");

            return View(model);
        }
        public IActionResult List()
        {
            var products = _repository.GetAll().Select(x => new ProductViewModel
            {
                Id = x.Id,
                ProductName = x.ProductName,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                CategoryId = x.CategoryId,
                ImageByteArray = x.Picture


            }).ToList();

            products.ForEach(x => x.ImageString = Convert.ToBase64String(x.ImageByteArray));

            return View(products);

        }

        public IActionResult Details(int id)
        {
            var product = _repository.Get(id);

            ProductViewModel productDetail = new()  
            {
                ProductName = product.ProductName,
                Description = product.Description,
                UnitInStock = product.UnitInStock,
                UnitPrice = product.UnitPrice,
                CategoryId = product.CategoryId,
                ImageByteArray = product.Picture,
                CategoryName = _categoryRepository.Get(product.CategoryId).CategoryName
            };
            productDetail.ImageString = Convert.ToBase64String(productDetail.ImageByteArray);
            return View(productDetail);
        }

        public IActionResult Edit(int id)
        {
            //ServiceResult result= ProductService.UpdateProduct(id);
            //if(Results.Errors.Count>0)
            //Tempdata ya hata ekle
            //değilse return view();
            SelectCategories();
            var updateProduct = _repository.Get(id);
            if (updateProduct != null)
            {
                var product = new ProductViewModel
                {
                    Id = updateProduct.Id,
                    ProductName = updateProduct.ProductName,
                    Description = updateProduct.Description,
                    UnitInStock = updateProduct.UnitInStock,
                    UnitPrice = updateProduct.UnitPrice,
                    ImageByteArray = updateProduct.Picture,
                    CategoryId = updateProduct.CategoryId,
                };
                return View(product);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductViewModel model)
        {
            //ProductService --> UpdateProduct(ProductViewModel model)-->İş mantığı-->ProductRepository(Product)
            //Automapper -->Veritabanına kayıt yapar --> Başarılı-->ProductService-->ServiceResult-->UI
            SelectCategories();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var updateProduct = _repository.Get(id);

            if (updateProduct == null)
            {
                ViewBag.EditState = AlertService.ShowAlert(AlertStates.Warning, "Bu Id'ye ait bir ürün bulunamadı.");
                return View(model);
            }
            updateProduct.ProductName = model.ProductName;
            updateProduct.Description = model.Description;
            updateProduct.UnitPrice = model.UnitPrice;
            updateProduct.UnitInStock = model.UnitInStock;
            updateProduct.CategoryId = model.CategoryId;
            updateProduct.CreatedById = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (model.Picture == null || model.Picture.Length == 0)
            {
                updateProduct.Picture = updateProduct.Picture;
            }
            else
            {
                var ms = new MemoryStream();                
                model.Picture.CopyTo(ms);
                updateProduct.Picture = ms.ToArray();
            }


            var result = _repository.Update(updateProduct);
            if (result)
            {
                TempData["EditState"] = AlertService.ShowAlert(AlertStates.Success, "Ürün başarıyla güncellendi !");
                return RedirectToAction(nameof(List));
            }
            SelectCategories();
            ViewBag.EditState = AlertService.ShowAlert(AlertStates.Danger, "Ürün güncelleme esnasında bir hata oluştu lütfen daha sonra tekrar deneyiniz.");
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            var result = _repository.Delete(id);

            if (result)
                TempData["DeleteState"] = AlertService.ShowAlert(AlertStates.Success, "Ürün başarıyla silindi.");
            else
                TempData["DeleteState"] = AlertService.ShowAlert(AlertStates.Danger, "Ürün silme esnasında bir hata oluştu lütfen daha sonra tekrar deneyiniz.");

            return RedirectToAction(nameof(List));

        }
    }
}
