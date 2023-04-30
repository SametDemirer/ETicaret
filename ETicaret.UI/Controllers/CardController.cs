using ETicaret.Data;
using ETicaret.Data.Entities;
using ETicaret.Services.Interfaces;
using ETicaret.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace ETicaret.UI.Controllers
{
    [Authorize]
    public class CardController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IRepository<Product> _productRepository;
        public CardController( IRepository<Product> productRepository, IBasketService basketService)
        {
           
            _productRepository = productRepository;
            _basketService = basketService;
        }
        public IActionResult List()
        {
            var basket=_basketService.GetBasketASync().BasketItems.Select(x=>new BasketItemViewModel
            {
                Quantity=x.Quantity,
                Basket=x.Basket,
                BasketId=x.BasketId,
                Product = x.Product,
                ProductId = x.ProductId,
   
            }).ToList();  
            return View(basket);
        }

        public IActionResult Add(int id)
        {
            
            var product = _productRepository.Get(id);
            if (product==null)
            {
                return RedirectToAction("List", "Product");
            }
            var basketItem = new BasketItem()
            {
                ProductId=id,
                Product=product,
                Quantity=1,
                ProductPicture=Convert.ToBase64String(product.Picture)
            };
            var addBasket = _basketService.AddItemToBasket(basketItem);
            return RedirectToAction(nameof(List));
        }
        public IActionResult Delete(int id)
        {
            var deleteItem=_basketService.DeleteBasketItem(id);

            return RedirectToAction(nameof(List));
        }

        public IActionResult DeleteAllItems(int id)
        {
            var deleteItems = _basketService.DeleteAllBasketItems(id);
            return RedirectToAction(nameof(List));
        }
    }
}
