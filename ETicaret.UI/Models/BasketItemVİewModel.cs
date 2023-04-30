using ETicaret.Data.Entities;

namespace ETicaret.UI.Models
{
    public class BasketItemViewModel
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
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
    
}
