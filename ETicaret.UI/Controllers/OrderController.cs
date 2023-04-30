using ETicaret.Data;
using ETicaret.Data.Common;
using ETicaret.Data.Entities;
using ETicaret.Data.Enums;
using ETicaret.Services.Interfaces;
using ETicaret.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Security.Claims;

namespace ETicaret.UI.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        private readonly IBasketService _basketService;
        private readonly IRepository<Order> _repository;
        private readonly IRepository<Customer> _customerRepository;


        public OrderController(IRepository<Order> repository, IRepository<Customer> customerRepository, IBasketService basketService)
        {
            _repository = repository;
            _customerRepository = customerRepository;
            _basketService = basketService;

        }
        public IActionResult Checkout()
        {
            var currentUser = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var user = _customerRepository.Get(currentUser);
            var basket = _basketService.GetBasketASync();
            if (basket == null)
            {
                return View();
            }
            var order = new OrderViewModel()
            {
                Adress = user.Adress,
                City = user.City,
                Country = user.Country,
                CustomerName = user.Name,
                CustomerSurname = user.Surname,
                CustomerPhone = user.Phone,
                Basket = basket,
                BasketId = basket.Id
            };
            return View(order);
        }

        [HttpPost]
        public IActionResult Checkout(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var currentUser = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var user = _customerRepository.Get(currentUser);
            var basket = _basketService.GetBasketASync();
            var newOrder = new Order()
            {
                Adress = model.Adress,
                Basket = basket,
                BasketId = basket.Id,
                City = model.City,
                Country = model.Country,
                CustomerId = currentUser,
                CustomerName = user.Name,
                CustomerSurname = user.Surname,
                CustomerPhone = user.Phone,
                OrderState = OrderState.OrderReceiving,
                Freight = basket.BasketItems.Sum(x => x.SubToTalPrice)
            };


            var result = _repository.Create(newOrder);
            if (result)
            {
                _basketService.DeleteBasket();
                TempData["OrderSuccess"] = AlertService.ShowAlert(AlertStates.Success, "Siparişiniz alındı !");
            }
            return RedirectToAction("List", "Card");
        }

        [HttpGet]
        public IActionResult OrderHistory()
        {
            var currentUser = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

            var pastOrders = _repository
                .GetAll(x => x.Include(b => b.Basket).ThenInclude(bi => bi.BasketItems))
                .Where(x => x.CustomerId == currentUser)
                .OrderByDescending(x => x.CreatedDate)
                .Select(x => new OrderViewModel
                {
                    CustomerName = x.CustomerName,
                    Freight = x.Freight,
                    OrderDate = x.CreatedDate,
                    OrderState = x.OrderState,
                    Quantity = x.Basket.BasketItems.Count(),
                    Id = x.Id

                }).ToList();

            if (pastOrders.Count == 0)
            {
                ViewBag.OrdersNull = AlertService.ShowAlert(AlertStates.Info, "Geçmiş siparişiniz bulunmamaktadır.");
            }

            return View(pastOrders);
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            var order = _repository.Get(id, x => x.Include(b => b.Basket).ThenInclude(bi => bi.BasketItems).ThenInclude(p => p.Product));
            if (order is null)
            {
                TempData["Message"] = AlertService.ShowAlert(AlertStates.Warning, "Böyle bir sipariş bulunamadı !");
                return RedirectToAction(nameof(OrderHistory));
            }
            var orderModel = new OrderViewModel()
            {
                Basket=order.Basket,
                Freight = order.Freight,
                CustomerName = order.CustomerName,
                OrderDate = order.CreatedDate,
                OrderState = order.OrderState             
            };
            return View(orderModel);
        }
    }
}

