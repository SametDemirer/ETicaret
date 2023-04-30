using ETicaret.Data;
using ETicaret.Data.Common;
using ETicaret.Data.Entities;
using ETicaret.Data.Enums;
using ETicaret.Services.Interfaces;
using ETicaret.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ETicaret.UI.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ETicaretDbContext _context;
        private readonly IRepository<Product> _productRepository;
        public ProductController(ETicaretDbContext context, IRepository<Product> productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        public IActionResult List(int? id)
        {
            var query = _productRepository.GetAll();
            if (id!=null)
            {
                query = query.Where(x => x.CategoryId == id).ToList();
            }
            var products = query.Select(x => new ProductViewModel
            {
                CategoryId = x.CategoryId,
                Description = x.Description,
                ImageByteArray=x.Picture,
                ProductName = x.ProductName,
                UnitInStock = x.UnitInStock,
                UnitPrice = x.UnitPrice,
                Id = x.Id,
            }).ToList();
            products.ForEach(x=>x.ImageString=Convert.ToBase64String(x.ImageByteArray));

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _productRepository.Get(id);
            if (product==null)
            {
                TempData["Product"] = AlertService.ShowAlert(AlertStates.Danger, "Aradığınız ürün bulunamadı.");
                return RedirectToAction(nameof(List));
            }
            ProductViewModel productDetail = new()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                UnitInStock = product.UnitInStock,
                UnitPrice = product.UnitPrice,
                CategoryId = product.CategoryId,
                ImageByteArray = product.Picture,

            };
            productDetail.ImageString = Convert.ToBase64String(productDetail.ImageByteArray);
            return View(productDetail);
            
        }

      
    }
}
