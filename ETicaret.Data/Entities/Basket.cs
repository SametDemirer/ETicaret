using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Entities
{
    public class Basket:BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public List<BasketItem> BasketItems { get; set; }=new List<BasketItem>();
    }
}
