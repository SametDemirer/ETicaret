using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Entities
{
    public class BasketItem : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal SubToTalPrice 
        {
            get
            {
                return this.Product.UnitPrice * this.Quantity;
            }
        }
        public string ProductPicture { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }

    }
}
