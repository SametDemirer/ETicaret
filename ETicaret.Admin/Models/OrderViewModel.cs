using ETicaret.Data.Entities;
using ETicaret.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETicaret.Admin.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerSurname { get; set; }
        [Required]
        public string Adress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public string CustomerPhone { get; set; }
        public decimal Freight { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public OrderState OrderState { get; set; }
        public int BaketId { get; set; }
        public Basket Basket { get; set; }
        public decimal SubTotalPrice { get; set; }
        public int ProgressPercentage { get; set; }




    }
}
