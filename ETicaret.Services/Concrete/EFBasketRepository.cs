using ETicaret.Data;
using ETicaret.Data.Entities;
using ETicaret.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ETicaret.Services.Concrete
{
    public class EFBasketRepository : IBasketService
    {
        private readonly ETicaretDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<BasketItem> _basketItemRepository;

        public EFBasketRepository(ETicaretDbContext dbContext, IHttpContextAccessor httpContextAccessor, IRepository<BasketItem> basketItemRepository)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _basketItemRepository = basketItemRepository;
        }
        public int CustomerId()
        {
            int id = Convert.ToInt16(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return id;
        }

        public Basket GetBasketASync()
        {
            var basket = _dbContext.Baskets.Include(x => x.BasketItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.CustomerId == CustomerId() && x.IsActive);

            if (basket == null)
            {
                basket = CreateBasket();
            }

            return basket;
        }

        public bool AddItemToBasket(BasketItem basketItem)
        {
            var basket = GetBasketASync();
            var item = basket.BasketItems.FirstOrDefault(x => x.ProductId == basketItem.ProductId);
            if (item != null)
                item.Quantity += basketItem.Quantity;
            else
            {
                item = basketItem;
                item.CreatedById = 1;
                item.CreatedDate = DateTime.Now;
                basket.BasketItems.Add(item);
            }

            return _dbContext.SaveChanges() > 0;

        }

        public bool DeleteBasket()
        {
            var deletedBasket = GetBasketASync();
            deletedBasket.IsActive = false;
            return _dbContext.SaveChanges() > 0;
        }

        public bool DeleteBasketItem(int id)
        {
            var basket = GetBasketASync();
            var deletedBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == id);
            if (deletedBasketItem != null)
            {
                deletedBasketItem.Quantity -= 1;
                if (deletedBasketItem.Quantity == 0)
                {
                    basket.BasketItems.Remove(deletedBasketItem);
                }

            }
            return _dbContext.SaveChanges() > 0;
        }

        public bool DeleteAllBasketItems(int id)
        {
            var basket = GetBasketASync();
            var deleteItems = basket.BasketItems.FirstOrDefault(x => x.ProductId == id);
            if (deleteItems != null)
                deleteItems.Quantity = 0;
            if (deleteItems.Quantity == 0)
            {
                basket.BasketItems.Remove(deleteItems);

            }
            return _dbContext.SaveChanges() > 0;
        }

        public Basket CreateBasket()
        {
            var newbasket = new Basket()
            {
                CreatedById = 1,
                CreatedDate = DateTime.Now,
                CustomerId = CustomerId(),
                IsActive = true
            };
            _dbContext.Baskets.Add(newbasket);

            return newbasket;
        }
    }
}
