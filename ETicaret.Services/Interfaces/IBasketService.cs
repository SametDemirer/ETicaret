using ETicaret.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Services.Interfaces
{
    public interface IBasketService
    {
        Basket GetBasketASync();
        bool AddItemToBasket(BasketItem basketItem);
        bool DeleteBasketItem(int id);
        bool DeleteBasket();
        bool DeleteAllBasketItems(int id);
        Basket CreateBasket();
        


    }
}
