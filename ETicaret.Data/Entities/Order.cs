using ETicaret.Data.Enums;
using Microsoft.Build.Framework;
using Microsoft.Build.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerSurname { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string City { get; set; }
        public decimal Freight { get; set; }
        [Required]
        public string Country { get; set; }
        public string CustomerPhone { get; set; }
        public OrderState OrderState { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }

    }
}
