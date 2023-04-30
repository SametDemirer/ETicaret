using ETicaret.Admin.Models;
using ETicaret.Data.Common;
using ETicaret.Data.Entities;
using ETicaret.Data.Enums;
using ETicaret.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace ETicaret.Admin.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orderRepository;
        public OrderController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
           
        }
        public void OrderEnumList()
        {
            var orderStates = new List<string>();
            foreach (var item in Enum.GetNames(typeof(OrderState)))
            {
                orderStates.Add(item.ToString());

            }
            ViewBag.OrderState = orderStates.Select(x => new SelectListItem
            {
                Text = x,
            });
        }
        [HttpGet]
        public IActionResult List()
        {
            var orders = _orderRepository.GetAll().OrderByDescending(x=>x.CreatedDate).Select(x=> new OrderViewModel
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                CustomerSurname = x.CustomerSurname,
                Freight = x.Freight,                
                OrderState = x.OrderState,
                OrderDate = x.CreatedDate
            }).ToList();

            if (orders == null)
            {
                ViewBag.OrdersNull = AlertService.ShowAlert(AlertStates.Info, " Yeni Sipariş bulunmamaktadır");
                return View();
            }

            return View(orders);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var order = _orderRepository.Get(id, x => x.Include(b => b.Basket).ThenInclude(bi => bi.BasketItems).ThenInclude(p => p.Product));
            var orderDetails = new OrderViewModel()
            {
                Basket = order.Basket,
                BaketId = order.BasketId,
                Adress = order.Adress,
                City = order.City,
                Country = order.Country,
                CustomerName = order.CustomerName,
                CustomerPhone = order.CustomerPhone,
                CustomerSurname = order.CustomerSurname,
                Freight = order.Basket.BasketItems.Sum(f => f.SubToTalPrice),
                Quantity = order.Basket.BasketItems.Sum(y => y.Quantity),
                OrderDate = order.CreatedDate,
                OrderState = order.OrderState,
                Id = order.Id,
            };
            return View(orderDetails);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            OrderEnumList();
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, OrderEditViewModel model)
        {
            OrderEnumList();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var order = _orderRepository.Get(id);
            order.OrderState = model.OrderState;
            if (model.OrderState == OrderState.OrderCompleted)
            {
                order.IsActive = false;
            }
            var result = _orderRepository.Update(order);
            if (result)
            {
                return RedirectToAction(nameof(List));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult CompletedOrders()
        {
            var completedOrders = _orderRepository.GetAllUnactives()
                .Select(x => new OrderViewModel
            {
                Id = x.Id,
                CustomerName = x.CustomerName,
                CustomerSurname = x.CustomerSurname,
                Freight = x.Freight,               
                OrderState = x.OrderState,
                OrderDate = x.CreatedDate
            }).OrderByDescending(x => x.OrderDate).ToList();
            if (completedOrders == null)
            {
                ViewBag.OrdersNull = AlertService.ShowAlert(AlertStates.Info, " Tamamlanmış sipariş bulunmamaktadır !");
                return View();
            }
            return View(completedOrders);
        }
    }
}
